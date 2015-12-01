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
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace Kiinteistöpalvelufirman_sovellus
{
    /// <summary>
    /// Interaction logic for Confrmation.xaml
    /// </summary>
    public partial class Confrmation : Window
    {
        string connStr = Kiinteistöpalvelufirman_sovellus.Properties.Settings.Default.Tietokanta;
        MySqlCommand cmd;
        Registration register_form = new Registration();
        public Confrmation()
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
            GetOrder();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login_form = new MainWindow();
            login_form.Show();
            Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Order order_form = new Order();
            register_form.Gender_grid.Visibility = Visibility.Hidden;
            register_form.Title = "Muokkaamislomake";
            register_form.Icon = new BitmapImage(new Uri("pack://application:,,,/Images/red-edit-icon.png"));
            register_form.textBlockHeading.Text = "Muokkaaminen";
            register_form.header2.Text = "Muokkaa alla olevat tiedot päivittäksesi käyttäjän tiedot";
            register_form.Cancel.Content = "Takaisin";
            register_form.Submit.Content = "Tallenna";
            register_form.Cancel.Click += this.Back_in_editting_form;
            register_form.Cancel.OnApplyTemplate();
            this.GetUserData();
            register_form.Show();
            Close();
        }

        public void Back_in_editting_form(object sender, RoutedEventArgs e)
        {
            Confrmation confirmation_form = new Confrmation();
            confirmation_form.Show();
        }

        private void Confirmation_Click(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    cmd = new MySqlCommand("INSERT INTO Tilauslomake (asiakas_id,kayntiosoite,postinumero_toimipaikka,asunnontyyppi,laskutusosoite,palvelut,kommentti,tilauspvm) values((SELECT id From Asiakkaat WHERE sahkoposti='" + Application.Current.Properties["user_email"].ToString() + "'),'" + txtAddress.Text + "','" + txtPostalCode.Text + "','" + txtFlatType.Text + "','" + txtBillingAddress.Text + "', '" + txtService.Text + "','" + txtComment.Text + "', '" + txtPVM.Text + "')", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    //Vahvistus popup
                    MessageBoxResult result = MessageBox.Show(this, "Tilauksesi on lähetetty onnistuneesti käsittelyyn. Paina OK päästäksesi selailemaan Teidän tilauksia.",
                    "Vahvistus", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (result == MessageBoxResult.OK)
                    {
                        Orderlist order_list = new Orderlist();
                        order_list.Show();
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Tilausta ei voitu vahvistaa. Ole hyvä ja yritä uudelleen.Virhe: " + ex.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        // Takaisin tapahtumaa varten tiedot haetaan uudelleen
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Order order_form = new Order();
            order_form.txtAddress.Text = OrderForm.address;
            order_form.txtBillingAddress.Text = OrderForm.billingAddress;
            order_form.txtPostalCode.Text = OrderForm.postalCode;
            order_form.txtComment.Text = OrderForm.comment;
            string flat_type = OrderForm.flatType.ToString();
            switch (flat_type)
            {
                case "Kerrostalo" :
                    order_form.Type1.IsChecked = true;
                    break;
                case "Omakotitalo":
                    order_form.Type2.IsChecked = true;
                    break;
                case "Rivitalo":
                    order_form.Type3.IsChecked = true;
                    break;
                case "Paritalo":
                    order_form.Type4.IsChecked = true;
                    break;
                default:
                    order_form.Type1.IsChecked = true;
                    break;

            }
            order_form.Submit.Content = "Tallenna tiedot";
            order_form.Submit.Width = 106;
            order_form.Next.Visibility = Visibility.Visible;
            order_form.Show();
            order_form.cmb_palvelut.SelectedItem = OrderForm.service;
            Close();
        }

        public void GetUserData()
        {

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Asiakkaat WHERE id='" + Application.Current.Properties["user_id"].ToString() + "'", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    register_form.txtEmail.Text = dataSet.Tables[0].Rows[0]["sahkoposti"].ToString();
                    register_form.txtFName.Text = dataSet.Tables[0].Rows[0]["etunimi"].ToString();
                    register_form.txtLName.Text = dataSet.Tables[0].Rows[0]["sukunimi"].ToString();
                    register_form.txtPassword.Password = dataSet.Tables[0].Rows[0]["salasana"].ToString();
                    register_form.txtPhonenumber.Text = dataSet.Tables[0].Rows[0]["puhelinnumero"].ToString();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Käyttäjän tiedot ei voitu hakea. Virhe: " + ex.Message, "Virhe", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void GetOrder()
        {
            var man_uri = new Uri("pack://application:,,,/Images/gender-man.png");
            var woman_uri = new Uri("pack://application:,,,/Images/gender-woman.png");
            var man_icon = new BitmapImage(man_uri);
            var woman_icon = new BitmapImage(woman_uri);
            user_name.Text = Application.Current.Properties["Logged_username"].ToString();
            string gender = Application.Current.Properties["Gender"].ToString();
            if (gender == "Mies")
            {
                user_icon.Source = man_icon;
            }
            else
            {
                user_icon.Source = woman_icon;
            }
            txtUserName.Text = Application.Current.Properties["Logged_username"].ToString();
            txtPVM.Text = DateTime.Today.ToShortDateString();
            txtAddress.Text = OrderForm.address;
            txtBillingAddress.Text = OrderForm.billingAddress;
            txtPostalCode.Text = OrderForm.postalCode;
            txtFlatType.Text = OrderForm.flatType;
            txtService.Text = OrderForm.service;

            if (OrderForm.comment != "")
            {
                txtComment.Text = OrderForm.comment;
            }
            else
            {
                txtComment.Visibility = Visibility.Hidden;
                txtCommentBlock.Visibility = Visibility.Hidden;
            }
        }
    }
}
