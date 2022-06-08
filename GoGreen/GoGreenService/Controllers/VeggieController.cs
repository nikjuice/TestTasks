
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoGreenService.Models;
using GoGreenService.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoGreenService.Controllers
{
    [EnableCors("GoGreenClientPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class VeggieController : ControllerBase
    {
        private readonly IVeggieService _veggieService;
   
        public VeggieController(IVeggieService veggieService)
        {
            _veggieService = veggieService;
        }

     
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        public async Task<ActionResult<IEnumerable<Veggie>>> Get()
        {
            var veggies = await _veggieService.GetAsync();
            
            if(veggies == null || veggies.Count() == 0)
            {
                return new List<Veggie>();
            }

            return Ok(veggies);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Veggie>> Get(string  id)
        {
            var veggie =  await _veggieService.GetAsync(id);

            if (veggie == null)
            {
                return NotFound();
            }

            return Ok(veggie);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostAsync([FromBody] Veggie veggie)
        {
            var result = await _veggieService.AddAsync(veggie);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(result);
        }
               
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PutAsync([FromBody] Veggie veggie)
        {
            var result = await _veggieService.UpdateAsync(veggie);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }
       
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var result =  await _veggieService.DeleteAsync(id);

            if(!result)
            {
                return NotFound();
            }

            return Ok();

        }
    }
}
