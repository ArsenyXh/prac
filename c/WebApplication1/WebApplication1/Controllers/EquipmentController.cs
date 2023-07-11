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
    public class EquipmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EquipmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select e.id, e.type, e.warehouseaddress, w.id, w.address from equipment e join warehouse w ON e.warehouseaddress = w.address";
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
        public JsonResult Post(Equipment e)
        {
            string query = @"insert into equipment (type, warehouseaddress) values (@type, @warehouseaddress)";
            DataTable table = new DataTable();
            string connectionString = _configuration.GetConnectionString("AppCon");
            NpgsqlDataReader reader;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, connection))
                {
                    myCommand.Parameters.AddWithValue("@type", e.Type);
                    myCommand.Parameters.AddWithValue("@warehouseaddress", e.Warehouseaddress);
                    reader = myCommand.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Equipment e)
        {
            string query = @"update equipment set type=@type, warehouseaddress=@warehouseaddress where id=@id";
            DataTable table = new DataTable();
            string connectionString = _configuration.GetConnectionString("AppCon");
            NpgsqlDataReader reader;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, connection))
                {
                    myCommand.Parameters.AddWithValue("@id", e.Id);
                    myCommand.Parameters.AddWithValue("@type", e.Type);
                    myCommand.Parameters.AddWithValue("@warehouseaddress", e.Warehouseaddress);
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
            string query = @"delete from equipment where id=@id";
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
