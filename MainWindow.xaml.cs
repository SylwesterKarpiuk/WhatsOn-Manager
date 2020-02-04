using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using WhatsOnManager.Views;

namespace WhatsOnManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        public MainWindow()
        {
            Settings.loggedInUser = "";
            InitializeComponent();
        }
        private void OnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SignInButton_Click(sender, e);
            }
        }
        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(Settings.connectionString);
            connection.Open();
            SqlCommand command;
            SqlDataReader reader;
            string sql = "Select id from Logins where Email = \'" + LoginField.Text + "\' and CAST(Password as varbinary(255)) = CAST(\'" + PasswordField.Password + "\' as varbinary(255))";
            command = new SqlCommand(sql, connection);

            try
            {
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Settings.loggedInUser = LoginField.Text;
                    MenuWindow menu = new MenuWindow();
                    menu.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wprowadzono błędny login, lub hasło");
                }
                connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Błąd połączenia: " + ex.ToString());
            }
        }
    }
}
