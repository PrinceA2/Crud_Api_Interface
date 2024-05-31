using CRUD_CORE_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;
using System.IO;

namespace CRUD_CORE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [Route("GetAllStudents")]
        [HttpGet]

        public async Task<IActionResult> GetAllStudents()
        {
            var studentModels = new List<Student_Model>();
            var header_section = new List<string>();

            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await con.OpenAsync();

            string query = "Select COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'API_TABLE'";

            SqlCommand cmd = new SqlCommand("GetUserData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Student_Model stmodel = new Student_Model();
                stmodel.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                stmodel.Sname = dt.Rows[i]["Sname"].ToString();
                stmodel.marks = Convert.ToInt32(dt.Rows[i]["marks"]);
                stmodel.Saddress = dt.Rows[i]["Saddress"].ToString();
                studentModels.Add(stmodel);
            }


            var file_path = "myfile.txt";

            using (SqlCommand cmd1 = new SqlCommand(query, con))
            {
                using (SqlDataReader dr = await cmd1.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        header_section.Add(dr["COLUMN_NAME"].ToString());
                    }
                    using (StreamWriter writer = new StreamWriter(file_path))
                    {
                            await writer.WriteLineAsync(string.Join("\t" + "|", header_section));
           
                        foreach (var user in studentModels)
                        {
                            await writer.WriteLineAsync(user.Id + " " + "|" + user.Sname + " " + "|" + user.marks + " " + "|" + user.Saddress);
                        }
                    }
                }
                con.Close();
            }
            return Ok(studentModels);
        }

        [Route("GetStudentsSpecific/{id}")]
        [HttpGet]

        public async Task <IActionResult> GetStudentsSpecific(int id)
        {
            List<Student_Model> specific_list = new List<Student_Model>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            con.Open();
            SqlCommand cmd = new SqlCommand("GetUserById",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr =  cmd.ExecuteReader();

                Student_Model mdl = new Student_Model()
                {
                    Id = (int)dr["Id"],
                    Sname = (string)dr["Sname"],
                    marks = (int)dr["marks"],
                    Saddress = (string)dr["Saddress"]
                };
            specific_list.Add(mdl);
            con.Close();
            
            return Ok(specific_list);
        }


        [Route("PostStudents")]
        [HttpPost]

        public async Task<IActionResult> PostStudents(Student_Model obj)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO API_TABLE VALUES (@Sname, @Marks, @Saddress)", con);
            cmd.Parameters.AddWithValue("@Sname", obj.Sname);
            cmd.Parameters.AddWithValue("@Marks", obj.marks);
            cmd.Parameters.AddWithValue("@Saddress", obj.Saddress);

            cmd.ExecuteNonQuery();
            con.Close();
            return Ok(obj);
        }

        [Route("PutStudents")]
        [HttpPut]
        
        public async Task<IActionResult> PutStudents(Student_Model obj)
        {

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            con.Open();
            SqlCommand cmd = new SqlCommand("Update API_TABLE SET Sname = @Sname, marks = @Marks, Saddress = @Saddress Where Id = @id", con);

            cmd.Parameters.AddWithValue("@id", obj.Id);
            cmd.Parameters.AddWithValue("@Sname", obj.Sname);
            cmd.Parameters.AddWithValue("@Marks", obj.marks);
            cmd.Parameters.AddWithValue("@Saddress", obj.Saddress);
            
            cmd.ExecuteNonQuery();
            con.Close();
            return Ok(obj);
        }

        [Route("DeleteStudents")]
        [HttpDelete]

        public async Task<IActionResult> DeleteStudents(Student_Model obj)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete From API_TABLE where Id = @id", con);
            cmd.Parameters.AddWithValue("@id", obj.Id);
            cmd.ExecuteNonQuery();
            con.Close();
            return Ok(obj);
        }
    }
}