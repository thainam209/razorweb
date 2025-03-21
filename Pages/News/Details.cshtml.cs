using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webdemo.Data;
using System.Data.SqlClient;

namespace webdemo.Pages.News
{
    public class DetailsModel : PageModel
    {
        public Article Article { get; set; }

        public void OnGet(int id)
        {
            Dbconnect db = new Dbconnect();
            db.OpenCon();

            string query = "SELECT Id, Title, Content FROM tintuc WHERE Id = @Id";
            using (SqlCommand cmd = new SqlCommand(query, db.getConnect()))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Article = new Article
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Content = reader.GetString(2)
                        };
                    }
                }
            }

            db.CloseCon();
        }
    }

    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
