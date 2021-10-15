using NascimentoSoftware.RallyTotal.Infraestrutura.Blog.Infraestrutura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace NascimentoSoftware.RallyTotal.Infraestrutura.Blog.Infraestrutura.Repository
{
    public class AutorRepository : IRepository<Autor>
    {
        public async Task<IEnumerable<Autor>> GetAll()
        {
            using (var sql = new SqlConnection(GetConnection()))
            {
                var query = $@"SELECT * FROM Autor";
                var autores = await sql.QueryAsync<Autor>(query).ConfigureAwait(false);
                return autores;
            }
        } 

        public async Task<int> Add(Autor objeto)
        {
            var param = new DynamicParameters();
            param.Add("Nome", objeto.Nome);

            var query = $@"INSERT INTO Autor (Nome) VALUES @Nome";
            try
            {
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
            throw new NotImplementedException();
        }

       

        public string GetConnection()
        {
            var connection = $@"Server=DESKTOP-2L16HEL\SQLEXPRESS;Database=RallyWorld;Trusted_Connection=True;MultipleActiveResultSets=true";
            return connection;
        }

        public async Task<Autor> GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(Autor objeto)
        {
            throw new NotImplementedException();
        }
    }
}
