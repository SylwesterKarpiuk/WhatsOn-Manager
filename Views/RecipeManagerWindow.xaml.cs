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

namespace WhatsOnManager.Views
{
    /// <summary>
    /// Interaction logic for RecipeManagerWindow.xaml
    /// </summary>
    public partial class RecipeManagerWindow : Window
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        public RecipeManagerWindow()
        {
            InitializeComponent();
        }

        private void executeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(MealName.Text) && !String.IsNullOrEmpty(Author.Text) && !String.IsNullOrEmpty(DiffLevel.Text) && !String.IsNullOrEmpty(Time.Text) && !String.IsNullOrEmpty(Rating.Text) && !String.IsNullOrEmpty(RateCount.Text) && !String.IsNullOrEmpty(Category.Text) && !String.IsNullOrEmpty(MainUrl.Text) && !String.IsNullOrEmpty(RecipeUrl.Text) && !String.IsNullOrEmpty(ImgUrl.Text))
            {

                string sql = "Select meal_name from dbo.Recipe where meal_name=\'" + MealName.Text +"\' and author=\'" + Author.Text+"\';";
                connection = new SqlConnection(Settings.connectionString);
                connection.Open();
                command = new SqlCommand(sql, connection);
                List<string> ingredientsCategoryList = new List<string>();
                try
                {
                    reader = command.ExecuteReader();
                    bool isnull = true;
                    while (reader.Read())
                    {
                        if(reader.GetString(0) != null)
                        {
                            isnull = false;
                        }
                    }
                    if (isnull)
                    {
                        reader.Close();
                        sql = "Insert into dbo.Recipe values (N\'" + MealName.Text + "\', N\'" + "" + "\'" + ", N\'" + Author.Text + "\'," + "N\'" + RecipeUrl.Text + "\'," + "N\'" + MainUrl.Text + "\'," + "N\'" + ImgUrl.Text + "\'," + "N\'" + DiffLevel.Text + "\'," + "N\'" + Time.Text + "\'," + "N\'" + Rating.Text + "\'," + "N\'" + RateCount.Text + "\'," + "N\'" + 0 + "\'," + "N\'" + 0 + "\'," + "N\'" + 0 + "\'," + "N\'" + Subcategory.Text + "\'," + "N\'" + Category.Text + "\'" + ");";
                        command = new SqlCommand(sql, connection);
                        reader = command.ExecuteReader();
                        reader.Close();
                        sql = "Select id from dbo.Recipe where meal_name =\'" + MealName.Text + "\' and author=\'" + Author.Text + "\';";
                        command = new SqlCommand(sql, connection);
                        reader = command.ExecuteReader();
                        int id = -1;
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) != null)
                            {
                                id = reader.GetInt32(0);
                            }
                        }
                        if (id != -1)
                        {
                            reader.Close();
                            sql = "Insert into dbo.RecipeIngredients values (N\'" + Ingredient01.Text + "\',N\'" + Ingredient02.Text + "\',N\'" + Ingredient03.Text + "\',N\'" + Ingredient04.Text + "\',N\'" + Ingredient05.Text + "\',N\'" + Ingredient06.Text + "\',N\'" + Ingredient07.Text + "\',N\'" + Ingredient08.Text + "\',N\'" + Ingredient09.Text + "\',N\'" + Ingredient10.Text + "\',N\'" + Ingredient11.Text + "\',N\'" + Ingredient12.Text + "\',N\'" + Ingredient13.Text + "\',N\'" + Ingredient14.Text + "\',N\'" + Ingredient15.Text + "\',N\'" + Ingredient16.Text + "\',N\'" + Ingredient17.Text + "\',N\'" + Ingredient18.Text + "\',N\'" + Ingredient19.Text + "\',N\'" + Ingredient20.Text + "\',N\'" + Ingredient21.Text + "\',N\'" + Ingredient22.Text + "\',N\'" + id + "\'); ";
                            command = new SqlCommand(sql, connection);
                            reader = command.ExecuteReader();
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd połączenia: " + ex.ToString());
                }


            }
        }
    }
}
