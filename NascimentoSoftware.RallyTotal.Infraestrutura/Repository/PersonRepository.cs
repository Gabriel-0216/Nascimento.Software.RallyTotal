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
        public int Add(Person objeto)
        {
            var param = new DynamicParameters();
            int rows = 0;
            var query = $@"INSERT INTO Person(PersonName, PhoneNumber, Country) VALUES (@PersonName, @PhoneNumber, @Country)";
            param.Add("PersonName", objeto.PersonName);
            param.Add("PhoneNumber", objeto.PhoneNumber);
            param.Add("Country", objeto.Country);

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
            var query = $@"DELETE FROM Person WHERE PersonId = @Id";
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

        public List<Person> GetAll()
        {
            var lista = new List<Person>();
            var query = $@"SELECT PersonId, PersonName, PhoneNumber, Country FROM PERSON";
            using (var connection = new SqlConnection(GetConnection()))
            {
                try
                {
                    lista = connection.Query<Person>(query).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return lista;
        }

        public Person GetOne(int id)
        {
            var pessoa = new Person();
            var param = new DynamicParameters();
            var query = $@"SELECT PersonId, PersonName, PhoneNumber, Country from PERSON WHERE PersonId = @Id";
            param.Add("Id", id);
            using (var connection = new SqlConnection(GetConnection()))
            {
                try
                {
                    pessoa = connection.Query<Person>(query, param: param, commandType: System.Data.CommandType.Text).FirstOrDefault();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return pessoa;
        }

        public int Update(Person objeto)
        {
            int rows = 0;
            var param = new DynamicParameters();
            var query = $@"UPDATE Person SET PersonName = @Name, PhoneNumber = @Number, Country = @Country WHERE PersonId = @Id";
            param.Add("Id", objeto.PersonId);
            param.Add("Name", objeto.PersonName);
            param.Add("Number", objeto.PhoneNumber);
            param.Add("Country", objeto.Country);
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
