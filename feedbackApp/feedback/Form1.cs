using System;
using System.Windows.Forms;
using System.Net.Mail;
using System.Data.SqlClient;
using System.IO;

namespace feedback
{
    public partial class Form1 : Form
    {
        public static string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        public string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + dir + @"\Mile.mdf;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlConnection connnect = new SqlConnection(conString);

            string tmp = "INSERT INTO Comments([FullName], [Email], [Comment]) VALUES(N'" + textBox1.Text + "', N'" + textBox2.Text + "',N'" + textBox3.Text + "')";

            SqlCommand command = new SqlCommand(tmp, connnect);
            try
            {
                connnect.Open();

                int n = command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connnect.Close();
            }

            MailMessage msg = new MailMessage();

            msg.To.Add("it@mile.by");
            msg.Subject = "Отзыв о работе магазина от " + this.textBox1.Text;
            msg.From = new MailAddress(this.textBox2.Text);
            msg.Body = this.textBox3.Text;

            SmtpClient smtp = new SmtpClient("aspmx.l.google.com", 25);

            try
            {
                smtp.Send(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
        }
    }
}
