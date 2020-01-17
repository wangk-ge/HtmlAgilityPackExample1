using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace HtmlAgilityPackExample1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonParseHtml_Click(object sender, EventArgs e)
        {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "HTML File (*.html;)|*.html";
			openFileDialog.Multiselect = false;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				if (String.IsNullOrEmpty(openFileDialog.FileName))
				{
					return;
				}

				string strHtml = String.Empty;

				using (StreamReader reader = new StreamReader(openFileDialog.FileName, Encoding.UTF8))
				{
					strHtml = reader.ReadToEnd();
					reader.Close();
				}

				HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
				doc.LoadHtml(strHtml); //加载html
				foreach (var err in doc.ParseErrors)
				{
					Console.WriteLine(err.Code);
				}

				//Console.WriteLine(doc.Text);

				HtmlAgilityPack.HtmlNode rootNode = doc.DocumentNode;//获取文档的根节点
				//Console.WriteLine(rootNode.OuterHtml);
#if true
				string xpath = @"//table";
				HtmlAgilityPack.HtmlNode node = rootNode.SelectSingleNode(xpath);//获取表格
				foreach (var script in node.Descendants("script").ToArray())
					script.Remove();
				foreach (var style in node.Descendants("style").ToArray())
					style.Remove();
				string innerText = node.OuterHtml;//到这里就是纯纯的表格了      
				var trNodes = node.SelectNodes("tr");
				
				foreach (var trnod in trNodes) //遍历行
				{
					var tdNodes = trnod.SelectNodes("td");
					for (int i = 0; i < tdNodes.Count; i++) //遍历列
					{
						Console.WriteLine(tdNodes[i].InnerText);
					}
				}
#endif
			}
		}
    }
}
