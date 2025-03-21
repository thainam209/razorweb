using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webdemo.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace webdemo.Pages
{
    public class NewsModel : PageModel
    {
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Content { get; set; }

        public List<Article> Articles { get; set; }

        public void OnGet()
        {
            Articles = new List<Article>();
            Dbconnect db = new Dbconnect();
            db.OpenCon();

            string query = "SELECT Id, Title FROM tintuc";
            using (SqlCommand cmd = new SqlCommand(query, db.getConnect()))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Articles.Add(new Article
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1)
                        });
                    }
                }
            }

            db.CloseCon();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Dbconnect db = new Dbconnect();
            db.OpenCon();

            string query = "INSERT INTO tintuc (Title, Content) VALUES (@Title, @Content)";
            using (SqlCommand cmd = new SqlCommand(query, db.getConnect()))
            {
                cmd.Parameters.AddWithValue("@Title", Title);
                cmd.Parameters.AddWithValue("@Content", Content);
                cmd.ExecuteNonQuery();
            }

            db.CloseCon();

            return RedirectToPage("/News");
        }
    }

    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
