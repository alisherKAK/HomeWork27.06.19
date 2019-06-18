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
using CefSharp.Wpf;
using CefSharp;

namespace HomeWork27_06_19
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<News> _news = new List<News>();

        public MainWindow()
        {
            InitializeComponent();
            ParseRssFile();
            newsListBox.ItemsSource = _news;
        }

        private async void ParseRssFile()
        {
            string result;

            using (var client = new WebClient())
            {
                result  = await client.DownloadStringTaskAsync(new Uri("https://www.gamespot.com/feeds/reviews/"));
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(result);

            XmlNodeList itemNodes = xmlDocument.SelectNodes("rss/channel/item");

            foreach (XmlNode itemNode in itemNodes)
            {
                XmlNode rssSubNode = itemNode.SelectSingleNode("title");
                string title = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = itemNode.SelectSingleNode("pubDate");
                string publishedDate = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = itemNode.SelectSingleNode("description");
                string description = rssSubNode != null ? rssSubNode.InnerText : "";

                _news.Add(new News()
                {
                    Description = description,
                    PublishedDate = Convert.ToDateTime(publishedDate),
                    Title = title
                });
            }

            newsBrowser.LoadHtml(_news[0].Description);
        }

        private void NewsBrowserIsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e){}

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            newsBrowser.LoadHtml(_news[newsListBox.SelectedIndex].Description);
        }
    }
}
