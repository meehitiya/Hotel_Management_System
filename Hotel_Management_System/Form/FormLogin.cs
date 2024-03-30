using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AMRConnector;
using System.Data.SqlClient;

namespace Hotel_Management_System
{
    public partial class FormLogin : Form
    {
        DbConnector db;
        public FormLogin()
        {
            InitializeComponent();
            db = new DbConnector();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void pictureBoxMinimize_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBoxMinimize, "Minimize");
        }

        private void pictureBoxClose_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBoxClose, "Close");
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBoxMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBoxShow_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBoxShow, "Show Password");
        }

        private void pictureBoxHide_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBoxHide, "Hide Password");
        }

        private void pictureBoxShow_Click(object sender, EventArgs e)
        {
            pictureBoxShow.Hide();
            textBoxPassword.UseSystemPasswordChar = false;
            pictureBoxHide.Show();
        }

        private void pictureBoxHide_Click(object sender, EventArgs e)
        {
            pictureBoxHide.Hide();
            textBoxPassword.UseSystemPasswordChar = true;
            pictureBoxShow.Show();
        }

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "select User_Name, User_Password from User_Table where User_Name = '" + textBoxUsername.Text+ "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    sdr.Read();

                    if (sdr["User_Password"].Equals(textBoxPassword.Text))
                    {
                        this.Close();
                       FormDashboard formDashboard = new FormDashboard();
                        formDashboard.Username = textBoxUsername.Text;
                        textBoxUsername.Clear();
                        textBoxPassword.Clear();
                        formDashboard.Show();
                        

                        MessageBox.Show("Login Successful..", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    { 
                    }
                    {
                        MessageBox.Show("Invalid password..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                else
                {
                    MessageBox.Show("Username or Password is incorrect..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        static string constr = @"Data Source=LAPTOP-IK8EKOS3;Initial Catalog=Hotel_Management_System;Integrated Security=True;TrustServerCertificate=True";       static SqlConnection con = new SqlConnection(constr);
    }
}
            