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

namespace AlarmCenter.UI.ContactsGroup
{
    /// <summary>
    /// ContactsEdit.xaml 的交互逻辑
    /// </summary>
    public partial class ContactsEdit : Window
    {
        //是新增数据还是修改数据
        public bool IsInsert { get; set; }

        //如果是编辑的话被编辑行的ID
        public int contactsID { get; set; }
        public string account{get;set;}

        public ContactsEdit()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 50;i++ )
                cbContactsID.Items.Add((i+1).ToString());

            if (IsInsert)
            {
                Contacts contacts = new Contacts();
                gridContacts.DataContext = contacts;
            }
            else//修改
            {
                Contacts contacts = new ContactsDAL().GetContactsByID(contactsID);
                gridContacts.DataContext = contacts;

                cbContactsID.SelectedItem = contacts.ContactsID;
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsInsert)
            {
                //grid.DataContext指向的User对象
                //用户修改过程中，数据绑定会自动把界面的修改同步到User对象中
                Contacts contacts = (Contacts)gridContacts.DataContext;
                contacts.Account = account;
                
                if (cbContactsID.SelectedItem == null)
                {
                    MessageBox.Show("请选择编号！ —_—");
                    return;
                }
                else
                {
                    contacts.ContactsID = cbContactsID.SelectedItem.ToString();
                }
                if (tbContactsName.Text == "")
                {
                    MessageBox.Show("请输入姓名！ —_—");
                    return;
                }

                try
                {
                    new ContactsDAL().Insert(contacts);//插入数据库
                }
                catch
                {
                    MessageBox.Show("联系人编号不能重复！请选择其他编号 *_* ");
                    return;
                }
            }
            else
            {
                //先从数据库中查询旧的值，然后把界面中的值设置到旧对象上
                //Update更新
                Contacts contacts = (Contacts)gridContacts.DataContext;
                if (cbContactsID.SelectedItem == null)
                {
                    MessageBox.Show("请选择编号！ —_—");
                    return;
                }
                if (tbContactsName.Text == "")
                {
                    MessageBox.Show("请输入姓名！ —_—");
                    return;
                }
                ContactsDAL dal = new ContactsDAL();
                dal.Update(contacts);
            }
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
