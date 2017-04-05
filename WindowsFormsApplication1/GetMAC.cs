using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Diagnostics;
using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class GetMAC : Form
    {
        //  private String user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        private String user = Environment.UserName;
        private String pass;
        private String pcName;

        private void GetMAC_Load(object sender, EventArgs e)
        {
            if (user == "Administrator" || user == "administrator" || user == "Admin" || user == "admin")
            {
                UserNameBox.Text = user;
            }
            else
            {
                UserNameBox.Text = user + ".adm";
            }
        }

        public GetMAC()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

             pcName = textBox1.Text;
            // do job
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "getmac",
                    Arguments = "/fo table /v",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }

             
            };

                user = UserNameBox.Text;
                 pass = PassBox.Text;


            if (pcName != "localhost" && pcName != null) {
                
                proc.StartInfo.Arguments = "/s " + pcName + " /U "+user+" /P "+pass+" /fo list /v";

               // label5.Text = proc.StartInfo.Arguments;
            }
            proc.Start();
            string error = proc.StandardError.ReadToEnd();
            proc.WaitForExit();
            if (error != null)
                label2.Text = error;
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                
                if (line.Contains("LAN-Verbindung"))
                {
                    
                      String[] tmpString = line.Split(' ');
                    if (tmpString[4] == "GB")
                    {
                        label2.Text = tmpString[0] + " " + tmpString[4] + " " + tmpString[5];
                        System.Windows.Forms.Clipboard.SetText(tmpString[5]);
                        label3.Visible = true;
                    }
                    else {
                        label2.Text = tmpString[0] + " " + tmpString[4];
                        System.Windows.Forms.Clipboard.SetText(tmpString[4]);
                        label3.Visible = true;

                    }

                }
            
                // do something with line}
            }


        } // end button

        private void PassBox_MouseClick(object sender, MouseEventArgs e)
        {
            PassBox.Text = "";
            PassBox.ForeColor = Color.Black; 
            PassBox.PasswordChar = '*';
        }

        private void label5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:oleksandr.bannov@gfk.com");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
