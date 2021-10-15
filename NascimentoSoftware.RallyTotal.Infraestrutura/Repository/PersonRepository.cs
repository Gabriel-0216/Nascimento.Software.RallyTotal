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
    public class PersonRepository : IRepository<Person>
    {
        public string GetConnection()
        {
            var connection = $@"Server=DESKTOP-2L16HEL\SQLEXPRESS;Database=RallyWorld;Trusted_Connection=True;MultipleActiveResultSets=true";
            return connection;
        }
        public async Task<int> Add(Person objeto)
        {
            var param = new DynamicParameters();
            var query = $@"INSERT INTO Person(PersonName, PhoneNumber, Country) VALUES (@PersonName, @PhoneNumber, @Country)";
            param.Add("PersonName", objeto.PersonName);
            param.Add("PhoneNumber", objeto.PhoneNumber);
            param.Add("Country", objeto.Country);

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
            var query = $@"DELETE FROM Person WHERE PersonId = @Id";
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

        public async Task<IEnumerable<Person>> GetAll()
        {
            var query = $@"SELECT PersonId, PersonName, PhoneNumber, Country FROM PERSON";
            using (var sql = new SqlConnection(GetConnection()))
            {
                try
                {
                    var lista = await sql.QueryAsync<Person>(query, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                    return lista;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<Person> GetOne(int id)
        {
            var param = new DynamicParameters();
            var query = $@"SELECT PersonId, PersonName, PhoneNumber, Country from PERSON WHERE PersonId = @Id";
            param.Add("Id", id);
            using (var sql = new SqlConnection(GetConnection()))
            {
                try
                {
                    var pessoa = await sql.QueryFirstOrDefaultAsync<Person>(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                    return pessoa;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<int> Update(Person objeto)
        {
            var param = new DynamicParameters();
            var query = $@"UPDATE Person SET PersonName = @Name, PhoneNumber = @Number, Country = @Country WHERE PersonId = @Id";
            param.Add("Id", objeto.PersonId);
            param.Add("Name", objeto.PersonName);
            param.Add("Number", objeto.PhoneNumber);
            param.Add("Country", objeto.Country);
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
