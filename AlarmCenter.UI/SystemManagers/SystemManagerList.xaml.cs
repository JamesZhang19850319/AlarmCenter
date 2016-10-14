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
using AlarmCenter.DAL;

namespace AlarmCenter.UI.SystemManagers
{
    /// <summary>
    /// SystemManagerListUI.xaml 的交互逻辑
    /// </summary>
    public partial class SystemManagerList : Window
    {
        public SystemManagerList()
        {
            InitializeComponent();
        }


        private void LoadData()
        {
            SystemManagerDAL dal = new SystemManagerDAL();
            gridSystemManager.ItemsSource = dal.ListAll();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
