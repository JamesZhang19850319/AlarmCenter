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
using AlarmCenter.UI.Users;
using AlarmCenter.DAL;

namespace AlarmCenter.UI.Users
{
    /// <summary>
    /// UserList.xaml 的交互逻辑
    /// </summary>
    public partial class UserList : Window
    {
        public int pageIndex = 1;
        public int pageCount;
        public const int pageSize = 1000;

        public UserList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            UserDAL dal = new UserDAL();
            //CreateDataGrid(grid);
            grid.ItemsSource = dal.GetUsersDataTable("Main", "用户资料", pageSize, pageIndex);
            pageCount = dal.pageCount;
            tbAllPage.Text = pageCount.ToString();
            tbPageSize.Text = pageSize.ToString();
            tbPageIndex.Text = pageIndex.ToString();
        }
        //private  void CreateDataGrid(DataGrid dataGrid)
        //{
        //    //自定义DataGrid
        //    //dataGrid = null;
        //    //dataGrid.Height = 340;
        //    //dataGrid.Margin = new Thickness(10, 30, 0, 0);
        //    //dataGrid.IsReadOnly = true;
        //    //dataGrid.AutoGenerateColumns = false;
        //    //dataGrid.CanUserResizeColumns = false;
        //    System.Windows.Data.Binding binding = null;
        //    binding = new System.Windows.Data.Binding("E_ID");
        //    binding.Mode = System.Windows.Data.BindingMode.OneWay;
        //    DataGridTextColumn dgtcE_ID = null;
        //    dgtcE_ID = new DataGridTextColumn();
        //    dgtcE_ID.Header = "编号";
        //    dgtcE_ID.Width = 70;
        //    dgtcE_ID.Visibility = Visibility.Collapsed;
        //    dgtcE_ID.Binding = binding;
        //    dataGrid.Columns.Add(dgtcE_ID);
        //    binding = new System.Windows.Data.Binding("E_Type");
        //    binding.Mode = System.Windows.Data.BindingMode.OneWay;
        //    DataGridTextColumn dgtcE_Type = null;
        //    dgtcE_Type = new DataGridTextColumn();
        //    dgtcE_Type.Header = "下拉框名称";
        //    dgtcE_Type.Width = 200;
        //    dgtcE_Type.Binding = binding;
        //    dataGrid.Columns.Add(dgtcE_Type);
        //    binding = new System.Windows.Data.Binding("E_TypeName");
        //    binding.Mode = System.Windows.Data.BindingMode.OneWay;
        //    DataGridTextColumn dgtcE_TypeName = null;
        //    dgtcE_TypeName = new DataGridTextColumn();
        //    dgtcE_TypeName.Header = "下拉框内容";
        //    dgtcE_TypeName.Width = 200;
        //    dgtcE_TypeName.Binding = binding;
        //    dataGrid.Columns.Add(dgtcE_TypeName);
        //    binding = new System.Windows.Data.Binding("状态");
        //    binding.Mode = System.Windows.Data.BindingMode.OneWay;
        //    DataGridTextColumn dgtcReveaState = null;
        //    dgtcReveaState = new DataGridTextColumn();
        //    dgtcReveaState.Header = "是否启用";
        //    dgtcReveaState.Width = 120;
        //    dgtcReveaState.Binding = binding;
        //    dataGrid.Columns.Add(dgtcReveaState);

        //    //DataGrid中Column的内容居中
        //    //Style styleRight = new Style(typeof(TextBlock));
        //    //Setter setRight = new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
        //    //styleRight.Setters.Add(setRight);
        //    //foreach (DataGridColumn c in dataGrid.Columns)
        //    //{
        //    //    DataGridTextColumn tc = c as DataGridTextColumn;
        //    //    if (tc != null)
        //    //    {
        //    //        tc.ElementStyle = styleRight;
        //    //    }
        //    //}
        //    ////表头居中
        //    //Style style = new Style(typeof(DataGridColumnHeader));
        //    //setRight = new Setter(DataGridColumnHeader.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
        //    //style.Setters.Add(setRight);
        //    //dataGrid.ColumnHeaderStyle = style;

        //}
                                        
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            UserEdit editUI = new UserEdit();
            editUI.IsInsert = true;
            if (editUI.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)grid.SelectedItem;
            if (user == null)
            {
                MessageBox.Show("请选择要编辑的行！");
                return;
            }
            UserEdit editUI = new UserEdit();
            editUI.IsInsert = false;
            editUI.EditingID = user.ID;
            if (editUI.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)grid.SelectedItem;
            if (user == null)
            {
                MessageBox.Show("请选择要删除的行！");
                return;
            }

            if (MessageBox.Show("确认删除此条数据吗？", "提醒",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                new UserDAL().DeleteByID(user.ID);

                LoadData();//刷新数据
            }
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            //下一页
            if (pageIndex == pageCount)
            {
                return;
            }
            pageIndex++;
            LoadData();
        }

        private void btnUpPage_Click(object sender, RoutedEventArgs e)
        {
            if (pageIndex == 1)
            {
                return;
            }
            pageIndex--;
            LoadData();
        }

        private void btnGoPage_Click(object sender, RoutedEventArgs e)
        {
            if (tbGoToPage.Text=="")
            {
                return;
            }
            else if (Convert.ToInt32(tbGoToPage.Text) > pageCount)
            {
                MessageBox.Show("您输入的值超出范围啦！");
                tbGoToPage.Text = "";
                return;
            }
            pageIndex = Convert.ToInt32(tbGoToPage.Text);
            LoadData();
        }

        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            pageIndex=1;
            LoadData();
        }

        private void btnEedPage_Click(object sender, RoutedEventArgs e)
        {
            pageIndex = pageCount;
            LoadData();
        }

        private void tbGoToPage_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;

            //屏蔽非法按键
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
                return;
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                e.Handled = false;
                return;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void tbGoToPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            //屏蔽中文输入和非法字符粘贴输入
            TextBox textBox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);

            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                double num = 0;
                if (!Double.TryParse(textBox.Text, out num))
                {
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }
            }
        }
    }
}
//<DataGridTextColumn Header="主机编号" Binding="{Binding Account}"></DataGridTextColumn>
//<DataGridTextColumn Header="用户名称"  Binding="{Binding UserName}"></DataGridTextColumn>
//<DataGridTextColumn Header="用户地址"  Binding="{Binding Address}"></DataGridTextColumn>
//<DataGridTextColumn Header="第一联系人"  Binding="{Binding FirstContact}"></DataGridTextColumn>
//<DataGridTextColumn Header="第一联系电话"  Binding="{Binding FirstContactTel}"></DataGridTextColumn>
//<DataGridTextColumn Header="第二联系人" Binding="{Binding SecondContact}"></DataGridTextColumn>
//<DataGridTextColumn Header="第二联系电话"  Binding="{Binding SecondContactTel}"></DataGridTextColumn>
//<DataGridTextColumn Header="应急队员"  Binding="{Binding EmergencyPer}"></DataGridTextColumn>
//<DataGridTextColumn Header="应急电话"  Binding="{Binding EmergencyPerTel}"></DataGridTextColumn>
//<DataGridTextColumn Header="用户电话"  Binding="{Binding UserTel}"></DataGridTextColumn>
//<DataGridTextColumn Header="联网电话" Binding="{Binding NetworkTel}"></DataGridTextColumn>
//<DataGridTextColumn Header="用户类型"  Binding="{Binding UserType}"></DataGridTextColumn>
//<DataGridTextColumn Header="辖区派出所"  Binding="{Binding Police}"></DataGridTextColumn>
//<DataGridTextColumn Header="派出所电话"  Binding="{Binding PoliceTel}"></DataGridTextColumn>
//<DataGridTextColumn Header="第一通知人" Binding="{Binding FirstNotice}"></DataGridTextColumn>
//<DataGridTextColumn Header="第一通知电话"  Binding="{Binding FirstNoticeTel}"></DataGridTextColumn>
//<DataGridTextColumn Header="第二通知人"  Binding="{Binding SecondNotice}"></DataGridTextColumn>
//<DataGridTextColumn Header="第二通知电话"  Binding="{Binding SecondNoticeTel}"></DataGridTextColumn>
//<DataGridTextColumn Header="通讯格式"  Binding="{Binding Protocol}"></DataGridTextColumn>
//<DataGridTextColumn Header="主机型号"  Binding="{Binding PanelName}"></DataGridTextColumn>
//<DataGridTextColumn Header="安装日期"  Binding="{Binding InstallDate}"></DataGridTextColumn>
//<DataGridTextColumn Header="安装单位"  Binding="{Binding InstallCompany}"></DataGridTextColumn>
//<DataGridTextColumn Header="安装人员"  Binding="{Binding Installer}"></DataGridTextColumn>
//<DataGridTextColumn Header="维护电话"  Binding="{Binding MaintenanceTel}"></DataGridTextColumn>
//<DataGridTextColumn Header="资料输入人员"  Binding="{Binding Writer}"></DataGridTextColumn>
//<DataGridTextColumn Header="合同日期"  Binding="{Binding Deadline}"></DataGridTextColumn>
//<DataGridTextColumn Header="收费标准"  Binding="{Binding Charge}"></DataGridTextColumn>
//<DataGridTextColumn Header="收费提示"  Binding="{Binding IsChargeTips}"></DataGridTextColumn>
//<DataGridTextColumn Header="用户状态"  Binding="{Binding StudentNumber}"></DataGridTextColumn>
//<DataGridTextColumn Header="主机状态"  Binding="{Binding PanelStatus}"></DataGridTextColumn>
//<DataGridTextColumn Header="通讯码"  Binding="{Binding SerialData}"></DataGridTextColumn>
//<DataGridTextColumn Header="来电号码"  Binding="{Binding IncomeTelNum}"></DataGridTextColumn>
//<DataGridTextColumn Header="故障状态"  Binding="{Binding Trouble}"></DataGridTextColumn>
//<DataGridTextColumn Header="最后一次报告时间"  Binding="{Binding LastTime}"></DataGridTextColumn>
//<DataGridCheckBoxColumn Header="停止使用" Binding="{Binding IsStop}"></DataGridCheckBoxColumn>
//<DataGridTextColumn Header="备注"  Binding="{Binding Mark}"></DataGridTextColumn>