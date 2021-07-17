using MovieProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.DAL.Services
{
    public class CategoryService
    {
        private readonly SqlConnection _conn;

        public CategoryService(SqlConnection conn)
        {
            _conn = conn;
        }

        public List<Category> Get()
        {
            try
            {
                _conn.Open();
                SqlCommand command = _conn.CreateCommand();
                command.CommandText = "SELECT * FROM Category";
                List<Category> result = new List<Category>();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Category
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"]
                    });
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
