using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Serialization;

namespace HomeWork27_06_19
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ParseRssFile();
        }

        private async void ParseRssFile()
        {
            string result;

            using (var client = new WebClient())
            {
                result  = await client.DownloadStringTaskAsync(new Uri("http://www.playground.ru/rss/news.xml"));
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            XmlNodeList itemNodes = xmlDocument.SelectNodes("rss/channel/item");

            foreach(XmlNode itemNode in itemNodes)
            {
                XmlNode rssSubNode = itemNode.SelectSingleNode("title");
                string title = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = itemNode.SelectSingleNode("guid");
                string guid = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = itemNode.SelectSingleNode("comments");
                string comments = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = itemNode.SelectSingleNode("link");
                string link = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = itemNode.SelectSingleNode("description");
                string description = rssSubNode != null ? rssSubNode.InnerText : "";
            }
        }
    }
}
