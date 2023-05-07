using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DesktopVoiceAssistant
{
    public partial class AddCommandForm : Form
    {
        public AddCommandForm()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            //of.Filter = "(*.exe)|*.exe";
            if (of.ShowDialog()==DialogResult.OK)
            {
                txtProcess.Text = of.FileName;
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (txtProcess.Text!=null && txtCommand.Text!=null)
            {
                
                StreamWriter sw = File.AppendText("commands.txt");
                sw.WriteLine(txtCommand.Text);
                sw.Close();

                StreamWriter sw2 = File.AppendText("process.txt");
                sw2.WriteLine(txtProcess.Text);
                sw2.Close();

                MessageBox.Show("Command Added Successfully", "Action completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtCommand.Text = "";
                if (radioButton1.Checked)
                {
                    txtProcess.Text = "";
                }
                else
                {
                    txtProcess.Text = "https://";
                }

            }
            else
            {

                MessageBox.Show("Invalid or null data entered", "Process Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                txtProcess.Text = "";
            }
            else
            {
                txtProcess.Text = "https://";
            }
        }
    }
}
