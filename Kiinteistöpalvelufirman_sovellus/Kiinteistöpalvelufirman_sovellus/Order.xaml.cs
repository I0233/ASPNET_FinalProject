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
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Kiinteistöpalvelufirman_sovellus
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        public string radioButton_value;
        string connStr = Kiinteistöpalvelufirman_sovellus.Properties.Settings.Default.Tietokanta;
        MySqlCommand cmd;
        Registration register_form = new Registration();

        public Order()
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
            Page_onload();
        }
        public void Page_onload()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {        
		        conn.Open();
                 cmd = new MySqlCommand("SELECT COUNT(*) FROM Tilauslomake WHERE asiakas_id=(SELECT id From Asiakkaat WHERE id='" + Application.Current.Properties["user_id"].ToString() + "')", conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 0)
                {
                    new_user_info.Visibility = Visibility.Visible;
                    Ok_btn.Visibility = Visibility.Visible;
                    GetOrders.Visibility = Visibility.Hidden;
                }
	        }
            
            txtpvm.Text = DateTime.Today.ToShortDateString();
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
        }

        public void Back_in_editting_form(object sender, RoutedEventArgs e)
        {
            Order order_form = new Order();
            order_form.Show();
        }
        public void Edit_Click(object sender, RoutedEventArgs e)
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

                    errormessage.Text = "Käyttäjän tiedot ei voitu hakea. Virhe: " + ex.Message;
                }
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login_page = new MainWindow();
            login_page.Show();
            Close();
        }

        private void Ok_btn_Click(object sender, RoutedEventArgs e)
        {
            new_user_info.Visibility = Visibility.Hidden;
            Ok_btn.Visibility = Visibility.Hidden;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (txtAddress.Text != "" && txtBillingAddress.Text != "" && txtPostalCode.Text != "" && (Type1.IsChecked != true || Type2.IsChecked != true || Type3.IsChecked != true || Type4.IsChecked != true)  )
            {
                OrderForm.address = txtAddress.Text;
                OrderForm.billingAddress = txtBillingAddress.Text;
                OrderForm.postalCode = txtPostalCode.Text;
                OrderForm.comment = txtComment.Text;
                OrderForm.service = cmb_palvelut.SelectedItem.ToString();
                OrderForm.flatType = radioButton_value;
                Next.Visibility = Visibility.Visible;
                successmessage.Text = "Palvelu on tilattu onnistuneesti. Ole hyvä ja siirry seuraavalle sivulle vahvistaksesi palvelutilauksen ";
                
            }
            else
            {
                errormessage.Text = "Täytä lomakkeen puuttuvat tiedot";
            }
        }

        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
           radioButton_value = radioButton.Content.ToString(); 
        }

        private void Rest_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }


        private void cmb_palvelut_Loaded(object sender, RoutedEventArgs e)
        {
            // ... A List.
            List<string> Palvelut = new List<string>();
            Palvelut.Add("Konetyöt");
            Palvelut.Add("Käsilumityöt ja lumen tiputukset katolta");
            Palvelut.Add("Nurmikon leikkaus");
            Palvelut.Add("Puiden kaadot/polttopuiden teko");
            Palvelut.Add("Putki- ja sähkötyöt");
            Palvelut.Add("Autojen apuvirta ja hinaukset");
            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = Palvelut;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Confrmation confrmation_form = new Confrmation();
            confrmation_form.Show();
            Close();
        }

        private void Reset()
        {
            txtAddress.Clear();
            txtBillingAddress.Clear();
            txtComment.Clear();
            txtPostalCode.Clear();
            cmb_palvelut.SelectedIndex = 0;
        }

        private void GetOrders_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
