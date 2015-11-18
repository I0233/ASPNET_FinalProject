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

        }

        private void Confirmation_Click(object sender, RoutedEventArgs e)
        {
            string connStr = Kiinteistöpalvelufirman_sovellus.Properties.Settings.Default.Tietokanta;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO Tilauslomake (asiakas_id,käyntiosoite,postinumero_toimipaikka,asunnontyyppi,laskutusosoite,palvelut,kommentti,tilauspvm) values((SELECT id From Asiakkaat WHERE sahkoposti='" + Application.Current.Properties["user_email"].ToString() + "'),'" + txtAddress.Text + "','" + txtPostalCode.Text + "','" + txtFlatType.Text + "','" + txtBillingAddress.Text + "', '" + txtService.Text + "','" + txtComment.Text + "', '" + txtPVM.Text + "')", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    //Vahvistus popup
                    MessageBoxResult result = MessageBox.Show(this, "Tilauksesi on lähtetty onnistuneesti käsittelyyn. Paina OK päästäksesi selailemaan sinun olemassa olevia tilauksia.",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (result == MessageBoxResult.OK)
                    {

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

        private void GetOrder()
        {
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
