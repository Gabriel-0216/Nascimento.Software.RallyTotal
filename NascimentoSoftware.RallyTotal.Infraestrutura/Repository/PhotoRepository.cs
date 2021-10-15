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
    public class PhotoRepository : IRepository<Photo>
    {
        public string GetConnection()
        {
            var connection = $@"Server=DESKTOP-2L16HEL\SQLEXPRESS;Database=RallyWorld;Trusted_Connection=True;MultipleActiveResultSets=true";
            return connection;
        }
        public int SearchFileName(string fileName)
        {
            var param = new DynamicParameters();
            param.Add("FileName", fileName);
            var query = @"SELECT PhotoId FROM PHOTO Where PhotoName = @FileName";
            var retorno = 0;
            using(var sql = new SqlConnection(GetConnection()))
            {
                try
                {
                    return retorno = (int)sql.ExecuteScalar(query, param: param, commandType: System.Data.CommandType.Text);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        public async Task<int> Add(Photo objeto)
        {
            var param = new DynamicParameters();
            var query = $@"INSERT INTO Photo(Title, PhotoName) VALUES (@Title, @Name)";
            param.Add("Title", "imagem");
            param.Add("Name", objeto.PhotoName);

            using(var sql = new SqlConnection(GetConnection()))
            {
                try
                {
                    var rows =  await sql.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
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
            var query = $@"DELETE FROM Photo WHERE PhotoId = @Id";
            param.Add("Id", id);

            using(var sql = new SqlConnection(GetConnection()))
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

        public async Task<IEnumerable<Photo>> GetAll()
        {
            var query = $@"SELECT PhotoId, Title, PhotoName FROM PHOTO";
            using(var sql = new SqlConnection(GetConnection()))
            {
                try
                {
                    var lista = await sql.QueryAsync<Photo>(query, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                    return lista;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<Photo> GetOne(int id)
        {
            var param = new DynamicParameters();
            var query = $@"SELECT PhotoId, Title, PhotoName FROM PHOTO where PhotoId = @Id";
            param.Add("Id", id);

            using (var sql = new SqlConnection(GetConnection()))
            {
                try
                {
                    var Photo = await sql.QueryFirstOrDefaultAsync<Photo>(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                    return Photo;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<int> Update(Photo objeto)
        {
            var param = new DynamicParameters();
            var query = $@"UPDATE Photo SET Title = @Title, PhotoName = @Name WHERE PhotoId = @Id";
            param.Add("Id", objeto.PhotoId);
            param.Add("Title", objeto.Title);
            param.Add("PhotoName", objeto.PhotoName);

            using(var sql = new SqlConnection(GetConnection()))
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
