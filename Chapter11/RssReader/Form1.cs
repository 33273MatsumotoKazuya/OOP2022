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
                var stream = wc.OpenRead(cbRssUrl.Text);

                var xdoc = XDocument.Load(stream);
                xNews = xdoc.Root.Descendants("item");

                foreach (var data in xNews) {
                    lbRssTitle.Items.Add((string)data.Element("title"));
                }

                cbAddText();
            }
        }

        private void cbAddText() {
            if (!cbRssUrl.Items.Contains(cbRssUrl.Text)) {
                cbRssUrl.Items.Add(cbRssUrl.Text);
            }
        }

        private void lbRssTitle_SelectedIndexChanged(object sender, EventArgs e) {
            var index = lbRssTitle.SelectedIndex;
            wbBrowser.Navigate((string)xNews.ElementAt(index).Element("link"));
        }
    }
}
