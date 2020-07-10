using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AirlineProject;

namespace AirlineProjectWebAPI.Controllers
{
    [AnonymousAuthentication]
    public class AnonymousFacadeController : ApiController
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private FlyingCenterSystem fcs = FlyingCenterSystem.GetInstance();
        //public IAnonymousUserFacade anonymousUserFacade;

        public AnonymousFacadeController()
        {
           // anonymousUserFacade = (IAnonymousUserFacade)fcs.Login("", "", out ILoginToken token);
        }

        [HttpGet]
        [Route("api/anonymousfacade/getallairlinecompanies")]
        public IHttpActionResult GetAllAirlineCompanies()
        {
            IList<AirlineCompany> result = ((AnonymousUserFacade)Request.Properties["facade"]).GetAllAirlineCompanies();

            if(result.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("api/anonymousfacade/getallflights")]
        public IHttpActionResult GetAllFlights()
        {
            IList<Flight> result = ((AnonymousUserFacade)Request.Properties["facade"]).GetAllFlights();

            IList<Flight> clonedResult = new List<Flight>();
            foreach (var item in result)
            {
                Flight clonedItem = CopyMachine.DeepCopy(item);

                clonedResult.Add(clonedItem);
            }

            //if (result.Count == 0)
            if(clonedResult.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            //return Ok(result);
            return Ok(clonedResult);
        }
        [HttpGet]
        [Route("api/anonymousfacade/getallflightsvacancy")]
        public IHttpActionResult GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> result = ((AnonymousUserFacade)Request.Properties["facade"]).GetAllFlightsVacancy();

            if (result.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("api/anonymousfacade/getairlinecompanybyid/{id}")]
        public IHttpActionResult GetAirlineCompanyById(long id) //CONTINUE THE LOG
        {
            log.Info($"entering GetAirlineCompanyById (id = {id})");


            log.Debug("getting an AirlineCompany from the facade pulled from the request properties");
            AirlineCompany result = ((AnonymousUserFacade)Request.Properties["facade"]).GetAirlineCompanyById(id);
            log.Debug("recieved AirlineCompany result successfully");

            log.Debug("checking if the result is null");
            if(result is null)
            {
                log.Debug("the result is null");
                log.Info($"exiting GetAirlineCompanyById without results");
                return StatusCode(HttpStatusCode.NoContent);
            }
            log.Debug("the result is NOT null");
            log.Info($"exiting GetAirlineCompanyById with result: {result}");
            return Ok(result);
        }

        [HttpGet]
        [Route("api/anonymousfacade/getflight")] //query parameter
        public IHttpActionResult GetFlight(long id)
        {
            Flight result = ((AnonymousUserFacade)Request.Properties["facade"]).GetFlight(id);

            //Flight clonedResult = CopyMachine.DeepCopy(result);

            if (result is null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("api/anonymousfacade/getflightsbydeparturedate")]
        public IHttpActionResult GetFlightsByDepartureDate([FromBody] DateTime departureDate)
        {
            IList<Flight> result = ((AnonymousUserFacade)Request.Properties["facade"]).GetFlightsByDepartureDate(departureDate);

            if (result.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("api/anonymousfacade/getflightsbydestinationcountry/{countryCode}")]
        public IHttpActionResult GetFlightsByDestinationCountry(long countryCode)
        {
            IList<Flight> result = ((AnonymousUserFacade)Request.Properties["facade"]).GetFlightsByDestinationCountry(countryCode);

            if (result.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("api/anonymousfacade/getflightsbylandingdate")]
        public IHttpActionResult GetFlightsByLandingDate([FromBody] DateTime landingDate)
        {
            IList<Flight> result = ((AnonymousUserFacade)Request.Properties["facade"]).GetFlightsByLandingDate(landingDate);

            if (result.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("api/anonymousfacade/getflightsbyorigincountry/{countryCode}")]
        public IHttpActionResult GetFlightsByOriginCountry(long countryCode)
        {
            IList<Flight> result = ((AnonymousUserFacade)Request.Properties["facade"]).GetFlightsByOriginCountry(countryCode);
            if (result.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(result);
        }
    }
}
