using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using WhatsOnManager.ViewModel;

namespace WhatsOnManager.Views
{
    /// <summary>
    /// Logika interakcji dla klasy IngredientCategoryManagerWindow.xaml
    /// </summary>
    public partial class IngredientCategoryManagerWindow : Window
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
       // ListViewItem SelectedItem { get { return (CategoryListView.SelectedItems.Count > 0 ? CategoryListView.SelectedItems[0] : null); } }
        public IngredientCategoryManagerWindow()
        {
            InitializeComponent();
            CategoryListView.ItemsSource = LoadIngredientsList();

        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        { }
        private void RefreshButtonClicked(object sender, RoutedEventArgs e)
        {
            CategoryListView.ItemsSource = LoadIngredientsList();
        }
        private void DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            
            string sql = "Delete from IngredientsCategory where Name = \'" + CategoryListView.SelectedItem + "\';";
            connection = new SqlConnection(Settings.connectionString);
            connection.Open();
            command = new SqlCommand(sql, connection);
            try
            {
                reader = command.ExecuteReader();
                connection.Close();
                CategoryListView.ItemsSource = LoadIngredientsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia: " + ex.ToString());
            }
        }
        private void AddCategoryButtonClicked(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(NewCategoryReader.Text))
            {
                string sql = "Insert into IngredientsCategory values (\'" + NewCategoryReader.Text + "\');";
                connection = new SqlConnection(Settings.connectionString);
                connection.Open();
                command = new SqlCommand(sql, connection);
                List<string> ingredientsCategoryList = new List<string>();
                try
                {
                    reader = command.ExecuteReader();
                    connection.Close();
                    CategoryListView.ItemsSource = LoadIngredientsList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd połączenia: " + ex.ToString());
                }
            }
        }
        private List<IngredientsCategoryViewModel> LoadIngredientsList()
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia: " + ex.ToString());
            }
            return ingredientsCategoryList;
        }
    }
}
