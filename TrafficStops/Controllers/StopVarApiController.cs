using System;
using System.Collections.Generic;
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

        //[HttpGet]
        //public List<double> GetStats([FromUri]StopVars query)
        //{
        //    query.SetToEmptyString();

        //    List<double> stats = new List<double>();

        //    var allStops = _context.StopEntries.Where(s => s.DriverAgeRange != null)
        //                                        .Where(s => s.DriverGender != null)
        //                                        .Where(s => s.DriverRace != null)
        //                                        .Where(s => s.OfficerGender != null)
        //                                        .Where(s => s.OfficerRace != null)
        //                                        .Where(s => s.OfficerAgeRange != null)
        //                                        .Where(s => s.ActivityResults != null)
        //                                        .Where(s => s.DriverAgeRange != "")
        //                                        .Where(s => s.DriverGender != "")
        //                                        .Where(s => s.DriverRace != "")
        //                                        .Where(s => s.OfficerGender != "")
        //                                        .Where(s => s.OfficerRace != "")
        //                                        .Where(s => s.OfficerAgeRange != "")
        //                                        .Where(s => s.ActivityResults != "")
        //                                        .ToList();




        //    var queryCountWandC = allStops.Where(s => s.DriverGender.Contains(query.GenderDriver))
        //                                        .Where(s => s.OfficerGender.Contains(query.GenderOfficer))
        //                                        .Where(s => s.DriverRace.Contains(query.RaceDriver))
        //                                        .Where(s => s.OfficerRace.Contains(query.RaceOfficer))
        //                                        .Where(s => s.DriverAgeRange.Contains(query.AgeDriver))
        //                                        .Where(s => s.OfficerAgeRange.Contains(query.AgeOfficer))
        //                                        .Where(s => s.ActivityResults.Contains(""))
        //                                        .ToList()
        //                                        ;


        //    var demoCount = queryCountWandC.Count();
        //    var resultCount = queryCountWandC.Where(s => s.ActivityResults.Contains(query.Result)).Count();


        //    double ratio = (double)resultCount / (double)demoCount;


        //    var compareResultCountC = allStops.Where(s => s.ActivityResults.Contains("")).Count();


        //    //var compareResultCountW = allStops.Where(s => s.ActivityResults.Contains("WARNING")).Count();

        //    //var compareResultCountC = allStops.Where(s => s.ActivityResults.Contains("CITATION ISSUED")).Count();

        //    //var check1 = allStops.Where(s => s.DriverGender.Contains(query.GenderDriver)).ToList();
        //    //var check2 = allStops.Where(s => s.OfficerGender.Contains(query.GenderOfficer)).ToList();

        //    //var check3 = allStops.Where(s => s.DriverRace.Contains(query.RaceDriver)).ToList();
        //    //var check4 = allStops.Where(s => s.OfficerRace.Contains(query.RaceOfficer)).ToList();

        //    //var check5 = allStops.Where(s => s.DriverAgeRange.Contains(query.AgeDriver)).ToList();
        //    //var check6 = allStops.Where(s => s.OfficerAgeRange.Contains(query.AgeOfficer)).ToList();

        //    var rounded = Math.Round(ratio, 2);


        //    stats.Add(Math.Round((rounded * 100), 2));
        //    stats.Add(demoCount);
        //    stats.Add(resultCount);

        //    return stats;
        //}

        [HttpGet]
        public List<double> GetStats([FromUri] StopVars query)
        {

            List<double> stats = new List<double>();

            double ratio = 0;
            int allResults = 0;
            int activityResults = 0;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=JESS_COMPUTER\\SQLEXPRESS;Database=firstDB;Trusted_Connection=true";
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
