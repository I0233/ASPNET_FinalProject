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
using System.Windows.Shapes;
using System.Threading;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Text.RegularExpressions;

namespace Kiinteistöpalvelufirman_sovellus
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        string connStr = Kiinteistöpalvelufirman_sovellus.Properties.Settings.Default.Tietokanta;
        MySqlConnection conn;
        public Registration()
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (Cancel.Content.ToString() == "Peruuta"){
                MainWindow login_page = new MainWindow();
                login_page.Show();
                Close();
            }
            else
            {
                Close();
            }
        }

        private void Rest_Click(object sender, RoutedEventArgs e)
        {
            txtEmail.Clear();
            txtFName.Clear();
            txtLName.Clear();
            txtPassword.Clear();
            txtPasswordConfirm.Clear();
            txtPhonenumber.Clear();
            successmessage.Text = "";
            errormessage.Text = "";
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (Submit.Content.ToString() == "Vahvista")
            {
                if (txtEmail.Text != "" && txtFName.Text != "" && txtLName.Text != "" && txtPassword.Password != "" && txtPasswordConfirm.Password != "" && txtPhonenumber.Text != "" && (gn_man.IsChecked == true || gn_woman.IsChecked == true))
                {
                    Customer.Validate(txtEmail.Text, txtFName.Text, txtLName.Text, txtPassword.Password, txtPasswordConfirm.Password, txtPhonenumber.Text);
                    errormessage.Text = Customer.validation_msg;
                    Customer.validation_msg = "";
                    if (errormessage.Text == "")
                    {
                        using (conn = new MySqlConnection(connStr))
                        {
                            try
                            {
                                conn.Open();
                                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM Asiakkaat WHERE sahkoposti='" + txtEmail.Text + "'", conn);
                                int count = Convert.ToInt32(cmd.ExecuteScalar());
                                if (count > 0)
                                {
                                    errormessage.Text = "Tämä käyttäjä on jo olemassa. Muuta sähköpostiä rekisteröityäksesi palvelun käyttäjäksi.";
                                }
                                else
                                {
                                    string gender;
                                    if (gn_man.IsChecked == true) { gender = "Mies"; }
                                    else { gender = "Nainen"; }
                                    MySqlCommand cmd2 = new MySqlCommand("INSERT INTO Asiakkaat (sahkoposti,etunimi,sukunimi,salasana,puhelinnumero,sukupuoli) values('" + txtEmail.Text + "','" + txtFName.Text + "','" + txtLName.Text + "','" + txtPassword.Password + "','" + txtPhonenumber.Text + "', '" + gender + "')", conn);
                                    cmd2.CommandType = CommandType.Text;
                                    cmd2.ExecuteNonQuery();
                                    conn.Close();
                                    successmessage.Text = "Olet rekisteröinyt onnistuneesti kiinteistöpalvelufirman asiakkaakasi";
                                    Cancel.Visibility = Visibility.Hidden;
                                    login_btn.Visibility = Visibility.Visible;
                                }
                            }
                            catch (Exception ex)
                            {

                                errormessage.Text = "Tietokantaan tallentamisessa on ilmeistynyt tällainen vihre: " + ex.Message;
                            }
                        }
                    }

                }
                else
                {
                    errormessage.Text = "Täytä puuttuvat tiedot vahvistaaksesi.";
                }
            }
            else
            {
                if (txtEmail.Text != "" && txtFName.Text != "" && txtLName.Text != "" && txtPassword.Password != "" && txtPasswordConfirm.Password != "" && txtPhonenumber.Text != "" )
                {
                    Customer.Validate(txtEmail.Text, txtFName.Text, txtLName.Text, txtPassword.Password, txtPasswordConfirm.Password, txtPhonenumber.Text);
                    errormessage.Text = Customer.validation_msg;
                    Customer.validation_msg = "";
                    if (errormessage.Text == "")
                    {
                        using (MySqlConnection conn = new MySqlConnection(connStr))
                        {
                            try
                            {
                                conn.Open();
                                Application.Current.Properties["user_email"] = txtEmail.Text;
                                Application.Current.Properties["Logged_username"] = txtFName.Text + " " + txtLName.Text;
                                if (txtEmail.Text != Application.Current.Properties["user_email"].ToString())
                                {
                                    MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM Asiakkaat WHERE sahkoposti='" + txtEmail.Text + "'", conn);
                                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                                    if (count > 0)
                                    {
                                        errormessage.Text = "Tämä käyttäjä on jo olemassa. Muuta sähköpostiä päivittäksesi käyttäjäntiedot.";
                                        return;
                                    }
                                }

                                MySqlCommand cmd2 = new MySqlCommand("Update Asiakkaat SET sahkoposti='" + txtEmail.Text + "', etunimi='" + txtFName.Text + "', sukunimi='" + txtLName.Text + "', salasana='" + txtPassword.Password + "', puhelinnumero='" + txtPhonenumber.Text + "' WHERE id='"+ Application.Current.Properties["user_id"].ToString() +"'", conn);
                                cmd2.CommandType = CommandType.Text;
                                cmd2.ExecuteNonQuery();
                                successmessage.Text = "Käyttäjän tiedot ovat päivitetty onnistuneesti.";
                                conn.Close();

                            }
                            catch (Exception ex)
                            {

                                errormessage.Text = "Käyttäjän tiedot ei voitu hakea. Virhe: " + ex.Message;
                            }
                        }
                    }
                }
                else
                {
                    errormessage.Text = "Täytä puuttuvat tiedot päivittäksesi käyttäjäntiedot.";
                }
            }
        }

        private void login_click(object sender, RoutedEventArgs e)
        {
            MainWindow login_form = new MainWindow();
            login_form.Show();
            Close();
        }
        

    }
}
