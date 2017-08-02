using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrafficStops.Models;

namespace TrafficStops.Controllers
{
    public class StopVarApiController : ApiController
    {

        private readonly ApplicationDbContext _context;

        public StopVarApiController() {

            _context = new ApplicationDbContext();
        }


        [HttpGet]
        public List<double> GetStats([FromUri] StopVars query)
        {

            List<double> stats = new List<double>();

            double ratio = 0;
            int allResults = 0;
            int activityResults = 0;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["firstDBString"].ToString();
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.LMPD_STOPS WHERE OfficerRace LIKE @officerRace AND OfficerAgeRange LIKE @officerAgeRange AND OfficerGender LIKE @officerGender AND DriverRace LIKE @driverRace AND DriverGender LIKE @driverGender AND DriverAgeRange LIKE @driverAgeRange", conn);
                cmd.Parameters.Add(new SqlParameter("officerRace", "%" + query.RaceOfficer + "%"));
                cmd.Parameters.Add(new SqlParameter("officerAgeRange", "%" + query.AgeOfficer + "%"));
                cmd.Parameters.Add(new SqlParameter("officerGender", "%" + query.GenderOfficer + "%"));
                cmd.Parameters.Add(new SqlParameter("driverRace", "%" + query.RaceDriver + "%"));
                cmd.Parameters.Add(new SqlParameter("driverAgeRange", "%" + query.AgeDriver + "%"));
                cmd.Parameters.Add(new SqlParameter("driverGender", "%" + query.GenderDriver + "%"));
                //cmd.Parameters.Add(new SqlParameter("activityResults", "%" + "" + "%"));





                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    allResults = (int)reader[0];
                }

                SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM dbo.LMPD_STOPS WHERE OfficerRace LIKE @officerRace AND OfficerAgeRange LIKE @officerAgeRange AND OfficerGender LIKE @officerGender AND DriverRace LIKE @driverRace AND DriverGender LIKE @driverGender AND DriverAgeRange LIKE @driverAgeRange AND ActivityResults LIKE @activityResults", conn);
                cmd2.Parameters.Add(new SqlParameter("officerRace", "%" + query.RaceOfficer + "%"));
                cmd2.Parameters.Add(new SqlParameter("officerAgeRange", "%" + query.AgeOfficer + "%"));
                cmd2.Parameters.Add(new SqlParameter("officerGender", "%" + query.GenderOfficer + "%"));
                cmd2.Parameters.Add(new SqlParameter("driverRace", "%" + query.RaceDriver + "%"));
                cmd2.Parameters.Add(new SqlParameter("driverAgeRange", "%" + query.AgeDriver + "%"));
                cmd2.Parameters.Add(new SqlParameter("driverGender", "%" + query.GenderDriver + "%"));
                cmd2.Parameters.Add(new SqlParameter("activityResults", "%" + query.Result + "%"));



                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    reader.Read();
                    activityResults = (int)reader[0];
                }

                ratio = (double)activityResults / (double)allResults;


            }

            var rounded = Math.Round(ratio, 2);


            stats.Add(Math.Round((rounded * 100), 2));
            stats.Add(allResults);
            stats.Add(activityResults);

            return stats;

        }



    }
}
