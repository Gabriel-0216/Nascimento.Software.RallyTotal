﻿using Dapper;
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
        public int Add(Sale objeto)
        {
            int rows = 0;
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
                    rows = sql.Execute(query, param: param, commandType: System.Data.CommandType.Text);
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
            var rows = 0;
            var param = new DynamicParameters();
            param.Add("Id", id);
            var query = $@"DELETE FROM SALE WHERE ID = @Id";
            using(var sql = new SqlConnection(GetConnection()))
            {
                try
                {
                    rows = sql.Execute(query, param: param, commandType: System.Data.CommandType.Text);
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

        public List<Sale> GetAll()
        {
            var listaVendas = new List<Sale>();
            var query = $@"SELECT SaleTitle, SaleId, RegisterDate, UpdateDate, Country, Price, PersonId, CategoryId,
                Photo, DescriptionSale FROM SALE";
            using(var sql = new SqlConnection(GetConnection()))
            {
                try
                {
                    listaVendas = sql.Query<Sale>(query, commandType: System.Data.CommandType.Text).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return listaVendas;

        }

        public Sale GetOne(int id)
        {
            var sale = new Sale();
            var param = new DynamicParameters();
            var query = $@"SELECT SaleTitle, SaleId, RegisterDate, UpdateDate, Country, Price, PersonId, CategoryId
                Photo, DescriptionSale FROM SALE where SaleId = @Id";
            param.Add("Id", id);
            using(var sql = new SqlConnection(GetConnection()))
            {
                try
                {
                    sale = sql.Query<Sale>(query, param: param, commandType: System.Data.CommandType.Text).FirstOrDefault();
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return sale;
        }

        public int Update(Sale objeto)
        {
            int rows = 0;
            var param = new DynamicParameters();
            var query = $@"UPDATE SALE SET SaleTitle = @Title, UpdateDate = @UpdateDate, Country = @Country, Price = @Price, PersonId = @PersonId, CategoryId = @CategoryId
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
                    rows = sql.Execute(query, param: param, commandType: System.Data.CommandType.Text);
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
