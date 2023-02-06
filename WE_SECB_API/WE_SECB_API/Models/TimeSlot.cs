using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WE_SECB_API.Models
{
    public class TimeSlot
    {
        public int TSId { get; set; }
        public string TSCode { get; set; }
        public TimeSpan StartTime  { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Status { get; set; }
        public int RID { get; set; }

        public DataTable Get_All(IConfiguration configuration)
        {
            string query = @"SELECT t.TSId,t.TSCode,t.StartTime,t.EndTime,r.RID,r.RoomName from tbl_TimeSlot t INNER JOIN tbl_Room r ON t.RID = r.RID  WHERE t.Status = 1 ORDER BY 1 DESC";
            //string query = @"SELECT * FROM tbl_TimeSlot WHERE Status = 1";
            DataTable dt = new DataTable();
            SqlDataReader sqlDataReader;
            SqlConnection myCon = new SqlConnection(configuration.GetConnectionString("AttendanceAppCon"));
            using (myCon)
            {
                myCon.Open();
                SqlCommand sc = new SqlCommand(query, myCon);
                using (sc)
                {
                    sqlDataReader = sc.ExecuteReader();
                    dt.Load(sqlDataReader);
                    sqlDataReader.Close();
                    myCon.Close();
                }
            }
            return dt;
        }

        public String Insert_One(IConfiguration configuration, TimeSlot timeSlot)
        {
            string query = @"INSERT INTO tbl_TimeSlot VALUES(@TSCODE,@STARTTIME,@ENDTIME,1,@RID)";
            DataTable dt = new DataTable();
            SqlDataReader sqlDataReader;
            SqlConnection myCon = new SqlConnection(configuration.GetConnectionString("AttendanceAppCon"));
            using (myCon)
            {
                myCon.Open();
                SqlCommand sc = new SqlCommand(query, myCon);
                using (sc)
                {
                    sc.Parameters.AddWithValue("@TSCODE", timeSlot.TSCode);
                    sc.Parameters.AddWithValue("@STARTTIME", timeSlot.StartTime);
                    sc.Parameters.AddWithValue("@ENDTIME", timeSlot.EndTime);
                    sc.Parameters.AddWithValue("@RID", timeSlot.RID);
                    sqlDataReader = sc.ExecuteReader();
                    dt.Load(sqlDataReader);
                    sqlDataReader.Close();
                    myCon.Close();
                }
            }
            return "TimeSlot Added Successfully";
        }

        public String Update_One(IConfiguration configuration, TimeSlot timeSlot)
        {
            string query = @"UPDATE tbl_TimeSlot SET
                                TSCode= @TSCODE,
                                StartTime= @STARTTIME,
                                ENDTIME= @ENDTIME,
                                RID= @RID WHERE TSId = @TSID";
            DataTable dt = new DataTable();
            SqlDataReader sqlDataReader;
            SqlConnection myCon = new SqlConnection(configuration.GetConnectionString("AttendanceAppCon"));
            using (myCon)
            {
                myCon.Open();
                SqlCommand sc = new SqlCommand(query, myCon);
                using (sc)
                {
                    sc.Parameters.AddWithValue("@TSCODE", timeSlot.TSCode);
                    sc.Parameters.AddWithValue("@STARTTIME", timeSlot.StartTime);
                    sc.Parameters.AddWithValue("@ENDTIME", timeSlot.EndTime);
                    sc.Parameters.AddWithValue("@RID", timeSlot.RID);
                    sc.Parameters.AddWithValue("@TSID", timeSlot.TSId);
                    sqlDataReader = sc.ExecuteReader();
                    dt.Load(sqlDataReader);
                    sqlDataReader.Close();
                    myCon.Close();
                }
            }
            return "TimeSlot Updated SuccessFully";
        }
        public String Delete_One(IConfiguration configuration, TimeSlot timeSlot)
        {

            string query = @"UPDATE tbl_TimeSlot SET Status = 0 WHERE TSID = @TSID";
            DataTable dt = new DataTable();
            SqlDataReader sqlDataReader;
            using (SqlConnection myCon = new SqlConnection(configuration.GetConnectionString("AttendanceAppCon")))
            {
                myCon.Open();
                using (SqlCommand sc = new SqlCommand(query, myCon))
                {
                    sc.Parameters.AddWithValue("@TSID", timeSlot.TSId);
                    sqlDataReader = sc.ExecuteReader();
                    dt.Load(sqlDataReader);
                    sqlDataReader.Close();
                    myCon.Close();
                }
            }
            return ("TimeSlot Deleted Successfully");
        }
    }
}
