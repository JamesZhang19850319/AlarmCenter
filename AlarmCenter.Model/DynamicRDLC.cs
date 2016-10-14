using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Xml;

/// <summary>
///DynamicRDLC 的摘要说明
/// </summary>
namespace AlarmCenter.DAL
{
    public class DynamicRDLC
    {
        public string filename = "";
        public XmlDocument doc = null;
        public XmlNode root = null;
        public XmlNamespaceManager xnm = null;
        public string ns = "";
        public DynamicRDLC()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public DynamicRDLC(string filename)
        {
            ns = "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition";
            this.filename = filename;
            doc = new XmlDocument();
            doc.Load(filename);
            root = doc.DocumentElement;
            xnm = new XmlNamespaceManager(doc.NameTable);
            xnm.AddNamespace("rd", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner");
            xnm.AddNamespace("default", ns);
            xnm.PushScope();
        }
        private XmlNode CreateNode(string nodename, string innertext)
        {
            XmlNode node = null;
            node = doc.CreateNode(XmlNodeType.Element, nodename, ns);
            node.InnerText = innertext;
            return node;
        }
        private XmlNode CreateNode(string nodename)
        {
            XmlNode node = null;
            node = doc.CreateNode(XmlNodeType.Element, nodename, ns);
            return node;
        }
        public void AddParamter(string name, string type, string prompt)
        {
            XmlNode node = null;
            XmlNode refCd = root.SelectSingleNode("//default:ReportParameters", xnm);
            XmlElement docFrag = doc.CreateElement("ReportParameter", ns);
            docFrag.SetAttribute("Name", name);
            node = doc.CreateNode(XmlNodeType.Element, "DataType", ns);
            node.InnerText = type;
            docFrag.AppendChild(node);
            node = doc.CreateNode(XmlNodeType.Element, "Nullable", ns);
            node.InnerText = "true";
            docFrag.AppendChild(node);
            node = doc.CreateNode(XmlNodeType.Element, "Prompt", ns);
            node.InnerText = prompt;
            docFrag.AppendChild(node);
            refCd.InsertAfter(docFrag, refCd.LastChild);
            doc.Save(filename);

        }



        public void AddTableColumn()
        {
            XmlNode refCd = root.SelectSingleNode("//default:Report//default:Body//default:ReportItems//default:Table//default:TableColumns", xnm);
            XmlNode docFrag = CreateNode("TableColumn");
            XmlNode width = CreateNode("Width", "2.5cm");
            docFrag.AppendChild(width);
            refCd.AppendChild(docFrag);
        }
        //动态增加详细列 
        public void AddDetailsCell(string ColName, string ColValue)
        {
            XmlNode refCd = root.SelectSingleNode("//default:Report//default:Body//default:ReportItems//default:Table//default:Details//default:TableRows//default:TableRow//default:TableCells", xnm);
            XmlElement docFrag = doc.CreateElement("TableCell", ns);
            XmlNode reportitems = CreateNode("ReportItems");
            XmlElement textbox = doc.CreateElement("Textbox", ns);
            textbox.SetAttribute("Name", ColName);
            XmlNode zindex = CreateNode("ZIndex", "20");
            textbox.AppendChild(zindex);
            XmlNode style = CreateNode("Style");
            textbox.AppendChild(style);
            XmlNode borderstyle = CreateNode("BorderStyle");
            style.AppendChild(borderstyle);
            XmlNode defaul = CreateNode("Default", "Solid");
            borderstyle.AppendChild(defaul);
            XmlNode textalign = CreateNode("TextAlign", "Center");
            style.AppendChild(textalign);
            XmlNode PaddingLeft = CreateNode("PaddingLeft", "2pt");
            style.AppendChild(PaddingLeft);
            XmlNode PaddingBottom = CreateNode("PaddingBottom", "2pt");
            style.AppendChild(PaddingBottom);
            XmlNode FontFamily = CreateNode("FontFamily", "宋体");
            style.AppendChild(FontFamily);
            XmlNode VerticalAlign = CreateNode("VerticalAlign", "Middle");
            style.AppendChild(VerticalAlign);
            XmlNode PaddingTop = CreateNode("PaddingTop", "2pt");
            style.AppendChild(PaddingTop);
            XmlNode PaddingRight = CreateNode("PaddingRight", "2pt");
            style.AppendChild(PaddingRight);
            XmlNode cangrow = CreateNode("CanGrow", "true");
            textbox.AppendChild(cangrow);
            XmlNode value = CreateNode("Value", ColValue);
            textbox.AppendChild(value);
            reportitems.AppendChild(textbox);
            docFrag.AppendChild(reportitems);
            refCd.InsertAfter(docFrag, refCd.LastChild);
        }
        //动态增加表头
        public void AddTableHeaderCell(string colname, string paramvalue)
        {
            XmlNode node = null;
            XmlNode refCd = root.SelectSingleNode("//default:Report//default:Body//default:ReportItems//default:Table//default:Header//default:TableRows//default:TableRow//default:TableCells", xnm);
            XmlElement docFrag = doc.CreateElement("TableCell", ns);
            XmlNode reportitems = CreateNode("ReportItems");
            XmlElement textbox = doc.CreateElement("Textbox", ns);
            textbox.SetAttribute("Name", colname);
            XmlNode zindex = CreateNode("ZIndex", "50");
            textbox.AppendChild(zindex);
            XmlNode style = CreateNode("Style");
            XmlNode borderstyle = CreateNode("BorderStyle");
            style.AppendChild(borderstyle);
            XmlNode defaul = CreateNode("Default", "Solid");
            borderstyle.AppendChild(defaul);
            XmlNode textalign = CreateNode("TextAlign", "Center");
            style.AppendChild(textalign);
            XmlNode PaddingLeft = CreateNode("PaddingLeft", "2pt");
            style.AppendChild(PaddingLeft);
            XmlNode PaddingBottom = CreateNode("PaddingBottom", "2pt");
            style.AppendChild(PaddingBottom);
            XmlNode FontFamily = CreateNode("FontFamily", "宋体");
            style.AppendChild(FontFamily);
            //XmlNode FontWeight = CreateNode("FontWeight", "700");
            //style.AppendChild(FontWeight);
            XmlNode VerticalAlign = CreateNode("VerticalAlign", "Bottom");
            style.AppendChild(VerticalAlign);
            XmlNode PaddingTop = CreateNode("PaddingTop", "2pt");
            style.AppendChild(PaddingTop);
            XmlNode PaddingRight = CreateNode("PaddingRight", "2pt");
            style.AppendChild(PaddingRight);
            textbox.AppendChild(style);
            XmlNode cangrow = CreateNode("CanGrow", "true");
            textbox.AppendChild(cangrow);
            XmlNode value = CreateNode("Value", paramvalue);
            textbox.AppendChild(value);
            reportitems.AppendChild(textbox);
            docFrag.AppendChild(reportitems);
            refCd.InsertAfter(docFrag, refCd.LastChild);
        }
        //动态添加页眉
        public void AddTableHaderFirstRowSingleCell(string colname, string paramvalue)
        {
            //XmlNode node = null;
            //XmlNode refCd = root.SelectSingleNode("//default:Report//default:PageHeader", xnm);
            ////XmlElement docFrag = doc.CreateElement("TableCell", ns);
            //node = CreateNode("ReportItems");
            //XmlElement textbox = doc.CreateElement("Textbox", ns);
            //textbox.SetAttribute("Name", colname);
            //node.AppendChild(textbox);
            //XmlNode zindex = CreateNode("ZIndex", "12");
            //textbox.AppendChild(zindex);
            //XmlNode style = CreateNode("Style");
            //XmlNode borderstyle = CreateNode("BorderStyle");
            //style.AppendChild(borderstyle);
            //XmlNode defaul = CreateNode("Default", "Solid");
            //borderstyle.AppendChild(defaul);
            //XmlNode textalign = CreateNode("TextAlign", "Center");
            //style.AppendChild(textalign);
            //XmlNode PaddingLeft = CreateNode("PaddingLeft", "2pt");
            //style.AppendChild(PaddingLeft);
            //XmlNode PaddingBottom = CreateNode("PaddingBottom", "2pt");
            //style.AppendChild(PaddingBottom);
            //XmlNode FontFamily = CreateNode("FontFamily", "宋体");
            //style.AppendChild(FontFamily);
            //XmlNode FontWeight = CreateNode("FontWeight", "700");
            //style.AppendChild(FontWeight);
            //XmlNode VerticalAlign = CreateNode("VerticalAlign", "Bottom");
            //style.AppendChild(VerticalAlign);
            //XmlNode PaddingTop = CreateNode("PaddingTop", "2pt");
            //style.AppendChild(PaddingTop);
            //XmlNode PaddingRight = CreateNode("PaddingRight", "2pt");
            //style.AppendChild(PaddingRight);
            //textbox.AppendChild(style);
            //XmlNode cangrow = CreateNode("CanGrow", "true");
            //textbox.AppendChild(cangrow);
            //XmlNode value = CreateNode("Value", paramvalue);
            //textbox.AppendChild(value);
            ////docFrag.AppendChild(node);
            //refCd.InsertAfter(docFrag, refCd.LastChild);
        }
    }
}
////实现多列表头

////  增加页眉内容

//public void AddTableHaderFirstRowSingleCell(string colname, string paramvalue)
//        {
//            XmlNode node = null;
//            XmlNodeList refCd = root.SelectNodes("//default:Report//default:Body//default:ReportItems//default:Table//default:Header//default:TableRows//default:TableRow//default:TableCells", xnm);
//            XmlElement docFrag = doc.CreateElement("TableCell", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
//            node = CreateNode("ReportItems");
//            XmlElement textbox = doc.CreateElement("Textbox", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
//            textbox.SetAttribute("Name", colname);
//            node.AppendChild(textbox);
//            XmlNode zindex = CreateNode("ZIndex", "12");
//            textbox.AppendChild(zindex);
//            XmlNode style = CreateNode("Style");
//            XmlNode borderstyle = CreateNode("BorderStyle");
//            style.AppendChild(borderstyle);
//            XmlNode defaul = CreateNode("Default", "Solid");
//            borderstyle.AppendChild(defaul);
//            XmlNode textalign = CreateNode("TextAlign", "Center");
//            style.AppendChild(textalign);
//            XmlNode PaddingLeft = CreateNode("PaddingLeft", "2pt");
//            style.AppendChild(PaddingLeft);
//            XmlNode PaddingBottom = CreateNode("PaddingBottom", "2pt");
//            style.AppendChild(PaddingBottom);
//            XmlNode FontFamily = CreateNode("FontFamily", "宋体");
//            style.AppendChild(FontFamily);
//            XmlNode FontWeight = CreateNode("FontWeight", "700");
//            style.AppendChild(FontWeight);
//            XmlNode VerticalAlign = CreateNode("VerticalAlign", "Bottom");
//            style.AppendChild(VerticalAlign);
//            XmlNode PaddingTop = CreateNode("PaddingTop", "2pt");
//            style.AppendChild(PaddingTop);
//            XmlNode PaddingRight = CreateNode("PaddingRight", "2pt");
//            style.AppendChild(PaddingRight);
//            textbox.AppendChild(style);
//            XmlNode cangrow = CreateNode("CanGrow", "true");
//            textbox.AppendChild(cangrow);
//            XmlNode value = CreateNode("Value", paramvalue);
//            textbox.AppendChild(value);
//            docFrag.AppendChild(node);
//            refCd[0].InsertAfter(docFrag, refCd[0].LastChild);
//            doc.Save(filename);

//        }

////表类型报表多列表头增加表头列的第一行

//        public void AddTableHaderFirstRowCell(string colname, string paramvalue)
//        {
//            XmlNode node =null;
//            XmlNodeList refCd = root.SelectNodes("//default:Report//default:Body//default:ReportItems//default:Table//default:Header//default:TableRows//default:TableRow//default:TableCells", xnm);
//            XmlElement docFrag = doc.CreateElement("TableCell", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
//            node = CreateNode("ColSpan", "2");
//            docFrag.AppendChild(node);
//            node = CreateNode("ReportItems");
//              XmlElement textbox = doc.CreateElement("Textbox", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
//              textbox.SetAttribute("Name", colname);
//              node.AppendChild(textbox);
//                 XmlNode zindex = CreateNode("ZIndex", "16");
//                 textbox.AppendChild(zindex);
//                 XmlNode style = CreateNode("Style");
//                     XmlNode borderstyle = CreateNode("BorderStyle");
//                     style.AppendChild(borderstyle);
//                        XmlNode defaul = CreateNode("Default", "Solid");
//                        borderstyle.AppendChild(defaul);
//                     XmlNode textalign = CreateNode("TextAlign", "Center");
//                     style.AppendChild(textalign);
//                     XmlNode PaddingLeft = CreateNode("PaddingLeft", "2pt");
//                     style.AppendChild(PaddingLeft);
//                     XmlNode PaddingBottom = CreateNode("PaddingBottom", "2pt");
//                     style.AppendChild(PaddingBottom);
//                     XmlNode FontFamily = CreateNode("FontFamily", "宋体");
//                     style.AppendChild(FontFamily);
//                     XmlNode FontWeight = CreateNode("FontWeight", "700");
//                     style.AppendChild(FontWeight);
//                     XmlNode VerticalAlign = CreateNode("VerticalAlign", "Bottom");
//                     style.AppendChild(VerticalAlign);
//                     XmlNode PaddingTop = CreateNode("PaddingTop", "2pt");
//                     style.AppendChild(PaddingTop);
//                     XmlNode PaddingRight = CreateNode("PaddingRight", "2pt");
//                     style.AppendChild(PaddingRight);
//                 textbox.AppendChild(style);
//                 XmlNode cangrow = CreateNode("CanGrow", "true");
//                 textbox.AppendChild(cangrow);
//                 XmlNode value = CreateNode("Value", paramvalue);
//                 textbox.AppendChild(value);        
//            docFrag.AppendChild(node);                 
//            refCd[0].InsertAfter(docFrag, refCd[0].LastChild);
//            doc.Save(filename);

//        }

//        //Parameters!dwmc.Value

////增加表头的第二行
//        public void AddTableHaderSecondRowCell(string colname,string paramvalue)
//        {
//            XmlNodeList refCd = root.SelectNodes("//default:Report/default:Body/default:ReportItems/default:Table/default:Header/default:TableRows/default:TableRow/default:TableCells", xnm);
//            XmlElement docFrag = doc.CreateElement("TableCell", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
//            XmlNode reportitems = CreateNode("ReportItems");
//               XmlElement textbox = doc.CreateElement("Textbox", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
//                  textbox.SetAttribute("Name", colname);

//                  XmlNode zindex = CreateNode("ZIndex", "9");
//                  textbox.AppendChild(zindex);
//                  XmlNode style = CreateNode("Style");
//                  XmlNode borderstyle = CreateNode("BorderStyle");
//                  style.AppendChild(borderstyle);
//                  XmlNode defaul = CreateNode("Default", "Solid");
//                  borderstyle.AppendChild(defaul);
//                  XmlNode textalign = CreateNode("TextAlign", "Center");
//                  style.AppendChild(textalign);
//                  XmlNode PaddingLeft = CreateNode("PaddingLeft", "2pt");
//                  style.AppendChild(PaddingLeft);
//                  XmlNode PaddingBottom = CreateNode("PaddingBottom", "2pt");
//                  style.AppendChild(PaddingBottom);
//                  XmlNode FontFamily = CreateNode("FontFamily", "宋体");
//                  style.AppendChild(FontFamily);                
//                  XmlNode VerticalAlign = CreateNode("VerticalAlign", "Middle");
//                  style.AppendChild(VerticalAlign);
//                  XmlNode PaddingTop = CreateNode("PaddingTop", "2pt");
//                  style.AppendChild(PaddingTop);
//                  XmlNode PaddingRight = CreateNode("PaddingRight", "2pt");
//                  style.AppendChild(PaddingRight);
//                  textbox.AppendChild(style);
//                  XmlNode cangrow = CreateNode("CanGrow", "true");
//                  textbox.AppendChild(cangrow);
//                  XmlNode value = CreateNode("Value", paramvalue);
//                  textbox.AppendChild(value);
//                  reportitems.AppendChild(textbox);
//            docFrag.AppendChild(reportitems);
//            refCd[1].InsertAfter(docFrag, refCd[1].LastChild);
//            doc.Save(filename);

//        }
        
////动态增加详细列 

//public void AddDetailsCell(string ColName,string ColValue)
//        {
//            XmlNode refCd = root.SelectSingleNode("//default:Report//default:Body//default:ReportItems//default:Table//default:Details//default:TableRows//default:TableRow//default:TableCells", xnm);
//            XmlElement docFrag = doc.CreateElement("TableCell", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
//            XmlNode reportitems = CreateNode("ReportItems");
//            XmlElement textbox = doc.CreateElement("Textbox", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
//            textbox.SetAttribute("Name", ColName);
//            XmlNode zindex = CreateNode("ZIndex", "9");
//            textbox.AppendChild(zindex);
//            XmlNode style = CreateNode("Style");
//            textbox.AppendChild(style);
//            XmlNode borderstyle = CreateNode("BorderStyle");
//            style.AppendChild(borderstyle);
//            XmlNode defaul = CreateNode("Default", "Solid");
//            borderstyle.AppendChild(defaul);
//            XmlNode textalign = CreateNode("TextAlign", "Center");
//            style.AppendChild(textalign);
//            XmlNode PaddingLeft = CreateNode("PaddingLeft", "2pt");
//            style.AppendChild(PaddingLeft);
//            XmlNode PaddingBottom = CreateNode("PaddingBottom", "2pt");
//            style.AppendChild(PaddingBottom);
//            XmlNode FontFamily = CreateNode("FontFamily", "宋体");
//            style.AppendChild(FontFamily);
//            XmlNode VerticalAlign = CreateNode("VerticalAlign", "Middle");
//            style.AppendChild(VerticalAlign);
//            XmlNode PaddingTop = CreateNode("PaddingTop", "2pt");
//            style.AppendChild(PaddingTop);
//            XmlNode PaddingRight = CreateNode("PaddingRight", "2pt");
//            style.AppendChild(PaddingRight);
//            XmlNode cangrow = CreateNode("CanGrow", "true");
//            textbox.AppendChild(cangrow);
//            XmlNode value = CreateNode("Value", ColValue);
//            textbox.AppendChild(value);
//            reportitems.AppendChild(textbox);
//            docFrag.AppendChild(reportitems);
//            refCd.InsertAfter(docFrag, refCd.LastChild);
//            doc.Save(filename);
       
//        }
//    //向数据集增加字段定义
//        public void AddDataSetColumn(string fieldname,string filetype)
//        {

//            XmlNode refCd = root.SelectSingleNode("//default:Report//default:DataSets//default:DataSet//default:Fields", xnm);
//            XmlElement docFrag = doc.CreateElement("Field", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");           
//            docFrag.SetAttribute("Name",fieldname);

//            XmlNode x1 = doc.CreateNode(XmlNodeType.Element, "rd:TypeName", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner");
//            x1.InnerText = filetype;
//            docFrag.AppendChild(x1);
//            XmlNode x2 = doc.CreateNode(XmlNodeType.Element, "DataField", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
//            x2.InnerText = fieldname;
//            docFrag.AppendChild(x2);         
//            refCd.InsertAfter(docFrag, refCd.LastChild);
//            doc.Save(filename);

//        }
//        /// <summary>
//        /// 根据列数据调宽
//        /// </summary>
//        /// <param name="width"></param>
//        public void EditPageHeaderTb28Width(decimal width)
//        {
//            XmlNode oldCd = root.SelectSingleNode("//default:Report//default:PageHeader//default:ReportItems//default:Textbox[@Name=/"textbox28/"]", xnm);
//            XmlNode newCd = oldCd.ChildNodes[3].Clone();
//            decimal aa = Convert.ToDecimal(newCd.InnerText.Replace("cm", ""));
//            newCd.InnerText = Convert.ToString(aa + width) + "cm";
//            oldCd.ReplaceChild(newCd, oldCd.ChildNodes[3]);
//            doc.Save(filename);

//        }
//        public void EditPageHeaderTb29Left(decimal width)
//        {
//            XmlNode oldCd = root.SelectSingleNode("//default:Report//default:PageHeader//default:ReportItems//default:Textbox[@Name=/"textbox29/"]", xnm);
//            XmlNode newCd = oldCd.ChildNodes[0].Clone();
//            decimal aa = Convert.ToDecimal(newCd.InnerText.Replace("cm", ""));
//            newCd.InnerText = Convert.ToString(aa + width) + "cm";
//            oldCd.ReplaceChild(newCd, oldCd.ChildNodes[0]);
//            doc.Save(filename);

//        }
//        public void EditPageFooterLineWidth(string name, decimal width)
//        {
//            XmlNode oldCd = root.SelectSingleNode(String.Format("//default:Report//default:PageFooter//default:ReportItems//default:Line[@Name=/"{0}/"]", name), xnm);
//            XmlNode newCd = oldCd.ChildNodes[3].Clone();
//            decimal aa = Convert.ToDecimal(newCd.InnerText.Replace("cm", ""));
//            newCd.InnerText = Convert.ToString(aa + width) + "cm";
//            oldCd.ReplaceChild(newCd, oldCd.ChildNodes[3]);
//            doc.Save(filename);

//        }
//        /// <summary>
//        /// 向表中增加一字段
//        /// </summary>
//        /// <param name="fieldwidth"></param>
//        public void AddTableColumn(string fieldwidth)
//        {

//            XmlNode refCd = root.SelectSingleNode("//default:Report//default:Body//default:ReportItems//default:Table//default:TableColumns", xnm);
//            XmlElement docFrag = doc.CreateElement("TableColumn", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
//            XmlNode aa = CreateNode("Width", fieldwidth);
//            docFrag.AppendChild(aa);
//            refCd.InsertAfter(docFrag, refCd.LastChild);
//            doc.Save(filename);

//        }
//        /// <summary>
//        /// 数据集中是否包含该字段
//        /// </summary>-
//        ///  <Field Name="belong">
//        ///        <rd:TypeName>System.String</rd:TypeName>
//        ///       <DataField>belong</DataField>
//        ///  </Field>
//        /// <param name="fieldname"></param>
//        /// <returns></returns>
//        public bool DataSetFieldIsExist(string fieldname)
//        {
//            XmlNode refCd = root.SelectSingleNode(string.Format("//default:Report//default:DataSets//default:DataSet//default:Fields//default:Field[@Name={0}]", "/"" + fieldname + "/""), xnm);
//            if (refCd != null)
//                return true;
//            else
//                return false;
//        }
//        /// <summary>
//        /// 详细列是否存在
//        /// </summary>
//        /// <param name="Paramname"></param>
//        /// <returns></returns>
//        public bool DetailColIsExist(string Paramname)
//        {
//            XmlNode refCd = root.SelectSingleNode(string.Format("//default:Report//default:Body//default:ReportItems//default:Table//default:Details//default:TableRows//default:TableRow//default:TableCells//default:TableCell//default:ReportItems//default:Textbox[@Name={0}]", "/"" + Paramname + "/""), xnm);
//            if (refCd != null)
//                return true;
//            else
//                return false;

//        }
//        /// <summary>
//        /// 表头列是否存在      
//        /// </summary>
//        /// <param name="Paramname"></param>
//        /// <returns></returns>
//        public bool TableHeaderIsExist(string Paramname)
//        {
//            XmlNode refCd = root.SelectSingleNode(string.Format("//default:Report//default:Body//default:ReportItems//default:Table//default:Header//default:TableRows//default:TableRow//default:TableCells//default:TableCell//default:ReportItems//default:Textbox[@Name={0}]", "/"" + Paramname + "/""), xnm);
//            if (refCd != null)
//                return true;
//            else
//                return false;

//        }
//        /// <summary>
//        /// 参数是否存在
//        /// </summary>
//        /// <param name="Paramname"></param>
//        /// <returns></returns>
//        public bool ParamererIsExist(string Paramname)
//        {
//            XmlNode refCd = root.SelectSingleNode(string.Format("//default:Report/default:ReportParameters/default:ReportParameter[@Name={0}]", "/"" + Paramname + "/""), xnm);
//            if (refCd != null)
//                return true;
//            else
//                return false;
       
//        }