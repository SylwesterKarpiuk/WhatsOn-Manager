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
            LoadIngredientsList();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            MenuWindow mainMenu = new MenuWindow();
            mainMenu.Show();
            this.Close();
        }
        private void RefreshButtonClicked(object sender, RoutedEventArgs e)
        {
            LoadIngredientsList();
        }
        private void DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            IngredientsCategoryViewModel cat = (IngredientsCategoryViewModel)CategoryListView.SelectedItem;
            string sql = "Delete from IngredientsCategory where name = N\'" + cat.Name + "\';";
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
        private void AddCategoryButtonClicked(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(NewCategoryReader.Text))
            {
                string sql = "Insert into IngredientsCategory values (N\'" + NewCategoryReader.Text + "\');";
                connection = new SqlConnection(Settings.connectionString);
                connection.Open();
                command = new SqlCommand(sql, connection);
                List<string> ingredientsCategoryList = new List<string>();
                try
                {
                    reader = command.ExecuteReader();
                    connection.Close();
                    LoadIngredientsList();
                    NewCategoryReader.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd połączenia: " + ex.ToString());
                }
            }
        }
        private void LoadIngredientsList()
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
                if (((List<IngredientsCategoryViewModel>)CategoryListView.ItemsSource).Count>0){
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
