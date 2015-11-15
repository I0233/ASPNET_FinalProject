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

namespace Kiinteistöpalvelufirman_sovellus
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        
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
        public void Page_onload(){
            new_user_info.Visibility = Visibility.Visible;
            Ok_btn.Visibility = Visibility.Visible;
            txtpvm.Text = DateTime.Today.ToShortDateString();
            var man_uri = new Uri("pack://application:,,,/Images/gender-man.png");
            var woman_uri = new Uri("pack://application:,,,/Images/gender-woman.png");
            var man_icon = new BitmapImage(man_uri);
            var woman_icon = new BitmapImage(woman_uri);
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

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

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

        }

        private void Rest_Click(object sender, RoutedEventArgs e)
        {

        }


        private void cmb_palvelut_Loaded(object sender, RoutedEventArgs e)
        {
            // ... A List.
            List<string> data = new List<string>();
            data.Add("Konetyöt");
            data.Add("Käsilumityöt ja lumen tiputukset katolta");
            data.Add("Nurmikon leikkaus");
            data.Add("Puiden kaadot/polttopuiden teko");
            data.Add("Putki- ja sähkötyöt");
            data.Add("Autojen apuvirta ja hinaukset");
            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
        }
    }
}
