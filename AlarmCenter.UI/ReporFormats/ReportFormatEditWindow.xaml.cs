using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AlarmCenter.DAL;

namespace AlarmCenter.UI.ReporFormats
{
    /// <summary>
    /// ReportFormatEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReportFormatEditWindow : Window
    {
        //是新增数据还是修改数据
        public bool IsInsert { get; set; }

        //如果是编辑的话被编辑行的ID
        public string tableName { set; get; }
        public int cidID { get; set; }

        public ReportFormatEditWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i=100; i < 1000; i++)
                cbCIDCode.Items.Add(i.ToString());
            StrategiesDAL strategiesDAL = new StrategiesDAL();
            foreach (Strategies i in strategiesDAL.GetStrategiesArray())
            {
                cbStrategiesName.Items.Add(i.StrategiesName);
            }
            if (IsInsert)
            {
                CID cid = new CID();
                gridCIDEdit.DataContext = cid;
            }
            else//修改
            {
                CID cid = new CIDDAL().GetReportFormatByID(tableName, cidID);
                cbCIDCode.SelectedItem = cid.CIDCode;
                cbStrategiesName.SelectedItem = cid.StrategiesName;
                gridCIDEdit.DataContext = cid;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //grid.DataContext指向的对象
            CID cid = (CID)gridCIDEdit.DataContext;
            if (cbCIDCode.SelectedItem == null)
            {
                MessageBox.Show("请选择CID码！ —_—");
                return;
            }
            if (cbStrategiesName.SelectedItem == null)
            {
                MessageBox.Show("请选择处理策略！ —_—");
                return;
            }
            if (tbEventTpye.Text == "")
            {
                MessageBox.Show("请输入事件类型！ —_—");
                return;
            }
            if (tbEventInformation.Text == "")
            {
                MessageBox.Show("请输入辅助信息！ —_—");
                return;
            }

            cid.CIDCode = cbCIDCode.SelectedItem.ToString();
            cid.StrategiesName = cbStrategiesName.SelectedItem.ToString();

            CIDDAL dal = new CIDDAL();
            if (IsInsert)
            {
                try
                {
                    dal.Insert(tableName, cid);//插入数据库
                }
                catch
                {
                    MessageBox.Show("CID码不能重复！请选择其他编号 *_* ");
                    return;
                }
            }
            else
            {
                dal.Update(tableName, cid);
            }
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
