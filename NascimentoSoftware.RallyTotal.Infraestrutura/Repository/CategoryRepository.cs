using Dapper;
using NascimentoSoftware.RallyTotal.Infraestrutura.Models;
using System;
using System.Collections;
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

        public async Task<int> Add(Category objeto)
        {
            var param = new DynamicParameters();
            var query = $@"INSERT INTO Category(CategoryName, RegisterDate, UpdateDate) VALUES (@Name, @Register, @Update)";
            param.Add("Name", objeto.CategoryName);
            param.Add("Register", objeto.RegisterDate);
            param.Add("Update", objeto.UpdateDate);
            using (var sql = new SqlConnection(GetConnection()))
            {
                try
                {
                    var rows = await sql.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                    return rows;

                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public async Task<int> Delete(int id)
        {
            var param = new DynamicParameters();
            var query = $@"DELETE FROM Category WHERE CategoryId = @Id";
            param.Add("Id", id);
            using (var sql = new SqlConnection(GetConnection()))
            {
                try
                {
                    var rows = await sql.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                    return rows;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {          
            using (var sql = new SqlConnection(GetConnection()))
            {
                try
                {
                    var query = $@"SELECT CategoryId, CategoryName, RegisterDate, UpdateDate from Category";
                    var list = await sql.QueryAsync<Category>(query).ConfigureAwait(false);
                    return list;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<Category> GetOne(int id)
        {
            var param = new DynamicParameters();
            var query = $@"SELECT CategoryId, CategoryName, RegisterDate, UpdateDate from Category Where CategoryId = @Id";
            param.Add("Id", id);
            using (var sql = new SqlConnection(GetConnection()))
            {
                try
                {
                    var category = await sql.QueryFirstOrDefaultAsync<Category>(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                   return category;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<int> Update(Category objeto)
        {
            var param = new DynamicParameters();
            var query = $@"UPDATE Category SET CategoryName = @Name, UpdateDate = @Update WHERE CategoryId = @Id";
            param.Add("Id", objeto.CategoryId);
            param.Add("Name", objeto.CategoryName);
            param.Add("Update", objeto.UpdateDate);

            using (var sql = new SqlConnection(GetConnection()))
            {
                try
                {
                    var rows = await sql.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                    return rows;
                }
                catch (Exception)
                {
                    return 0;
                }
            }

        }
    }
}
