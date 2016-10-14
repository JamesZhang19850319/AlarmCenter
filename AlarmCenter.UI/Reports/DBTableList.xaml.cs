using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

namespace AlarmCenter.UI.Reports
{
    /// <summary>
    /// DBTableList.xaml 的交互逻辑
    /// </summary>
    public partial class DBTableList : Window
    {
        ArrayList myDataList,mydt;
        string currentItemText;
        int  currentItemIndex;
        public DataTable dataTable{get;set;}
        public DBTableList()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            myDataList = LoadListBoxData(dataTable);
            mydt = myDataList;
            LeftListBox.ItemsSource = myDataList;
            // Get data from somewhere and fill in my local ArrayList
            //ArrayList myDataList = LoadListBoxData(table);
            // Bind ArrayList with the ListBox
            //LeftListBox.ItemsSource = myDataList;
        }
        private DataTable SelectTable(DataTable table, ListBox lb)
        {
            List<DataColumn> list = new List<DataColumn>();
            foreach (DataColumn dc in table.Columns)
            {
                if (!lb.Items.Contains(dc.ColumnName))
                {
                    list.Add(dc);
                }
            }
            foreach (DataColumn c in list)
            {
                table.Columns.Remove(c);
            }
            return table;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (LeftListBox.SelectedItem == null)
            {
                MessageBox.Show("请选择一项");
                return;
            }
            if (RightListBox.Items.Count >= 8)
            {
                MessageBox.Show("您最多只能选择8个项");
                return;
            }
            // Find the right item and it's value and index
            currentItemText = LeftListBox.SelectedValue.ToString();
            currentItemIndex = LeftListBox.SelectedIndex;
            RightListBox.Items.Add(currentItemText);

            if (myDataList != null)
            {
                myDataList.RemoveAt(currentItemIndex);
            }

            // Refresh data binding
            ApplyDataBinding();
        }
        private ArrayList LoadListBoxData(DataTable table)
        {
            ArrayList itemsList = new ArrayList();
            DataColumnCollection colum = table.Columns;
            for (int i = 0; i < colum.Count; i++)
            {
                itemsList.Add(colum[i].ColumnName);
            }
            return itemsList;
        }
        private void ApplyDataBinding()
        {
            LeftListBox.ItemsSource = null;
            // Bind ArrayList with the ListBox
            LeftListBox.ItemsSource = myDataList;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (RightListBox.SelectedItem == null)
            {
                MessageBox.Show("请选择一行");
                return;
            }
            // Find the right item and it's value and index
            currentItemText = RightListBox.SelectedValue.ToString();
            currentItemIndex = RightListBox.SelectedIndex;
            // Add RightListBox item to the ArrayList
            myDataList.Add(currentItemText);
            RightListBox.Items.RemoveAt(currentItemIndex);
            //RightListBox.Items.RemoveAt(RightListBox.Items.IndexOf(RightListBox.SelectedItem));

            // Refresh data binding
            ApplyDataBinding();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (RightListBox.Items.Count <= 0)
            {
                MessageBox.Show("您没有选择任何内容");
                return;
            }
            dataTable = SelectTable(dataTable, RightListBox);
            ReportWindowRDLC ui = new ReportWindowRDLC();
            ui.table = dataTable;
            this.Close();
            ui.Show();
            
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
