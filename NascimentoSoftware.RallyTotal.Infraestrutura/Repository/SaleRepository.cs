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
    public class SaleRepository : IRepository<Sale>
    {
        public string GetConnection()
        {
            var connection = $@"Server=DESKTOP-2L16HEL\SQLEXPRESS;Database=RallyWorld;Trusted_Connection=True;MultipleActiveResultSets=true";
            return connection;
        }
        public int SellerExists(int PersonId)
        {
            int exists = 0;
            var param = new DynamicParameters();
            var query = "SELECT COUNT(SaleId) from SALE where PersonId = @PersonId";
            param.Add("PersonId", PersonId);
            using(var sql = new SqlConnection(GetConnection()))
            {
                exists = (int)sql.ExecuteScalar(query, param: param, commandType: System.Data.CommandType.Text);
            }

            return exists;
        }
        public async Task <int> Add(Sale objeto)
        {
            var param = new DynamicParameters();
            var query = $@"INSERT INTO SALE (SaleTitle, RegisterDate, UpdateDate, Country, Price, PersonId, CategoryId,
                Photo, DescriptionSale) VALUES (@SaleTitle, @RegisterDate, @UpdateDate, @Country, @Price, @PersonId,
                @CategoryId, @Photo, @Description)";
            param.Add("SaleTitle", objeto.SaleTitle);
            param.Add("RegisterDate", objeto.RegisterDate);
            param.Add("UpdateDate", objeto.UpdateDate);
            param.Add("Country", objeto.Country);
            param.Add("Price", objeto.Price);
            param.Add("PersonId", objeto.PersonID);
            param.Add("CategoryId", objeto.CategoryId);
            param.Add("Photo", objeto.Photo);
            param.Add("Description", objeto.DescriptionSale);

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

        public async Task<int> Delete(int id)
        {
            var param = new DynamicParameters();
            param.Add("Id", id);
            var query = $@"DELETE FROM SALE WHERE SaleId = @Id";
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

        public async Task<IEnumerable<Sale>> GetAll()
        {
            var query = $@"SELECT SaleTitle, SaleId, RegisterDate, UpdateDate, Country, Price, PersonId, CategoryId,
                Photo, DescriptionSale FROM SALE";
            using(var sql = new SqlConnection(GetConnection()))
            {
                try
                {
                    var listaVendas = await sql.QueryAsync<Sale>(query, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                    return listaVendas;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<Sale> GetOne(int id)
        {
            var param = new DynamicParameters();
            var query = $@"SELECT SaleTitle, SaleId, RegisterDate, UpdateDate, Country, Price, PersonId, CategoryId,
                Photo, DescriptionSale FROM SALE where SaleId = @Id";
            param.Add("Id", id);
            using(var sql = new SqlConnection(GetConnection()))
            {
                try
                {
                    var sale = await sql.QueryFirstOrDefaultAsync<Sale>(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
                    return sale;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<int> Update(Sale objeto)
        {
            var param = new DynamicParameters();
            var query = $@"UPDATE SALE SET SaleTitle = @Title, UpdateDate = @UpdateDate, Country = @Country, Price = @Price, PersonId = @PersonId, CategoryId = @CategoryId,
                           Photo = @Photo, DescriptionSale = @Description WHERE SaleId = @Id";
            param.Add("UpdateDate", objeto.UpdateDate);
            param.Add("Title", objeto.SaleTitle);
            param.Add("Country", objeto.Country);
            param.Add("Price", objeto.Price);
            param.Add("PersonId", objeto.PersonID);
            param.Add("CategoryId", objeto.CategoryId);
            param.Add("Photo", objeto.Photo);
            param.Add("Description", objeto.DescriptionSale);
            param.Add("Id", objeto.SaleId);

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
