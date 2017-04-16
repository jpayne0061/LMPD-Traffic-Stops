using System;
using System.Collections.Generic;
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
        public List<double> GetStats([FromUri]StopVars query)
        {
            query.SetToEmptyString();

            List<double> stats = new List<double>();

            var allStops = _context.StopEntries.Where(s => s.DriverAgeRange != null)
                                                .Where(s => s.DriverGender != null)
                                                .Where(s => s.DriverRace != null)
                                                .Where(s => s.OfficerGender != null)
                                                .Where(s => s.OfficerRace != null)
                                                .Where(s => s.OfficerAgeRange != null)
                                                .Where(s => s.ActivityResults != null)
                                                .Where(s => s.DriverAgeRange != "")
                                                .Where(s => s.DriverGender != "")
                                                .Where(s => s.DriverRace != "")
                                                .Where(s => s.OfficerGender != "")
                                                .Where(s => s.OfficerRace != "")
                                                .Where(s => s.OfficerAgeRange != "")
                                                .Where(s => s.ActivityResults != "")
                                                .Where(s => s.DriverGender.Contains(query.GenderDriver))
                                                .Where(s => s.OfficerGender.Contains(query.GenderOfficer))
                                                .Where(s => s.DriverRace.Contains(query.RaceDriver))
                                                .Where(s => s.OfficerRace.Contains(query.RaceOfficer))
                                                .Where(s => s.DriverAgeRange.Contains(query.AgeDriver))
                                                .Where(s => s.OfficerAgeRange.Contains(query.AgeOfficer))
                                                .Where(s => s.ActivityResults.Contains(""))
                                                ;


            var demographicCount = allStops.Count();
            var resultCount = allStops.Where(s => s.ActivityResults.Contains(query.Result)).Count();


            double ratio = (double)resultCount / (double)demographicCount;

            var rounded = Math.Round(ratio, 2);


            stats.Add(Math.Round((rounded * 100), 2));
            stats.Add(demographicCount);
            stats.Add(resultCount);

            return stats;
        }



    }
}

