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
    /// Logika interakcji dla klasy MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }
        public void IngredientManagerButtonClicked(object sender, RoutedEventArgs e)
        {
            IngredientManagerWindow menu = new IngredientManagerWindow();
            menu.Show();
            this.Close();
        }
        public void IngredientCategoryManagerButtonClicked(object sender, RoutedEventArgs e)
        {
            IngredientCategoryManagerWindow menu = new IngredientCategoryManagerWindow();
            menu.Show();
            this.Close();
        }
        public void IngredientSubcategoryManagerButtonClicked(object sender, RoutedEventArgs e)
        {
            IngredientSubcategoryManagerWindow menu = new IngredientSubcategoryManagerWindow();
            menu.Show();
            this.Close();
        }
        public void RecipeManagerButtonClicked(object sender, RoutedEventArgs e)
        {

        }
        public void RecipeCategoryButtonClicked(object sender, RoutedEventArgs e)
        {

        }
        public void PriceUpdateButtonClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
