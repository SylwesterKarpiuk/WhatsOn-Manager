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

namespace WhatsOnManager.Views
{
    /// <summary>
    /// Interaction logic for IngredientSubcategoryManagerWindow.xaml
    /// </summary>
    public partial class IngredientSubcategoryManagerWindow : Window
    {
        public IngredientSubcategoryManagerWindow()
        {
            InitializeComponent();
            MenuWindow mainMenu = new MenuWindow();
            mainMenu.Show();
            this.Close();
        }
        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            MenuWindow mainMenu = new MenuWindow();
            mainMenu.Show();
            this.Close();
        }
    }
}
