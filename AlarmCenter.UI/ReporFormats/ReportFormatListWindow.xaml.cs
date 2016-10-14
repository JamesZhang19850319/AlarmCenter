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
    /// ReportFormatListWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReportFormatListWindow : Window
    {
        private string cid_DRC = "DRC_CONTACTID";
        private string cid_Ademco = "ADEMCO_CONTACTID";

        public ReportFormatListWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDRC_CIDData();
            LoadADEMCO_CIDData();
        }
        private void LoadDRC_CIDData()
        {
            //显示DRC_CONTACTID
            CIDDAL dal = new CIDDAL();
            dgDRC_CONTACTID.ItemsSource = dal.GetReportFormatArray(cid_DRC);
        }
        private void LoadADEMCO_CIDData()
        {
            //显示DRC_CONTACTID
            CIDDAL dal = new CIDDAL();
            dgADEMCO_CONTACTID.ItemsSource = dal.GetReportFormatArray(cid_Ademco);
        }

        private void btnDRC_CONTACTIDAdd_Click(object sender, RoutedEventArgs e)
        {
            ReportFormatEditWindow reportFormatEditWindow = new ReportFormatEditWindow();
            reportFormatEditWindow.IsInsert = true;
            reportFormatEditWindow.tableName = cid_DRC;
            if (reportFormatEditWindow.ShowDialog() == true)
            {
                LoadDRC_CIDData();
            }
        }

        private void btnDRC_CONTACTIDEdit_Click(object sender, RoutedEventArgs e)
        {
            CID cid = (CID)dgDRC_CONTACTID.SelectedItem;

            if (cid == null)
            {
                MessageBox.Show("请选择要编辑的行！");
                return;
            }

            ReportFormatEditWindow reportFormatEditWindow = new ReportFormatEditWindow();
            reportFormatEditWindow.IsInsert = false;
            reportFormatEditWindow.tableName = cid_DRC;
            reportFormatEditWindow.cidID = cid.ID;
            if (reportFormatEditWindow.ShowDialog() == true)
            {
                LoadDRC_CIDData();
            }
        }
        private void btnDRC_CONTACTIDDelete_Click(object sender, RoutedEventArgs e)
        {
            CID cid = (CID)dgDRC_CONTACTID.SelectedItem;
            if (cid == null)
            {
                MessageBox.Show("请选择要删除的行！");
                return;
            }
            if (MessageBox.Show("确认删除此条数据吗？", "提醒",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                new CIDDAL().DeleteByID(cid_DRC, cid.ID);

                LoadDRC_CIDData();//刷新数据
            }
        }

        private void dgDRC_CONTACTID_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CID cid = (CID)dgDRC_CONTACTID.SelectedItem;

            if (cid == null)
            {
                MessageBox.Show("请选择要编辑的行！");
                return;
            }

            ReportFormatEditWindow reportFormatEditWindow = new ReportFormatEditWindow();
            reportFormatEditWindow.IsInsert = false;
            reportFormatEditWindow.tableName = cid_DRC;
            reportFormatEditWindow.cidID = cid.ID;
            if (reportFormatEditWindow.ShowDialog() == true)
            {
                LoadDRC_CIDData();
            }
        }

        private void btnADEMCO_CONTACTIDAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnADEMCO_CONTACTIDEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnADEMCO_CONTACTIDDelete_Click(object sender, RoutedEventArgs e)
        {

        }
        private void dgADEMCO_CONTACTID_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

    }
}
