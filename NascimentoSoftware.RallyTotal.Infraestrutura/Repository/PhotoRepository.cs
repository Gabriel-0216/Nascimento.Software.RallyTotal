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
        public int Add(Photo objeto)
        {
            int rows = 0;
            var param = new DynamicParameters();
            var query = $@"INSERT INTO Photo(Title, PhotoName) VALUES (@Title, @Name)";
            param.Add("Title", "imagem");
            param.Add("Name", objeto.PhotoName);

            using(var connection = new SqlConnection(GetConnection()))
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
            var query = $@"DELETE FROM Photo WHERE PhotoId = @Id";
            param.Add("Id", id);

            using(var connection = new SqlConnection(GetConnection()))
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

        public List<Photo> GetAll()
        {
            var lista = new List<Photo>();
            var query = $@"SELECT PhotoId, Title, PhotoName FROM PHOTO";
            using(var connection = new SqlConnection(GetConnection()))
            {
                try
                {
                    lista = connection.Query<Photo>(query, commandType: System.Data.CommandType.Text).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return lista;
        }

        public Photo GetOne(int id)
        {
            var Photo = new Photo();
            var param = new DynamicParameters();
            var query = $@"SELECT PhotoId, Title, PhotoName FROM PHOTO where PhotoId = @Id";
            param.Add("Id", id);

            using (var connection = new SqlConnection(GetConnection()))
            {
                try
                {
                    Photo = connection.Query<Photo>(query, param: param, commandType: System.Data.CommandType.Text).FirstOrDefault();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return Photo;
        }

        public int Update(Photo objeto)
        {
            var rows = 0;
            var param = new DynamicParameters();
            var query = $@"UPDATE Photo SET Title = @Title, PhotoName = @Name WHERE PhotoId = @Id";
            param.Add("Id", objeto.PhotoId);
            param.Add("Title", objeto.Title);
            param.Add("PhotoName", objeto.PhotoName);

            using(var connection = new SqlConnection(GetConnection()))
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
