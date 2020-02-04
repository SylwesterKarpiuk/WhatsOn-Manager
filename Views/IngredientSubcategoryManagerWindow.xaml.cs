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
using System.Windows.Shapes;
using WhatsOnManager.ViewModel;

namespace WhatsOnManager.Views
{
    /// <summary>
    /// Interaction logic for IngredientSubcategoryManagerWindow.xaml
    /// </summary>
    public partial class IngredientSubcategoryManagerWindow : Window
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        List<IngredientsSubcategoryViewModel> ingredientsCategoryList;
        public IngredientSubcategoryManagerWindow()
        {
            InitializeComponent();
            LoadIngredientsList();
            LoadCategoriesList();
        }
        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            MenuWindow mainMenu = new MenuWindow();
            mainMenu.Show();
            this.Close();
        }
        private void LoadIngredientsList()
        {
            string sql = "Select name, category_name from IngredientsSubcategory;";
            connection = new SqlConnection(Settings.connectionString);
            connection.Open();
            command = new SqlCommand(sql, connection);
            ingredientsCategoryList = new List<IngredientsSubcategoryViewModel>();
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ingredientsCategoryList.Add(new IngredientsSubcategoryViewModel() { Name = reader.GetString(0), CategoryName = reader.GetString(1) });
                }
                connection.Close();
                SubcategoryListView.ItemsSource = ingredientsCategoryList;
                if (((List<IngredientsSubcategoryViewModel>)SubcategoryListView.ItemsSource).Count > 0)
                {
                    SubcategoryListView.SelectedItem = ((List<IngredientsSubcategoryViewModel>)SubcategoryListView.ItemsSource)[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia: " + ex.ToString());
            }
        }
        private void RefreshButtonClicked(object sender, RoutedEventArgs e)
        {
            LoadIngredientsList();
            LoadCategoriesList();
        }
        private void DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            IngredientsSubcategoryViewModel cat = (IngredientsSubcategoryViewModel)SubcategoryListView.SelectedItem;
            string sql = "Delete from IngredientsSubcategory where Name = \'" + cat.Name + "\';";
            connection = new SqlConnection(Settings.connectionString);
            connection.Open();
            command = new SqlCommand(sql, connection);
            try
            {
                reader = command.ExecuteReader();
                connection.Close();
                LoadIngredientsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia: " + ex.ToString());
            }
        }
        private void AddSubcategoryButtonClicked(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(NewSubcategoryReader.Text))
            {
                IngredientsCategoryViewModel cat = (IngredientsCategoryViewModel)CategoryListView.SelectedItem;
                if (cat != null)
                {
                    string sql = "Insert into IngredientsSubcategory values (\'" + NewSubcategoryReader.Text + "\', \'" + cat.Name + "\');";
                    connection = new SqlConnection(Settings.connectionString);
                    connection.Open();
                    command = new SqlCommand(sql, connection);
                    List<string> ingredientsCategoryList = new List<string>();
                    try
                    {
                        reader = command.ExecuteReader();
                        connection.Close();
                        LoadIngredientsList();
                        NewSubcategoryReader.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Błąd połączenia: " + ex.ToString());
                    }
                }

            }
        }//
        private void LoadCategoriesList()
        {
            string sql = "Select name from IngredientsCategory;";
            connection = new SqlConnection(Settings.connectionString);
            connection.Open();
            command = new SqlCommand(sql, connection);
            List<IngredientsCategoryViewModel> ingredientsCategoryList = new List<IngredientsCategoryViewModel>();
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ingredientsCategoryList.Add(new IngredientsCategoryViewModel() { Name = reader.GetString(0) });
                }
                connection.Close();
                CategoryListView.ItemsSource = ingredientsCategoryList;
                if (((List<IngredientsCategoryViewModel>)CategoryListView.ItemsSource).Count > 0)
                {
                    CategoryListView.SelectedItem = ((List<IngredientsCategoryViewModel>)CategoryListView.ItemsSource)[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia: " + ex.ToString());
            }
        }
    }
}
