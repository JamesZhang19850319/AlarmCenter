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
using CustomWPFColorPicker;
using AlarmCenter.DAL;

namespace AlarmCenter.UI.Settings
{
    /// <summary>
    /// ProcessingStrategiesEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StrategiesEditWindow : Window
    {
        //是新增数据还是修改数据
        public bool IsInsert { get; set; }

        //如果是编辑的话被编辑行的ID
        public int strategiesID { get; set; }

        public StrategiesEditWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 50; i++)
                cbStrategiesID.Items.Add((i + 1).ToString());

            //增加可选项
            cbNoticeType.Items.Add("仅声音提示");
            cbNoticeType.Items.Add("声音和窗口提示");
            cbNoticeType.Items.Add("不提示");

            if (IsInsert)
            {
                Strategies strategies = new Strategies();
                gridStrategies.DataContext = strategies;
            }
            else//修改
            {
                Strategies strategies = new StrategiesDAL().GetStrategiesByID(strategiesID);
                gridStrategies.DataContext = strategies;

                //字符串转换成brush
                BrushConverter brushConverter = new BrushConverter();
                EventFontColorPicker.CurrentColor=(SolidColorBrush)brushConverter.ConvertFromString(strategies.EventFontColor);
                EventBackgroundColorPicker.CurrentColor = (SolidColorBrush)brushConverter.ConvertFromString(strategies.EventBackgroundColor);
                cbStrategiesID.SelectedItem = strategies.StrategiesID;
                cbNoticeType.SelectedItem = strategies.NoticeType;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsInsert)
            {
                //grid.DataContext指向的User对象
                //用户修改过程中，数据绑定会自动把界面的修改同步到User对象中
                Strategies strategies = (Strategies)gridStrategies.DataContext;
                if (cbNoticeType.SelectedItem == null)
                {
                    MessageBox.Show("请选择提示类型！ —_—");
                    return;
                }
                if (cbStrategiesID.SelectedItem == null)
                {
                    MessageBox.Show("请选择编号！ —_—");
                    return;
                }
                if (tbStrategiesName.Text == "")
                {
                    MessageBox.Show("请输入名称！ —_—");
                    return;
                }
                strategies.EventFontColor= tbEventFontColor.Text;
                strategies.EventBackgroundColor = tbEventBackgroundColor.Text;
                strategies.StrategiesID = cbStrategiesID.SelectedItem.ToString();
                strategies.NoticeType = cbNoticeType.SelectedItem.ToString();

                try
                {
                    new StrategiesDAL().Insert(strategies);//插入数据库
                }
                catch
                {
                    MessageBox.Show("编号不能重复！请选择其他编号 *_* ");
                    return;
                }
            }
            else
            {
                //先从数据库中查询旧的值，然后把界面中的值设置到旧对象上
                //Update更新
                Strategies strategies = (Strategies)gridStrategies.DataContext;
                if (cbNoticeType.SelectedItem == null)
                {
                    MessageBox.Show("请选择提示类型！ —_—");
                    return;
                }
                if (cbStrategiesID.SelectedItem == null)
                {
                    MessageBox.Show("请选择编号！ —_—");
                    return;
                }
                if (tbStrategiesName.Text == "")
                {
                    MessageBox.Show("请输入名称！ —_—");
                    return;
                }

                strategies.EventFontColor = tbEventFontColor.Text;
                strategies.EventBackgroundColor = tbEventBackgroundColor.Text;
                strategies.StrategiesID = cbStrategiesID.SelectedItem.ToString();
                strategies.NoticeType = cbNoticeType.SelectedItem.ToString();

                StrategiesDAL dal = new StrategiesDAL();
                dal.Update(strategies);
            }
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
