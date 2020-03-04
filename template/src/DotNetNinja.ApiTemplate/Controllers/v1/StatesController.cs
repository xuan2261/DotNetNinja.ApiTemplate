using System;
using System.Linq;
using DotNetNinja.ApiTemplate.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DotNetNinja.ApiTemplate.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StatesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(State.AllStates);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var state = State.AllStates.SingleOrDefault(state =>
                    state.Id.Equals(id, StringComparison.CurrentCultureIgnoreCase));
                if (state != null)
                {
                    return Ok(state);
                }

                return NotFound();
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Post([FromBody] State state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            state.Id = state.Id.ToUpper();
            if (State.AllStates.Any(item => item.Id == state.Id))
            {
                return Conflict();
            }

            State.AllStates.Add(state);

            return Created(new Uri($"/States/{state.Id}", UriKind.Relative), state);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            var state = State.AllStates.SingleOrDefault(state =>
                state.Id.Equals(id, StringComparison.CurrentCultureIgnoreCase));
            if (state == null)
            {
                return NotFound();
            }

            State.AllStates.Remove(state);
            return Ok();
        }
    }
}