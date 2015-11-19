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
    /// Interaction logic for Orderlist.xaml
    /// </summary>
    public partial class Orderlist : Window
    {
        string connStr = Kiinteistöpalvelufirman_sovellus.Properties.Settings.Default.Tietokanta;
        Registration register_form = new Registration();
        bool isInsertMode = false;
        bool isBeingEdited = false;

        public Orderlist()
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
            Window_Loaded();
            dg_Orders.DataContext = GET_ORDERS().DefaultView;
           
        }

        private void Window_Loaded()
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
        }

        private void dgEmp_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            isInsertMode = true;
        }

        private void dgEmp_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isBeingEdited = true;
        }

        public static DataTable GET_ORDERS()
        {

            using (MySqlConnection conn = new MySqlConnection(Kiinteistöpalvelufirman_sovellus.Properties.Settings.Default.Tietokanta))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Tilauslomake WHERE asiakas_id='" + Application.Current.Properties["user_id"].ToString() + "'", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet,"Orders");
                    return dataSet.Tables["Orders"];
                    
                }
                catch(Exception ex){
                    MessageBox.Show("Tilauksia ei voitu hakea. Yritä myöhemmin uudelleen. Virhe: " + ex.Message, "Virhe", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
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
            Orderlist order_list = new Orderlist();
            order_list.Show();
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

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            Order order_form = new Order();
            order_form.Show();
            Close();
        }
    }
}
