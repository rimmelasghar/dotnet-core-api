using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using WE_SECB_API.Models;
using Microsoft.Extensions.Configuration;

namespace WE_SECB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TimeSlotController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            DataTable dt = new TimeSlot().Get_All(_configuration);
            return new JsonResult(dt);
        }

        [HttpPost]
        public JsonResult Post(TimeSlot timeSlot)
        {
            string res = new TimeSlot().Insert_One(_configuration, timeSlot);
            return new JsonResult(res);
        }

        [HttpPut]
        public JsonResult Put(TimeSlot timeSlot)
        {
            string res = new TimeSlot().Update_One(_configuration, timeSlot);
            return new JsonResult(res);
        }
        
        [HttpDelete]
        public JsonResult Delete(TimeSlot timeSlot)
        {
            string res = new TimeSlot().Delete_One(_configuration, timeSlot);
            return new JsonResult("TimeSlot Deleted Successfully");
        }
    }
}
