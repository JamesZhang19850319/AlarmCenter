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

namespace AlarmCenter.UI.Zones
{
    /// <summary>
    /// ZoneEdit.xaml 的交互逻辑
    /// </summary>
    public partial class ZoneEdit : Window
    {
        //是新增数据还是修改数据
        public bool IsInsert { get; set; }

        //如果是编辑的话被编辑行的ID
        public int zoneID { get; set; }
        public string account{get;set;}

        public ZoneEdit()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 8;i++ )
                cbPartitionNum.Items.Add((i+1).ToString());
            for (int i = 0; i < 999; i++)
                cbZoneNum.Items.Add((i+1));

            if (IsInsert)
            {
                Zone zone = new Zone();
                gridZone.DataContext = zone;
            }
            else//修改
            {
                Zone zone = new ZoneDAL().GetZoneByID(zoneID);
                gridZone.DataContext = zone;

                cbPartitionNum.SelectedItem = zone.PartitionNum;
                cbZoneNum.SelectedItem = zone.ZoneNum;
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsInsert)
            {
                //grid.DataContext指向的User对象
                //用户修改过程中，数据绑定会自动把界面的修改同步到User对象中
                Zone zone = (Zone)gridZone.DataContext;
                zone.Account = account;
                if (cbPartitionNum.SelectedItem != null)
                    zone.PartitionNum = cbPartitionNum.SelectedItem.ToString();
                if (cbZoneNum.SelectedItem != null)
                    zone.ZoneNum = Convert.ToInt32(cbZoneNum.SelectedItem.ToString());

                try
                {
                    new ZoneDAL().Insert(zone);//插入数据库
                }
                catch
                {
                    MessageBox.Show("防区编号不能重复！请输入其他主机编号 *_* ");
                    return;
                }
            }
            else
            {
                //先从数据库中查询旧的值，然后把界面中的值设置到旧对象上
                //Update更新
                Zone zone = (Zone)gridZone.DataContext;
                if (cbPartitionNum.SelectedItem != null)
                    zone.PartitionNum = cbPartitionNum.SelectedItem.ToString();
                if (cbZoneNum.SelectedItem != null)
                    zone.ZoneNum = Convert.ToInt32(cbZoneNum.SelectedItem.ToString());
                ZoneDAL dal = new ZoneDAL();
                dal.Update(zone);
            }
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
