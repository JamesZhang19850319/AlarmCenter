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
using System.Data;
using AlarmCenter.DAL;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AlarmCenter.UI.Reports
{
    /// <summary>
    /// ReportWindowRDLC.xaml 的交互逻辑
    /// </summary>
    public partial class ReportWindowRDLC : Window
    {
        public DataTable table { get; set; }
        public static  string reportName { get; set; }
        //private DataSet m_dataSet;
        public ReportWindowRDLC()
        {
            InitializeComponent();
            //_reportViewer.Load += ReportViewer_Load;
        }
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dtx = new DataTable();

            if (!_isReportViewerLoaded)
            {
                //ReportDataSource reportDataSource1 = new ReportDataSource();
                //dtx = Dtx(table);
                //reportDataSource1.Name = "MyDataSet"; //Name of the report dataset in our .RDLC file
                //reportDataSource1.Value = dtx;
                //this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                //this._reportViewer.LocalReport.ReportPath = "../../Reports/UserReport.rdlc";
                //ReportParameter[] param = new ReportParameter[9];
                //List<string> columnName = new List<string>(8);
                //foreach (DataColumn c in table.Columns)
                //{
                //    columnName.Add(c.ColumnName);
                //}
                //for (int i = 0; i < 8 - table.Columns.Count; i++)
                //{
                //    columnName.Add(" ");
                //}
                //param[0] = new ReportParameter("ReportParameter1", columnName[0]);
                //param[1] = new ReportParameter("ReportParameter2", columnName[1]);
                //param[2] = new ReportParameter("ReportParameter3", columnName[2]);
                //param[3] = new ReportParameter("ReportParameter4", columnName[3]);
                //param[4] = new ReportParameter("ReportParameter5", columnName[4]);
                //param[5] = new ReportParameter("ReportParameter6", columnName[5]);
                //param[6] = new ReportParameter("ReportParameter7", columnName[6]);
                //param[7] = new ReportParameter("ReportParameter8", columnName[7]);
                //param[8] = new ReportParameter("ReportParameter9", reportName);
                //_reportViewer.LocalReport.SetParameters(param);
                //_reportViewer.RefreshReport();

                //_isReportViewerLoaded = true;
            }
        }

        private bool _isReportViewerLoaded;
        public DataTable Dtx(DataTable dt)
        {
            MyDataSet.DataTable1DataTable dtx1 = new MyDataSet.DataTable1DataTable();
            object[] obj = new object[dt.Columns.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtx1.Rows.Add(dtx1.NewRow());

                for (int j = 0; j < dt.Columns.Count ; j++)
                {
                    dtx1.Rows[i][j] = dt.Rows[i][j].ToString();
                }
            }
            return dtx1;
        }

        //private bool _isReportViewerLoaded;

        //public DataTable Dtx(DataSet dt)
        //{
        //    DataSet1.DataTable1DataTable dtx1 = new DataSet1.DataTable1DataTable();
        //    //DataTable tc = new DataTable();
        //    //int[] obj = new int[dt.Columns.Count];
        //    //特别注意：所选择的表的列的数目需<=Bigtable的字段数目
        //    //请自行填写保护代码
        //    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
        //    {
        //        dtx1.Rows.Add(dtx1.NewRow());

        //        for (int j = 0; j < 7; j++)
        //        {
        //            dtx1.Rows[i][j] = dt.Tables[0].Rows[i][j].ToString();
        //        }
        //    }
        //    return dtx1;
        //}
        //private void ShowReport()
        //{

        //    m_dataSet.DataSetName="MyData";
        //    this._reportViewer.Reset();
            
        //    //param[1] = new ReportParameter("ReportParameter2", "txtParameter.Text2");
        //    //this._reportViewer.LocalReport.LoadReportDefinition(m_rdl);
        //    this._reportViewer.LocalReport.DataSources.Add(new ReportDataSource("MyData", m_dataSet.Tables[0]));
        //    this._reportViewer.LocalReport.ReportPath = "../../Reports/Report1.rdlc";
        //    ReportParameter[] param = new ReportParameter[1];
        //    param[0] = new ReportParameter("ReportParameter1", "txtParameter.Text1");
        //    _reportViewer.LocalReport.SetParameters(param);
        //    this._reportViewer.RefreshReport();
        //}
        //private void BindReportViewer()
        //{
        //    ReportViewer1.Visible = true;
               
        //    //Invoke Stored procedure With Input parameter to it.
        //    //DataSet dsReport = objSP.GetTable(storedProcedure,txtParameter.Text));
        //    //Hardcoded Values.
        //    IList >Customer< customerList = new List>Customer<();
        //    customerList.Add(new Customer(1,"Santosh Poojari"));
        //    customerList.Add(new Customer(2, "Santosh Poojari1"));
        //    customerList.Add(new Customer(3, "Santosh Poojari2"));
       
        //    ReportParameter[] param = new ReportParameter[1];
        //    param[0] = new ReportParameter("Report_Parameter_0",txtParameter.Text);
        //    ReportViewer1.LocalReport.SetParameters(param);

        //    ReportDataSource rds = new ReportDataSource
        //        ("DataSet1_Customers_DataTable1", customerList);
        //    ReportViewer1.LocalReport.DataSources.Clear();
        //    ReportViewer1.LocalReport.DataSources.Add(rds);
        //    ReportViewer1.LocalReport.Refresh();
        //}
        //private void ReportViewer_Load(object sender, EventArgs e)
        //{
        //    m_dataSet = new DataSet();
        //    DataTable dtx = new DataTable();
        //    m_dataSet = OleDbHelper.ExecuteDataSet("select * from [用户资料]");
        //    //ShowReport();

        //    //if (!_isReportViewerLoaded)
        //    //{
        //    //    ReportDataSource reportDataSource1 = new ReportDataSource();

        //    //    dtx = Dtx(m_dataSet);
        //    //    reportDataSource1.Name = "dataset"; //Name of the report dataset in our .RDLC file
        //    //    reportDataSource1.Value = dtx;
        //    //    this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
        //    //    this._reportViewer.LocalReport.ReportPath = "../../Reports/Report1.rdlc";

        //    //    _reportViewer.RefreshReport();

        //    //    _isReportViewerLoaded = true;
        //    //}
        //}
        /// <summary>
        /// 序列化到内存流
        /// </summary>
        /// <returns></returns>
        //private Stream GetRdlcStream(XmlDocument xmlDoc)
        //{
        //    Stream ms = new MemoryStream();
        //    XmlSerializer serializer = new XmlSerializer(typeof(XmlDocument));
        //    serializer.Serialize(ms, xmlDoc);

        //    ms.Position = 0;
        //    return ms;
        //}

        ///// <summary>
        ///// 修改RDLC文件
        ///// </summary>
        ///// <returns></returns>
        //private XmlDocument ModifyRdlc()
        //{
        //    XmlDocument xmlDoc = new XmlDocument();

        //    //xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "FirstRdlc.rdlc");
        //    xmlDoc.Load("../../Reports/Report1.rdlc");

        //    //添加Field节点
        //    XmlNodeList fileds = xmlDoc.GetElementsByTagName("Fields");

        //    XmlNode filedNode = fileds.Item(0).FirstChild.CloneNode(true);
        //    filedNode.Attributes["Name"].Value = "GPA";
        //    filedNode.FirstChild.InnerText = "GPA";
        //    fileds.Item(0).AppendChild(filedNode);

        //    //添加TablixColumn

        //    XmlNodeList tablixColumns = xmlDoc.GetElementsByTagName("TablixColumns");
        //    XmlNode tablixColumn = tablixColumns.Item(0).FirstChild;
        //    XmlNode newtablixColumn = tablixColumn.CloneNode(true);
        //    tablixColumns.Item(0).AppendChild(newtablixColumn);

        //    //TablixMember
        //    XmlNodeList tablixMembers = xmlDoc.GetElementsByTagName("TablixColumnHierarchy");

        //    XmlNode tablixMember = tablixMembers.Item(0).FirstChild.FirstChild;
        //    XmlNode newTablixMember = tablixMember.CloneNode(true);
        //    tablixMembers.Item(0).FirstChild.AppendChild(newTablixMember);

        //    XmlNodeList tablixRows = xmlDoc.GetElementsByTagName("TablixRows");

        //    //TablixRows1
        //    var tablixRowsRowCells1 = tablixRows.Item(0).FirstChild.ChildNodes[1];
        //    XmlNode tablixRowCell1 = tablixRowsRowCells1.FirstChild;
        //    XmlNode newtablixRowCell1 = tablixRowCell1.CloneNode(true);
        //    var textBox1 = newtablixRowCell1.FirstChild.ChildNodes[0];
        //    textBox1.Attributes["Name"].Value = "GPA1";

        //    var paragraphs = textBox1.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "Paragraphs").FirstOrDefault();
        //    paragraphs.FirstChild.FirstChild.FirstChild.FirstChild.InnerText = "GPA";
        //    var defaultName1 = textBox1.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "rd:DefaultName").FirstOrDefault().InnerText = "GPA1";

        //    tablixRowsRowCells1.AppendChild(newtablixRowCell1);

        //    //TablixRows2
        //    var tablixRowsRowCells2 = tablixRows.Item(0).ChildNodes[1].ChildNodes[1];
        //    XmlNode tablixRowCell2 = tablixRowsRowCells2.FirstChild;
        //    XmlNode newtablixRowCell2 = tablixRowCell2.CloneNode(true);
        //    var textBox2 = newtablixRowCell2.FirstChild.ChildNodes[0];
        //    textBox2.Attributes["Name"].Value = "GPA";

        //    var paragraphs2 = textBox2.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "Paragraphs").FirstOrDefault();
        //    paragraphs2.FirstChild.FirstChild.FirstChild.FirstChild.InnerText = "=Fields!GPA.Value";
        //    var defaultName2 = textBox2.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "rd:DefaultName").FirstOrDefault().InnerText = "GPA";

        //    tablixRowsRowCells2.AppendChild(newtablixRowCell2);

        //    xmlDoc.Save(AppDomain.CurrentDomain.BaseDirectory + "FirstRdlc1.rdlc");
        //    return xmlDoc;
        //}

        ///// <summary>
        ///// 修改xsd文件
        ///// </summary>
        //private void ModifyXSD()
        //{
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "Students.xsd");

        //    XmlNodeList nodeList = xmlDoc.GetElementsByTagName("xs:sequence");
        //    XmlNode node = nodeList.Item(0).FirstChild.CloneNode(true);
        //    node.Attributes["name"].Value = "GPA";
        //    node.Attributes["msprop:Generator_ColumnVarNameInTable"].Value = "columnGPA";
        //    node.Attributes["msprop:Generator_ColumnPropNameInRow"].Value = "GPA";
        //    node.Attributes["msprop:Generator_ColumnPropNameInTable"].Value = "GPAColumn";
        //    node.Attributes["msprop:Generator_UserColumnName"].Value = "GPA";
        //    nodeList.Item(0).AppendChild(node);

        //    xmlDoc.Save(AppDomain.CurrentDomain.BaseDirectory + "Students.xsd");
        //}
  //  在做报表项目时,有时不仅要满足用户的需求,而且要求软件要有一定的适应性,这就往往就要求报表要动态地生成. 
  //  最近在做一个项目时,就碰到这样子的问题.系统要在不现的地区使用,不同的地区下属单位是不一样的,而报表中有列就是显示下属单位的某一属性的.因而报表中的这一项不公名称不能确定,数目也不能定.所以它要求在生成过程中动态地改变报表中的格式.
  //  这个项目是用.NET的C#开发的,报表用微软的RDLC模板报表.
  //  RDLC 是用XML文档定义报表格式,这样就很容易想到通过更改XML文档元素内容来实现这一需求.
  // 报表中实现了多列表头以及向报表中动态添加列增加参数据等内容.
  // 使用方法：

  //  private void ProressAllXiangZhen()
  //      {　

  //          string sql = null;
  //              string filename = Application.StartupPath + "//ChecksMonthAll.rdlc";//报表定义文件地址
  //                            ReportXml Rex = new ReportXml(destname);//创建XML操作对象
  //              XiangZhenEx xz = new XiangZhenEx();//强类型数据集
  //              xz = (new XiangZhenBL()).GetAllXiangZhen();             

  ////根据数据表值，修改报表格式

  //              foreach (DataRow row in xz._XiangZhen.Rows)//每镇增加两列当月所得及年累计值
  //              {
                  

  //                  string code = row["code"].ToString();
  //                  string name = row["name"].ToString();

  //                  //系统提示
  //                  UpStatus(string.Format("设置乡镇{0}报表格式...", name));
  //                  //增加参数
  //                  Rex.AddParamter("Params" + code, "String", name);

  //                  //增加字段
  //                  Rex.AddDataSetColumn("Month_" + code + "Region", "System.Decimal");
  //                  Rex.AddTableColumn("2.5cm");
  //                  Rex.AddDataSetColumn("Year_" + code + "Region", "System.Decimal");
  //                  Rex.AddTableColumn("2.5cm");
  //                  //增加表头第一行，跨列居中
  //                  Rex.AddTableHaderFirstRowCell("Heade" + code + "01", String.Format("=Parameters!{0}.Value", "Params" + code));
  //                  //增加表头第二行，加两列与上对齐
  //                  Rex.AddTableHaderSecondRowCell("Heade" + code + "0101", "当月");
  //                  Rex.AddTableHaderSecondRowCell("Heade" + code + "0102", "全年");
  //                  //增加详细数据行列
  //                  Rex.AddDetailsCell("Detail" + code + "01", String.Format("=Fields!{0}.Value", "Month_" + code + "Region"));
  //                  Rex.AddDetailsCell("Detail" + code + "02", String.Format("=Fields!{0}.Value", "Year_" + code + "Region"));
  //                  sql += "," + String.Format("dbo.Get_Month_Region('{0}',{1},{2},shui_code,{3}) as /"{4}/"", code, m_year, m_month, m_item2, "Month_" + code + "Region");
  //                  sql += "," + String.Format("dbo.Get_Year_Region('{0}',{1},{2},shui_code,{3}) as /"{4}/"", code, m_year, m_month, m_item2, "Year_" + code + "Region");
  //                  if (row["name"].ToString() == "市直" || row["name"].ToString() == "县直") AddXiangZhenHeJi(Rex, xz._XiangZhen.Rows);
  //              }
  //              UpStatus("调整报表宽度...");
  //              Int32 width = (xz._XiangZhen.Rows.Count+1) * 5;
  //              Rex.EditPageHeaderTb28Width(width);
  //              Rex.EditPageHeaderTb29Left(width);
  //              Rex.EditPageFooterLineWidth("line1", width);
  //              Rex.EditPageFooterLineWidth("line2", width);
  //              sql = string.Format("select acctname,sum(money)money,bili,sum(country_get)country_get,"
  //                          + "sum(province_get)province_get,sum(region_get)region_get,"
  //                          + "shui_code " + sql + " from  v_report_month_allzhen  where "
  //                           + "year={0} and month={1} and item IN({2})"
  //                             + " group by shui_code,bili,acctname", m_year, m_month, m_item);
  //              System.Diagnostics.Debug.WriteLine(sql);
  //              UpStatus(string.Format("正在取此数据，大概需要2-3分钟"));
  //              ReportBl BL = new ReportBl();
  //              DataSet re = BL.GetMonthReport(sql);
  //              UpStatus("已经取得数据，正在加载报表数据...");
  //              this.reportViewer.ProcessingMode = ProcessingMode.Local;
  //              this.reportViewer.LocalReport.DataSources.Clear();

  //              Microsoft.Reporting.WinForms.ReportDataSource RD = new Microsoft.Reporting.WinForms.ReportDataSource("ReportDT_v_report_month", re.Tables[0]);
  //              this.reportViewer.LocalReport.DataSources.Add(RD);

  //              ReportParameter[] cc = new ReportParameter[xz._XiangZhen.Rows.Count + 4];
  //              for (int i = 0; i < xz._XiangZhen.Rows.Count; i++)//每镇增加两列当月所得及年累计值
  //              {
  //                  DataRow row = xz._XiangZhen.Rows[i];
  //                  string code = row["code"].ToString();
  //                  string name = row["name"].ToString();
  //                  cc[i] = new ReportParameter("Params" + code, name);
  //              }
  //              UpStatus("设置报表参数");
  //              this.reportViewer.LocalReport.ReportPath = destname;
  //              cc[xz._XiangZhen.Rows.Count] = new ReportParameter("dwmc", "全县");
  //              cc[xz._XiangZhen.Rows.Count + 1] = new ReportParameter("date", string.Format("{0}年{1}月", m_year, m_month));
  //              cc[xz._XiangZhen.Rows.Count + 2] = new ReportParameter("shou_item", m_item.Replace('/'', ' ').Replace(',', '、'));
  //              cc[xz._XiangZhen.Rows.Count + 3] = new ReportParameter("Params100", "乡镇小计");
  //              this.reportViewer.LocalReport.SetParameters(cc);
  //              UpStatus("就绪");
  //              this.reportViewer.RefreshReport();
             
       
  //      }

  //类代码示例(报表操作类ReportXML源码):

  //public ReportXml(string filename)
  //      {
  //          this.filename = filename;
  //          doc = new XmlDocument();           
  //          doc.Load(filename);           
  //          root = doc.DocumentElement;
  //          xnm=new XmlNamespaceManager(doc.NameTable);
  //          xnm.AddNamespace("rd", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner");
  //          xnm.AddNamespace("default", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
  //          xnm.PushScope();
  //          xpathdoc = new XPathDocument(filename);
  //          xnv = xpathdoc.CreateNavigator();
           
  //      }
  //      private XmlNode CreateNode( string nodename, string innertext)
  //      {

  //          XmlNode node = null;
  //          node = doc.CreateNode(XmlNodeType.Element, nodename, "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
  //          node.InnerText = innertext;
  //          return node;
  //      }

  //private XmlNode CreateNode(string nodename)
  //      {

  //          XmlNode node = null;
  //          node = doc.CreateNode(XmlNodeType.Element, nodename, "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
  //          return node;
  //      }
  //      public void AddParamter(string name, string type, string prompt)
  //      {

  //          XmlNode node = null;
  //          XmlNode refCd = root.SelectSingleNode("//default:ReportParameters", xnm);
  //          XmlElement docFrag = doc.CreateElement("ReportParameter", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
  //          docFrag.SetAttribute("Name",name);
  //          node = doc.CreateNode(XmlNodeType.Element, "DataType", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
  //          node.InnerText = type;
  //          docFrag.AppendChild(node);
  //          node = doc.CreateNode(XmlNodeType.Element, "Nullable", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
  //          node.InnerText = "true";
  //          docFrag.AppendChild(node);
  //          node = doc.CreateNode(XmlNodeType.Element, "Prompt", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
  //          node.InnerText = prompt;
  //          docFrag.AppendChild(node);           

  //          refCd.InsertAfter(docFrag, refCd.LastChild);

  //          doc.Save(filename);    
       
  //      }
    }
}
