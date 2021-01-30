using MaterialSkin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RamokSelfbot.Commands.Utils.Forms
{
    public partial class GUI : MaterialSkin.Controls.MaterialForm
    {
        MaterialSkinManager skinManager = MaterialSkinManager.Instance;
        public GUI()
        {
            InitializeComponent();
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.DARK;
            skinManager.ColorScheme = new ColorScheme(Primary.DeepPurple500, Primary.Grey900, Primary.Grey900, Accent.DeepPurple400, TextShade.WHITE);
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            start = 1;
            textBoxConnected.Text = "Connected as : " + Program.client.User.Username + "#" + Program.client.User.Discriminator + " (" + Program.client.User.Id + ")";
            JSON config = JsonConvert.DeserializeObject<JSON>(System.IO.File.ReadAllText("config.json"));
            textBoxPrefix.Text = config.prefix;
            textBoxR.Text = config.embedcolorr.ToString();
            textBoxG.Text = config.embedcolorg.ToString();
            textBoxB.Text = config.embedcolorb.ToString();
            textBoxTwitch.Text = config.twitchlink;
            textBoxYoutubeAPIKEY.Text = config.youtubeapikey;

                materialCheckBox2.Checked = config.nitrosniper;
                materialCheckBox1.Checked = config.antieveryone;
                materialCheckBox3.Checked = config.experimentalcommands;
                materialCheckBox4.Checked = config.nsfw;
                materialCheckBox5.Checked = config.debug;
            start = 0;
        }

        private void materialCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(start == 0)
            {
                JSON config = JsonConvert.DeserializeObject<JSON>(System.IO.File.ReadAllText("config.json"));
                config.experimentalcommands = materialCheckBox3.Checked;
                var configoutput = JsonConvert.SerializeObject(config, Formatting.Indented);
                System.IO.File.WriteAllText("config.json", configoutput);
            }
        }

        private void materialCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(start == 0)
            {
                JSON config = JsonConvert.DeserializeObject<JSON>(System.IO.File.ReadAllText("config.json"));
                config.nitrosniper = materialCheckBox2.Checked;
                var configoutput = JsonConvert.SerializeObject(config, Formatting.Indented);
                System.IO.File.WriteAllText("config.json", configoutput);
            }
        }

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(start == 0)
            {
                JSON config = JsonConvert.DeserializeObject<JSON>(System.IO.File.ReadAllText("config.json"));
                config.antieveryone = materialCheckBox1.Checked;
                var configoutput = JsonConvert.SerializeObject(config, Formatting.Indented);
                System.IO.File.WriteAllText("config.json", configoutput);
            }
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            JSON config = JsonConvert.DeserializeObject<JSON>(System.IO.File.ReadAllText("config.json"));
            if (config.prefix != textBoxPrefix.Text)
            {
                MessageBox.Show("To apply changes (prefix) u need to restart the selfbot.");
                config.prefix = textBoxPrefix.Text;
            }

            config.embedcolorr = int.Parse(textBoxR.Text);
            config.embedcolorg = int.Parse(textBoxG.Text);
            config.embedcolorb = int.Parse(textBoxB.Text);
            config.twitchlink = textBoxTwitch.Text;
            config.youtubeapikey = textBoxYoutubeAPIKEY.Text;

            var configoutput = JsonConvert.SerializeObject(config, Formatting.Indented);
            System.IO.File.WriteAllText("config.json", configoutput);
        }

        private void materialCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if(start == 0)
            {
                JSON config = JsonConvert.DeserializeObject<JSON>(System.IO.File.ReadAllText("config.json"));
                config.nsfw = materialCheckBox1.Checked;
                var configoutput = JsonConvert.SerializeObject(config, Formatting.Indented);
                System.IO.File.WriteAllText("config.json", configoutput);
            }
        }

        private void materialCheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if(start == 0)
            {
                if(Program.Debug != materialCheckBox5.Checked)
                {
                    MessageBox.Show("If you want to change the Debug state, restart the selfbot !", "Debug mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                JSON config = JsonConvert.DeserializeObject<JSON>(System.IO.File.ReadAllText("config.json"));
                config.debug = materialCheckBox5.Checked;
                var configoutput = JsonConvert.SerializeObject(config, Formatting.Indented);
                System.IO.File.WriteAllText("config.json", configoutput);
            }
        }

        public int start = 1;
    }
}
