using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PremuimCalculator.Data;
using PremuimCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PremuimCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremuimController : ControllerBase
    {
        private readonly ILogger<PremuimController> _logger;
        private readonly PremuimDbContext  premuimDbContext;

   
        public PremuimController(ILogger<PremuimController> logger, PremuimDbContext premuimDbContext)
        {
            this.premuimDbContext = premuimDbContext ?? throw new ArgumentNullException(nameof(premuimDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        [HttpGet("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Occupation>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Occupation>>> GetOccuPationAsync()
        {
            var occupationFromDb = await premuimDbContext.Occupations.ToListAsync();
            if(occupationFromDb is null)
            {
                return NotFound("Data was not Seeded");
            }
            return Ok(occupationFromDb);
        }
        [HttpGet("[action]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<OccupationRating>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OccupationRating>>> GetOccuPationRatingAsync()
        {
            var occupationRatingFromDb = await premuimDbContext.OccupationRatings.ToListAsync();
            if (occupationRatingFromDb is null)
            {
                return NotFound("Data was not Seeded");
            }
            return Ok(occupationRatingFromDb);
        }
    }
}
