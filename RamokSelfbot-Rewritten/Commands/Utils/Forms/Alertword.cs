using MaterialSkin.Controls;
using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace RamokSelfbot.Commands.Utils.Forms
{
    public partial class Alertword : MaterialForm
    {
        MaterialSkinManager skinManager = MaterialSkinManager.Instance;
        public Alertword()
        {
            InitializeComponent();
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.DARK;
            skinManager.ColorScheme = new ColorScheme(Primary.DeepPurple500, Primary.Grey900, Primary.Grey900, Accent.DeepPurple400, TextShade.WHITE);
        }

        private void Alertword_Load(object sender, EventArgs e)
        {
            start = 1;
            var a = RamokSelfbot.Utils.DumpConfig();
            if(a.alertwordlist[0] != null)
            {
                materialCheckBox1.Checked = true;
            }    
            if(a.alertwordlist[1] != null)
            {
                materialCheckBox2.Checked = true;
            }      
            if(a.alertwordlist[2] != null)
            {
                materialCheckBox3.Checked = true;
            }

            materialSingleLineTextField1.Visible = materialCheckBox1.Checked;
            materialSingleLineTextField2.Visible = materialCheckBox2.Checked;
            materialSingleLineTextField3.Visible = materialCheckBox3.Checked;
        }

        public int start;

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            var config = RamokSelfbot.Utils.DumpConfig();
            if (materialCheckBox1.Checked)
            {
                if(!string.IsNullOrEmpty(materialSingleLineTextField1.Text))
                {
                    config.alertwordlist[0] = materialSingleLineTextField1.Text;
                } else
                {
                    config.alertwordlist[0] = null;
                    materialCheckBox1.Checked = false;
                }
            }

            if (materialCheckBox2.Checked)
            {
                if (!string.IsNullOrEmpty(materialSingleLineTextField2.Text))
                {
                    config.alertwordlist[1] = materialSingleLineTextField2.Text;
                }
                else
                {
                    config.alertwordlist[1] = null;
                    materialCheckBox2.Checked = false;
                }
            }

            if (materialCheckBox3.Checked)
            {
                if (!string.IsNullOrEmpty(materialSingleLineTextField3.Text))
                {
                    config.alertwordlist[2] = materialSingleLineTextField3.Text;
                }
                else
                {
                    config.alertwordlist[2] = null;
                    materialCheckBox3.Checked = false;
                }
            }

            config.alertword = true;

            var configoutput = JsonConvert.SerializeObject(config, Formatting.Indented);
            System.IO.File.WriteAllText("config.json", configoutput);
        }

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            materialSingleLineTextField1.Visible = materialCheckBox1.Checked;
        }

        private void materialCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            materialSingleLineTextField2.Visible = materialCheckBox2.Checked;

        }

        private void materialCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            materialSingleLineTextField3.Visible = materialCheckBox3.Checked;

        }
    }
}
