using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using WE_SECB_API.Models;

namespace WE_SECB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CourseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT  *   FROM    tbl_Course WHERE Status = 1 ORDER BY 1 DESC";
            DataTable dt = new DataTable();
            SqlDataReader sqlDataReader;
            using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("AttendanceAppCon")))
            {
                myCon.Open();
                using (SqlCommand sc = new SqlCommand(query, myCon))
                {
                    sqlDataReader = sc.ExecuteReader();
                    dt.Load(sqlDataReader);
                    sqlDataReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(dt);
        }

        [HttpPost]
        public JsonResult Post(Course course)
        {
            string query = @"INSERT INTO tbl_Course VALUES(@CourseName, @CourseFee, @CreditHour,@CourseCode, 1)";
            DataTable dt = new DataTable();
            SqlDataReader sqlDataReader;
            using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("AttendanceAppCon")))
            {
                myCon.Open();
                using (SqlCommand sc = new SqlCommand(query, myCon))
                {
                    sc.Parameters.AddWithValue("@CourseName", course.CourseName);
                    sc.Parameters.AddWithValue("@CourseFee", course.CourseFee);
                    sc.Parameters.AddWithValue("@CreditHour", course.CreditHour);
                    sc.Parameters.AddWithValue("@CourseCode", course.CourseCode);
                    sqlDataReader = sc.ExecuteReader();
                    dt.Load(sqlDataReader);
                    sqlDataReader.Close();
                    myCon.Close();
                }
            }
            //SqlCommand sc = new SqlCommand(query, Connection.GetSqlConnection());
            //sc.ExecuteNonQuery();
            return new JsonResult("Course Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Course course)
        {
            string query = @"UPDATE tbl_Course 
                            SET CourseName = @CourseName, 
                                CourseFee = @CourseFee, 
                                CreditHour = @CreditHour,
                                CourseCode = @CourseCode
                            WHERE CourseID = @CourseID";
            //SqlCommand sc = new SqlCommand(query, Connection.GetSqlConnection());
            //sc.Parameters.AddWithValue("@TSCode", timeSlot.TSCode);
            //sc.Parameters.AddWithValue("@StartTime", timeSlot.StartTime);
            //sc.Parameters.AddWithValue("@EndTime", timeSlot.EndTime);
            //sc.Parameters.AddWithValue("@TSId", timeSlot.TSId);
            //sc.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataReader sqlDataReader;
            using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("AttendanceAppCon")))
            {
                myCon.Open();
                using (SqlCommand sc = new SqlCommand(query, myCon))
                {
                    sc.Parameters.AddWithValue("@CourseName", course.CourseName);
                    sc.Parameters.AddWithValue("@CourseFee", course.CourseFee);
                    sc.Parameters.AddWithValue("@CreditHour", course.CreditHour);
                    sc.Parameters.AddWithValue("@CourseCode", course.CourseCode);
                    sc.Parameters.AddWithValue("@CourseID", course.CourseID);
                    sqlDataReader = sc.ExecuteReader();
                    dt.Load(sqlDataReader);
                    sqlDataReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Course Updated Successfully");
        }

        [HttpDelete]
        public JsonResult Delete(Course course)
        {
            string query = @"UPDATE tbl_Course SET Status = 0 WHERE CourseID = @CourseID";
            //SqlCommand sc = new SqlCommand(query, Connection.GetSqlConnection());
            //sc.Parameters.AddWithValue("@TSId", timeSlot.TSId);
            //sc.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataReader sqlDataReader;
            using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("AttendanceAppCon")))
            {
                myCon.Open();
                using (SqlCommand sc = new SqlCommand(query, myCon))
                {
                    sc.Parameters.AddWithValue("@CourseID", course.CourseID);
                    sqlDataReader = sc.ExecuteReader();
                    dt.Load(sqlDataReader);
                    sqlDataReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Course Deleted Successfully");
        }
    }
}
