using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RamokSelfbot.Commands.Experimental.Forms
{
    public partial class Github : Form
    {
        public Github()
        {
            InitializeComponent();
        }

        private void Github_Load(object sender, EventArgs e)
        {
            CefSharp.WinForms.ChromiumWebBrowser browser = new CefSharp.WinForms.ChromiumWebBrowser("https://github.com/RamokTVL/RamokSelfbot-Rewritten");
            this.Controls.Add(browser);
        }
    }
}
