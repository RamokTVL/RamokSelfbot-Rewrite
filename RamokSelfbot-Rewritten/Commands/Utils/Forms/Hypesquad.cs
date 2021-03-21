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
    public partial class Hypesquad : MaterialSkin.Controls.MaterialForm
    {
        MaterialSkinManager skinManager = MaterialSkinManager.Instance;
        public Hypesquad()
        {
            InitializeComponent();
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.DARK;
            skinManager.ColorScheme = new ColorScheme(Primary.DeepPurple500, Primary.Grey900, Primary.Grey900, Accent.DeepPurple400, TextShade.WHITE);
        }

        private void GUI_Load(object sender, EventArgs e)
        {

        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.SelectedIndex.ToString();

            switch (selectedItem)
            {
                case "-1":
                    richTextBox1.Clear();
                    richTextBox1.AppendText("Please, set a hypesquad!");

                    // MessageBox.Show("Please, set a hypesquad!", "Hypesquad error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "0":
                    Program.client.User.SetHypesquad(Discord.Hypesquad.None);
                    Program.client.User.Update();
                    richTextBox1.Clear();
                    richTextBox1.AppendText("Hypesquad set to none!");

                    //MessageBox.Show("Hypesquad set to none!", "Hypesquad success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "1":
                    Program.client.User.SetHypesquad(Discord.Hypesquad.Brilliance);
                    Program.client.User.Update();
                    richTextBox1.Clear();
                    richTextBox1.AppendText("Hypesquad set to Brilliance!");
                    return;
                    //MessageBox.Show("Hypesquad set to Brilliance!", "Hypesquad success", MessageBoxButtons.OK, MessageBoxIcon.Information);
#pragma warning disable CS0162 // Code inaccessible détecté
                    break;
#pragma warning restore CS0162 // Code inaccessible détecté
                case "2":
                    Program.client.User.SetHypesquad(Discord.Hypesquad.Bravery);
                    Program.client.User.Update();
                    richTextBox1.Clear();
                    richTextBox1.AppendText("Hypesquad set to Bravery!");

                    //  MessageBox.Show("Hypesquad set to Bravery!", "Hypesquad success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "3":
                    Program.client.User.SetHypesquad(Discord.Hypesquad.Balance);
                    Program.client.User.Update();
                    richTextBox1.Clear();
                    richTextBox1.AppendText("Hypesquad set to Balance!");

                    //    MessageBox.Show("Hypesquad set to Balance!", "Hypesquad success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }
    }
}
