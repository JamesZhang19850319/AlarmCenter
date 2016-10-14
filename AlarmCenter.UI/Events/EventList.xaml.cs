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

namespace AlarmCenter.UI.Events
{
    /// <summary>
    /// UserList.xaml 的交互逻辑
    /// </summary>
    public partial class EventList : Window
    {
        public int pageIndex = 1;
        public int pageCount;
        public const int pageSize = 40;

        public EventList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            EventDAL dal = new EventDAL();
            grid.ItemsSource = dal.GetEventsArray("Event", "报警事件");
            pageCount = dal.pageCount;
            tbAllPage.Text = pageCount.ToString();
            tbPageSize.Text = pageSize.ToString();
            tbPageIndex.Text = pageIndex.ToString();
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


        /// <summary>
        /// 限制非法字符输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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