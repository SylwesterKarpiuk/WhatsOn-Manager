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
    /// Interaction logic for IngredientManagerWindow.xaml
    /// </summary>
    public partial class IngredientManagerWindow : Window
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        List<IngredientsSubcategoryViewModel> ingredientsCategoryList;
        List<IngredientsViewModel> ingredientsViewModel;
        IngredientsCategoryViewModel cat;
        IngredientsSubcategoryViewModel subcat;
        public IngredientManagerWindow()
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
            string sql = "Select name, category_name, main_category_name from IngredientsList;";
            connection = new SqlConnection(Settings.connectionString);
            connection.Open();
            command = new SqlCommand(sql, connection);
            ingredientsViewModel = new List<IngredientsViewModel>();
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ingredientsViewModel.Add(new IngredientsViewModel() { Name = reader.GetString(0), SubcategoryName = reader.GetString(1), CategoryName = reader.GetString(2) });
                }
                connection.Close();
                IngredientsListView.ItemsSource = ingredientsViewModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia: " + ex.ToString());
            }
        }
        private void LoadSubcategoriesList(string categoryName)
        {
            string sql = "Select name, category_name from IngredientsSubcategory where category_name = \'" + categoryName + "\';";
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia: " + ex.ToString());
            }
        }
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
            IngredientsViewModel cat = (IngredientsViewModel)IngredientsListView.SelectedItem;
            string sql = "Delete from IngredientsList where name = \'" + cat.Name + "\';";
            connection = new SqlConnection(Settings.connectionString);
            connection.Open();
            command = new SqlCommand(sql, connection);
            try
            {
                reader = command.ExecuteReader();
                connection.Close();
                RefreshButtonClicked(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia: " + ex.ToString());
            }
        }
        private void AddIngredientButtonClicked(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(NewIngredientReader.Text))
            {
                IngredientsCategoryViewModel cat = (IngredientsCategoryViewModel)CategoryListView.SelectedItem;
                IngredientsSubcategoryViewModel subcat = (IngredientsSubcategoryViewModel)SubcategoryListView.SelectedItem;
                if (cat != null && subcat != null)
                {
                    connection = new SqlConnection(Settings.connectionString);
                    connection.Open();
                    string sql = "select name from IngredientsList where name = \'" + NewIngredientReader.Text + "\'";
                    command = new SqlCommand(sql, connection);
                    try
                    {
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if(reader.GetString(0) !=null)
                            {
                                connection.Close();
                                return;
                            }
                        }
                       
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Błąd połączenia: " + ex.ToString());
                    }
                   
                    sql = "Insert into IngredientsList values (\'" + NewIngredientReader.Text + "\', \'" + cat.Name + "\', \'" + subcat.Name + "\');";
                    connection.Open();
                    command = new SqlCommand(sql, connection);
                    List<string> ingredientsCategoryList = new List<string>();
                    try
                    {
                        reader = command.ExecuteReader();
                        connection.Close();
                        RefreshButtonClicked(sender, e);
                        NewIngredientReader.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Błąd połączenia: " + ex.ToString());
                    }
                }

            }
        }//

        private void CategoryListViewChosen(object sender, MouseButtonEventArgs e)
        {

                cat = (IngredientsCategoryViewModel)CategoryListView.SelectedItem;
                LoadSubcategoriesList(cat.Name);
           
        }
    }
}
