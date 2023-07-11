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
    public class KartController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public KartController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select k.id, k.type, k.warehouseaddress, w.id, w.address from kart k join warehouse w ON k.warehouseaddress = w.address";
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
        public JsonResult Post(Kart k)
        {
            string query = @"insert into kart (type, warehouseaddress) values (@type, @warehouseaddress)";
            DataTable table = new DataTable();
            string connectionString = _configuration.GetConnectionString("AppCon");
            NpgsqlDataReader reader;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, connection))
                {
                    myCommand.Parameters.AddWithValue("@type", k.Type);
                    myCommand.Parameters.AddWithValue("@warehouseaddress", k.Warehouseaddress);
                    reader = myCommand.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Kart k)
        {
            string query = @"update kart set type=@type, warehouseaddress=@warehouseaddress where id=@id";
            DataTable table = new DataTable();
            string connectionString = _configuration.GetConnectionString("AppCon");
            NpgsqlDataReader reader;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, connection))
                {
                    myCommand.Parameters.AddWithValue("@id", k.Id);
                    myCommand.Parameters.AddWithValue("@type", k.Type);
                    myCommand.Parameters.AddWithValue("@warehouseaddress", k.Warehouseaddress);
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
            string query = @"delete from kart where id=@id";
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
