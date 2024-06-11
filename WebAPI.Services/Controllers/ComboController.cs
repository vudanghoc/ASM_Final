using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Services;
using Services.Models.Combo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private readonly IComboService _comboService;

        public ComboController(IComboService comboService)
        {
            _comboService = comboService;
        }
        // GET: api/<ComboController>
        [HttpGet]
        [Route("searchString")]
        public async Task<IActionResult> Get(string? search)
        {
            var option = await _comboService.GetAllCombos(search);
            if (option == null)
            {
                return NotFound("[Tìm thấy đóe :v]");
            }
            return Ok(option);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var option = await _comboService.GetAllCombos();
            if (option == null)
            {
                return NotFound("[Tìm thấy đóe :v]");
            }
            return Ok(option);

        }

        // GET api/<ComboController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var option = await _comboService.GetComboById(id);
            return Ok(option);
        }
/*        // GET api/<ComboController>/ga
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var option = await _comboService.GetComboByName(name);
            return Ok(option);
        }*/

        // POST api/<ComboController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ComboForCreate comboDto)
        {
            var combo = await _comboService.AddCombo(comboDto);
            return Ok(combo);
        }

        // PUT api/<ComboController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ComboForUpdate comboDto)
        {
            var combo = await _comboService.UpdateCombo(comboDto, id);
            return Ok(combo);
        }

        // DELETE api/<ComboController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var combo = await _comboService.DeleteCombo(id);
            return Ok(combo);
        }
    }
}
