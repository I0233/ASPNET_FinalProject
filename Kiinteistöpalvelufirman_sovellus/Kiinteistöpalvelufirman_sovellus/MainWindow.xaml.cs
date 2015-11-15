using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;


namespace Kiinteistöpalvelufirman_sovellus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            // Manually alter window height and width
            this.SizeToContent = SizeToContent.Manual;

            // Automatically resize width relative to content
            this.SizeToContent = SizeToContent.Width;

            // Automatically resize height relative to content
            this.SizeToContent = SizeToContent.Height;

            // Automatically resize height and width relative to content
            this.SizeToContent = SizeToContent.WidthAndHeight;
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Registration register_form = new Registration();
            register_form.Show();
            Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Tarkistaa kentien syötteitä 
            if (txtUserName.Text == ""){
                errormessage.Text = "Käyttäjänimi puuttuu..";
                txtUserName.Focus();
            }
            else if (txtPassword.Password == ""){
                errormessage.Text = "Salasana puuttuu..";
                txtPassword.Focus();
            }
            else if (txtUserName.Text == "" && txtPassword.Password == ""){
                errormessage.Text = "Käyttäjänimi ja salasana puuttuvat..";
                txtUserName.Focus();
            }
            else if (txtUserName.Text != "" && !Regex.IsMatch(txtUserName.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Antamasi sähköpostiosoitteesi ei kelpaa.";
                txtUserName.Select(0, txtUserName.Text.Length);
                txtUserName.Focus();
            }
            else
            {
                string connStr = Kiinteistöpalvelufirman_sovellus.Properties.Settings.Default.Tietokanta;
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM Asiakkaat WHERE sähköposti='" + txtUserName.Text + "' AND salasana='" + txtPassword.Password +"'",conn);
                        MySqlCommand cmd2 = new MySqlCommand("SELECT * FROM Asiakkaat WHERE sähköposti='" + txtUserName.Text + "' AND salasana='" + txtPassword.Password + "'", conn);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        cmd2.CommandType = CommandType.Text;
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        adapter.SelectCommand = cmd2;
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        if (count == 0)
                        {
                            errormessage.Text = "Tämä käyttäjä ei löytynyt tietokannastamme. Ole hyvä ja rekisteröidy käyttäjäksi.";
                        } else {
                            Application.Current.Properties["Logged_username"] = dataSet.Tables[0].Rows[0]["etunimi"].ToString() + " " + dataSet.Tables[0].Rows[0]["sukunimi"].ToString();
                            Application.Current.Properties["Gender"] = dataSet.Tables[0].Rows[0]["sukupuoli"].ToString();
                            Order order_page = new Order();
                            order_page.user_name.Text = Application.Current.Properties["Logged_username"].ToString();
                            order_page.Show();
                            Close();
                        }
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        errormessage.Text = "Tietokantaan ei saa yhteyttä virhe on " + ex.Message;
                    }
                }
            }

        }

    }
}