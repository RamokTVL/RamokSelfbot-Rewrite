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
    public partial class Trello : Form
    {
        public Trello()
        {
            InitializeComponent();
        }

        private void Trello_Load(object sender, EventArgs e)
        {
            CefSharp.WinForms.ChromiumWebBrowser browser = new CefSharp.WinForms.ChromiumWebBrowser("https://trello.com/b/LMua8bEa/ramokselfbot");
            this.Controls.Add(browser);
        }
    }
}
