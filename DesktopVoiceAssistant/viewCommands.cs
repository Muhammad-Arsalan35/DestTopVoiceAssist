using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopVoiceAssistant
{
    public partial class viewCommands : Form
    {
        string[] commands;
        string[] process;
        public viewCommands()
        {
            InitializeComponent();
        }

        private void viewCommands_Load(object sender, EventArgs e)
        {



            loadGrid();

            
        }

        public void loadGrid()
        {
            bunifuCustomDataGrid1.Columns.Clear();
            bunifuCustomDataGrid1.Rows.Clear();
            commands = File.ReadAllLines("commands.txt");
            process = File.ReadAllLines("process.txt");

            bunifuCustomDataGrid1.Columns.Add("Commands", "Commands");
            bunifuCustomDataGrid1.Columns.Add("Process", "Action");

            for (int i = 0; i < commands.Length; i++)
            {

                bunifuCustomDataGrid1.Rows.Add();

                bunifuCustomDataGrid1.Rows[i].Cells[0].Value = commands[i];

                bunifuCustomDataGrid1.Rows[i].Cells[1].Value = process[i];
            

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.Rows.Count<=2)
            {
                MessageBox.Show("Can't Delete last command, program must contain minimum 1 command","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
           else  if (bunifuCustomDataGrid1.SelectedRows.Count>0 )
            {
                try
                {
                    if (MessageBox.Show("Do you want to delete selected row?\nCommand : " + bunifuCustomDataGrid1.SelectedCells[0].Value.ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        deleteCommand(bunifuCustomDataGrid1.SelectedCells[0].Value.ToString(), bunifuCustomDataGrid1.SelectedCells[1].Value.ToString());

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Null Row selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               


            }
        }

        public void deleteCommand(string commandTxt, string processTxt)
        {
            StreamWriter sw = File.CreateText("commands.txt");
            StreamWriter sw2 = File.CreateText("process.txt");
            for (int i = 0; i < commands.Length; i++)
            {

                if (commands[i] != commandTxt)
                {
                    sw.WriteLine(commands[i]);
                    sw2.WriteLine(process[i]);


                }
            }

            sw.Close();
            sw2.Close();

            loadGrid();


        }
    }
}
