using NascimentoSoftware.RallyTotal.Infraestrutura.Blog.Infraestrutura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace NascimentoSoftware.RallyTotal.Infraestrutura.Blog.Infraestrutura.Repository
{
    public class PostRepository : IRepository<Post>
    {
        public string GetConnection()
        {
            var connection = $@"Server=DESKTOP-2L16HEL\SQLEXPRESS;Database=RallyWorld;Trusted_Connection=True;MultipleActiveResultSets=true";
            return connection;
        }
        public async Task<int> Add(Post objeto)
        {
            var param = new DynamicParameters();
            param.Add("AutorId", objeto.AutorId);
            param.Add("Assunto", objeto.Assunto);
            param.Add("Texto", objeto.Texto);
            try
            {
                var query = $@"INSERT INTO Post (AutorId, Assunto, Texto) VALUES (@AutorId, @Assunto, @Texto)";
                using(var sql = new SqlConnection(GetConnection()))
                {
                    var rows = await sql.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                    return rows;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<int> Delete(int id)
        {
            var param = new DynamicParameters();
            param.Add("Id", id);
            try
            {
                var query = $@"DELETE FROM Post WHERE Id = @Id";
                using(var sql = new SqlConnection(GetConnection()))
                {
                    var rows = await sql.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                    return rows;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            try
            {
                using(var sql = new SqlConnection(GetConnection()))
                {
                    var query = $@"SELECT Id, AutorId, Assunto, Texto FROM Post";
                    var lista = await sql.QueryAsync<Post>(query, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<IEnumerable<Post>> GetTop10()
        {
            try
            {
                using(var sql = new SqlConnection(GetConnection()))
                {
                    var query = $@"SELECT TOP 10 Id, AutorId, Assunto, Texto FROM POST ORDER BY Id DESC";
                    var lista = await sql.QueryAsync<Post>(query, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<IEnumerable<Post>> GetTop5()
        {
            try
            {
                using(var sql = new SqlConnection(GetConnection()))
                {
                    var query = $@"SELECT TOP 5 Id, AutorId, Assunto, Texto FROM POST ORDER BY Id DESC";
                    var lista = await sql.QueryAsync<Post>(query, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Post> GetOne(int id)
        {
            var param = new DynamicParameters();
            param.Add("Id", id);
            try
            {
                using (var sql = new SqlConnection(GetConnection()))
                {
                    var query = $@"SELECT Id, AutorId, Assunto, Texto FROM POST WHERE Id = @Id";
                    var post = await sql.QueryFirstOrDefaultAsync<Post>(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                    return post;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> Update(Post objeto)
        {
            var param = new DynamicParameters();
            param.Add("Id", objeto.Id);
            param.Add("AutorId", objeto.AutorId);
            param.Add("Assunto", objeto.Assunto);
            param.Add("Texto", objeto.Texto);
            try
            {
                using(var sql = new SqlConnection(GetConnection()))
                {
                    var query = $@"UPDATE Post SET AutorId = @AutorId, Assunto = @Assunto, Texto = @Texto where Id = @Id";
                    var rows = await sql.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                    return rows;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
