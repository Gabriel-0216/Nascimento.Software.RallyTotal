using Dapper;
using NascimentoSoftware.RallyTotal.Infraestrutura.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NascimentoSoftware.RallyTotal.Infraestrutura.Repository
{
    public class CategoryRepository : IRepository<Category>
    {
        public string GetConnection()
        {
            var connection = $@"Server=DESKTOP-2L16HEL\SQLEXPRESS;Database=RallyWorld;Trusted_Connection=True;MultipleActiveResultSets=true";
            return connection;
        }

        public int Add(Category objeto)
        {
            int rows = 0;
            var param = new DynamicParameters();
            var query = $@"INSERT INTO Category(CategoryName, RegisterDate, UpdateDate) VALUES (@Name, @Register, @Update)";
            param.Add("Name", objeto.CategoryName);
            param.Add("Register", objeto.RegisterDate);
            param.Add("Update", objeto.UpdateDate);
            using (var connection = new SqlConnection(GetConnection()))
            {
                try
                {
                    rows = connection.Execute(query, param: param, commandType: System.Data.CommandType.Text);
                }
                catch (Exception)
                {
                    return 0;
                }
            }

            return rows;
        }

        public int Delete(int id)
        {
            int rows = 0;
            var param = new DynamicParameters();
            var query = $@"DELETE FROM Category WHERE CategoryId = @Id";
            param.Add("Id", id);
            using (var connection = new SqlConnection(GetConnection()))
            {
                try
                {
                    rows = connection.Execute(query, param: param, commandType: System.Data.CommandType.Text);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            return rows;
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            var lista = new List<Category>();
            var query = $@"SELECT CategoryId, CategoryName, RegisterDate, UpdateDate from Category";
            using (var connection = new SqlConnection(GetConnection()))
            {
                try
                {
                    lista = connection.Query<Category>(query).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return lista;
        }

        public Category GetOne(int id)
        {

            var category = new Category();
            var param = new DynamicParameters();
            var query = $@"SELECT CategoryId, CategoryName, RegisterDate, UpdateDate from Category Where CategoryId = @Id";
            param.Add("Id", id);
            using (var connection = new SqlConnection(GetConnection()))
            {
                try
                {
                    category = connection.Query<Category>(query, param: param, commandType: System.Data.CommandType.Text).FirstOrDefault();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return category;
        }

        public int Update(Category objeto)
        {
            int rows = 0;
            var param = new DynamicParameters();
            var query = $@"UPDATE Category SET CategoryName = @Name, UpdateDate = @Update WHERE CategoryId = @Id";
            param.Add("Id", objeto.CategoryId);
            param.Add("Name", objeto.CategoryName);
            param.Add("Update", objeto.UpdateDate);

            using (var connection = new SqlConnection(GetConnection()))
            {
                try
                {
                    rows = connection.Execute(query, param: param, commandType: System.Data.CommandType.Text);
                }
                catch (Exception)
                {
                    return 0;
                }
            }

            return rows;
        }
    }
}
