using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bushour.api
{
    [Route("api/bushour")]
    [ApiController]
    public class BusHourController : ControllerBase
    {
        /// <summary>
        /// Injectable service
        /// </summary>
        private readonly IBusHourService _service;

        /// <summary>
        /// Default constructor to initiate bus hour api 
        /// </summary>
        /// <param name="service">Injectable service</param>        
        public BusHourController(IBusHourService service)
        {
            _service = service;
        }

        /// <summary>
        /// Bus line by id
        /// </summary>
        /// <remarks>
        /// Endpoint responsible for returning one or a list of buses lines by identification number
        /// </remarks>
        /// <param name="id">Identification number from line of bus</param>
        /// <returns>A json object</returns>
        [HttpGet("id/{id}")]
        public async Task<ActionResult<IEnumerable<string>>> GetId(string id = "0")
        {
            IEnumerable<LinhaVM> lines = await _service.GetBusId(id);
            if (lines != null && lines.Any())
                return Ok(lines);
            else
                return NoContent();
        }

        /// <summary>
        /// Bus line by name
        /// </summary>
        /// <remarks>
        /// Endpoint responsible for returning one or a list of buses lines by name of line
        /// </remarks>
        /// <param name="name">Name description from line of bus</param>
        /// <returns>A json object</returns>
        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<string>>> GetName(string name)
        {
            IEnumerable<LinhaVM> lines = await _service.GetBusName(name);
            if (lines != null && lines.Any())
                return Ok(lines);
            else
                return NoContent();
        }

        /// <summary>
        /// Bus line hour by id
        /// </summary>
        /// <remarks>
        /// Endpoint responsible for returning one or a list of buses lines hours by identification number
        /// </remarks>
        /// <param name="line">Identification number from line of bus</param>
        /// <returns>A json object</returns>
        [HttpGet("line/{id}")]
        public async Task<ActionResult<IEnumerable<string>>> GetLine(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            IEnumerable<HorarioVM> hours = await _service.GetHours(id);
            if (hours != null && hours.Any())
                return Ok(hours);            
            else
                return NoContent();
        }
        
    }
}
