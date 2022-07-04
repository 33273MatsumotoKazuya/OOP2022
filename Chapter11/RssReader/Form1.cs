using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RssReader {
    public partial class RssReader : Form {
        public RssReader() {
            InitializeComponent();
        }

        IEnumerable<XElement> xNews;

        private void btRssGet_Click(object sender, EventArgs e) {

            using (var wc = new WebClient()) {
                lbRssTitle.Items.Clear();
                var stream = wc.OpenRead(cbRssUrl.Text);

                var xdoc = XDocument.Load(stream);
                xNews = xdoc.Root.Descendants("item");

                foreach (var data in xNews) {
                    lbRssTitle.Items.Add((string)data.Element("title"));
                }

                lbRssTitle.SetSelected(0, true);
                cbAddText();
            }
        }

        private void lbRssTitle_Click(object sender, EventArgs e) {
            if (lbRssTitle.SelectedIndex == -1) return;
            wvBrowser.Source = new Uri((string)xNews.ElementAt(lbRssTitle.SelectedIndex).Element("link"));
        }

        private void btNext_Click(object sender, EventArgs e) {
            wvBrowser.GoForward();
        }
        
        private void btBack_Click(object sender, EventArgs e) {
            wvBrowser.GoBack();
        }

        private void RssReader_Load(object sender, EventArgs e) {
            checkEnabled();
        }
        
        private void wvBrowser_NavigationCompleted(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlNavigationCompletedEventArgs e) {
            checkEnabled();
        }

        private void checkEnabled() {
            btBack.Enabled = wvBrowser.CanGoBack;
            btNext.Enabled = wvBrowser.CanGoForward;
        }

        private void cbAddText() {
            if (!cbRssUrl.Items.Contains(cbRssUrl.Text)) {
                cbRssUrl.Items.Add(cbRssUrl.Text);
            }
        }
    }
}
