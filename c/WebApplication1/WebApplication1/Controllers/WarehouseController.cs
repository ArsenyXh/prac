using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Npgsql;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public WarehouseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select id, address from warehouse";
            DataTable table = new DataTable();
            string connectionString = _configuration.GetConnectionString("AppCon");
            NpgsqlDataReader reader;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    reader = command.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Warehouse w)
        {
            string query = @"insert into warehouse (address) values (@address)";
            DataTable table = new DataTable();
            string connectionString = _configuration.GetConnectionString("AppCon");
            NpgsqlDataReader reader;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, connection))
                {
                    myCommand.Parameters.AddWithValue("@address", w.Address);
                    reader = myCommand.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }
            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Warehouse w)
        {
            string query = @"update warehouse set address= @address where id=@id";
            DataTable table = new DataTable();
            string connectionString = _configuration.GetConnectionString("AppCon");
            NpgsqlDataReader reader;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, connection))
                {
                    myCommand.Parameters.AddWithValue("@id", w.Id);
                    myCommand.Parameters.AddWithValue("@address", w.Address);
                    reader = myCommand.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from warehouse where id=@id";
            DataTable table = new DataTable();
            string connectionString = _configuration.GetConnectionString("AppCon");
            NpgsqlDataReader reader;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, connection))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    reader = myCommand.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }
            return new JsonResult("Deleted Successfully");
        }
    }
}
