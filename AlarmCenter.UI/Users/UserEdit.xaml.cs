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
using AlarmCenter.UI.Zones;
using AlarmCenter.UI.ContactsGroup;

namespace AlarmCenter.UI.Users
{
    /// <summary>
    /// UserEdit.xaml 的交互逻辑
    /// </summary>
    public partial class UserEdit : Window
    {
        //是新增数据还是修改数据
        public bool IsInsert { get; set; }

        //如果是编辑的话被编辑行的ID
        public int EditingID { get; set; }

        public UserEdit()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsInsert)
            {
                //grid.DataContext指向的User对象
                //用户修改过程中，数据绑定会自动把界面的修改同步到User对象中
                User user = (User)gridBasic.DataContext;

                if (cbUserTpye.SelectedItem != null)
                    user.UserType = cbUserTpye.SelectedValue.ToString();
                if (cbProtocol.SelectedItem != null)
                    user.Protocol = cbProtocol.SelectedValue.ToString();
                if (cbPanelName.SelectedItem != null)
                    user.PanelName = cbPanelName.SelectedValue.ToString();

                try
                {
                    new UserDAL().Insert(user);//插入数据库
                }
                catch
                {
                    MessageBox.Show("主机编号不正确或者重复，请重新输入不超过6位数的主机编号！ *_* ");
                    return;
                }
            }
            else
            {
                //先从数据库中查询旧的值，然后把界面中的值设置到旧对象上
                //Update更新
                User currentUser = (User)gridBasic.DataContext;

                if (cbUserTpye.SelectedItem != null)
                    currentUser.UserType = cbUserTpye.SelectedValue.ToString();
                if (cbProtocol.SelectedItem != null)
                    currentUser.Protocol = cbProtocol.SelectedValue.ToString();
                if (cbPanelName.SelectedItem != null)
                    currentUser.PanelName = cbPanelName.SelectedValue.ToString();
                
                
                try
                {
                    //第一步：获取原来的主机编号
                    UserDAL userDAL = new UserDAL();
                    User sourceUser = userDAL.GetByID(EditingID);

                    //第二步：尝试更新主机编号
                    userDAL.Update(currentUser); ;//更新数据库

                    //第三步：如果更新主机编号成功，则更新该用户下面所有防区和联系人的主机编号字段
                    //防区
                    ZoneDAL zoneDAL = new ZoneDAL();
                    Zone[] zoneArray=zoneDAL.GetZonesArray(sourceUser.Account);
                    foreach(Zone zone in zoneArray)
                    {
                        zone.Account = currentUser.Account;
                        zoneDAL.Update(zone);
                    }
                    //联系人
                    ContactsDAL contactsDAL = new ContactsDAL();
                    Contacts[] contactsArray = contactsDAL.GetContactsArray(sourceUser.Account);
                    foreach (Contacts contacts in contactsArray)
                    {
                        contacts.Account = currentUser.Account;
                        contactsDAL.Update(contacts);
                    }
                }
                catch
                {
                    MessageBox.Show("主机编号不正确或者重复，请重新输入不超过6位数的主机编号！ *_* ");
                    return;
                }
            }
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //窗口打开的时候让第一个输入控件获得焦点
            //txtName.Focus();
            ComboBoxLoad();
            if (IsInsert)
            {
                User user = new User();
                gridBasic.DataContext = user;
            }
            else//修改
            {
                User user = new UserDAL().GetByID(EditingID);
                gridBasic.DataContext = user;
                cbUserTpye.SelectedValue = user.UserType;
                cbProtocol.SelectedValue = user.Protocol;
                cbPanelName.SelectedValue = user.PanelName;
                
                LoadZoneData();
                LoadContactsData();
            }
            
        }
        private void ComboBoxLoad()
        {
            GroupDAL groupDAL = new GroupDAL();
            //用户类型
            cbUserTpye.ItemsSource = groupDAL.GetGroupItems("用户类型").DefaultView;
            cbUserTpye.DisplayMemberPath = groupDAL.GetGroupItems("用户类型").Columns["子组"].ToString();
            cbUserTpye.SelectedValuePath = groupDAL.GetGroupItems("用户类型").Columns["子组"].ToString();
            //通讯格式
            cbProtocol.ItemsSource = groupDAL.GetGroupItems("通讯格式").DefaultView;
            cbProtocol.DisplayMemberPath = groupDAL.GetGroupItems("通讯格式").Columns["子组"].ToString();
            cbProtocol.SelectedValuePath = groupDAL.GetGroupItems("通讯格式").Columns["子组"].ToString();
            //主机型号
            cbPanelName.ItemsSource = groupDAL.GetGroupItems("主机类型").DefaultView;
            cbPanelName.DisplayMemberPath = groupDAL.GetGroupItems("主机类型").Columns["子组"].ToString();
            cbPanelName.SelectedValuePath = groupDAL.GetGroupItems("主机类型").Columns["子组"].ToString();

            //ComboBox1.ItemsSource = ds.Tables[0].DefaultView;
            //cbUserTpye.DisplayMemberPath = ds.Tables[0].Columns["Displayvalue"].ToString();
            //cbUserTpye.SelectedValuePath = ds.Tables[0].Columns["Datavalue"].ToString(); 
        }
        private void LoadZoneData()
        {
            ZoneDAL zoneDAL = new ZoneDAL();
            User user = new UserDAL().GetByID(EditingID);
            dataGridZone.ItemsSource = zoneDAL.GetZonesArray(user.Account);
        }
        private void LoadContactsData()
        {
            ContactsDAL contactsDAL = new ContactsDAL();
            User user = new UserDAL().GetByID(EditingID);
            dataGridContacts.ItemsSource = contactsDAL.GetContactsArray(user.Account);
        }
        private void btnAddZone_Click(object sender, RoutedEventArgs e)
        {
            ZoneEdit zoneEdit = new ZoneEdit();
            User user = new UserDAL().GetByID(EditingID);
            zoneEdit.IsInsert = true;
            zoneEdit.account = user.Account;
            if (zoneEdit.ShowDialog() == true)
            {
                LoadZoneData();
            }
        }

        private void btnEditZone_Click(object sender, RoutedEventArgs e)
        {
            Zone zone = (Zone)dataGridZone.SelectedItem;
            if (zone == null)
            {
                MessageBox.Show("请选择要编辑的行！");
                return;
            }
            ZoneEdit zoneEdit = new ZoneEdit();
            zoneEdit.IsInsert = false;
            zoneEdit.zoneID = zone.ID;
            if (zoneEdit.ShowDialog() == true)
            {
                LoadZoneData();
            }
        }
        private void dataGridZone_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Zone zone = (Zone)dataGridZone.SelectedItem;
            if (zone == null)
            {
                MessageBox.Show("请选择要编辑的行！");
                return;
            }
            ZoneEdit zoneEdit = new ZoneEdit();
            zoneEdit.IsInsert = false;
            zoneEdit.zoneID = zone.ID;
            if (zoneEdit.ShowDialog() == true)
            {
                LoadZoneData();
            }
        }
        private void btnDeleteZone_Click(object sender, RoutedEventArgs e)
        {
            Zone zone = (Zone)dataGridZone.SelectedItem;
            if (zone == null)
            {
                MessageBox.Show("请选择要删除的行！");
                return;
            }

            if (MessageBox.Show("确认删除此条数据吗？", "提醒",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                new ZoneDAL().DeleteByID(zone.ID);

                LoadZoneData();//刷新数据
            }
        }
        private void btnAddContacts_Click(object sender, RoutedEventArgs e)
        {
            ContactsEdit contactsEdit = new ContactsEdit();
            User user = new UserDAL().GetByID(EditingID);
            contactsEdit.IsInsert = true;
            contactsEdit.account = user.Account;
            if (contactsEdit.ShowDialog() == true)
            {
                LoadContactsData();
            }
        }

        private void btnEditContacts_Click(object sender, RoutedEventArgs e)
        {
            Contacts contacts = (Contacts)dataGridContacts.SelectedItem;
            if (contacts == null)
            {
                MessageBox.Show("请选择要编辑的行！");
                return;
            }
            ContactsEdit contactsEdit = new ContactsEdit();
            contactsEdit.IsInsert = false;
            contactsEdit.contactsID = contacts.ID;
            if (contactsEdit.ShowDialog() == true)
            {
                LoadContactsData();
            }
        }

        private void btnDeleteContacts_Click(object sender, RoutedEventArgs e)
        {
            Contacts contacts = (Contacts)dataGridContacts.SelectedItem;
            if (contacts == null)
            {
                MessageBox.Show("请选择要删除的行！");
                return;
            }

            if (MessageBox.Show("确认删除此条数据吗？", "提醒",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                new ContactsDAL().DeleteByID(contacts.ID);

                LoadContactsData();//刷新数据
            }
        }
        private void tbAccount_TextChanged(object sender, TextChangedEventArgs e)
        {
            //屏蔽中文输入和非法字符粘贴输入
            TextBox textBox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];//建立一个长度为更改字符长度的数组。
            e.Changes.CopyTo(change, 0);//把更改后的字符串放到数组里面，从第0个索引放。

            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                double num = 0;
                if (!Double.TryParse(textBox.Text, out num))//如果转换为双精度成功则返回双精度值，否则返回num。
                {
                    MessageBox.Show("shurubudui");
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }
            }
        }

        private void tbAccount_PreviewKeyDown(object sender, KeyEventArgs e)
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
            else if (e.Key == Key.Back)
            {
                e.Handled = false;
                return;
            }
            else
            {
                e.Handled = true;
                return;
            }
        }

    }
}
