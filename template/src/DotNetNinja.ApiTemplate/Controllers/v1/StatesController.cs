using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ChaosMonkey.Guards;
using DotNetNinja.ApiTemplate.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DotNetNinja.ApiTemplate.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StatesController : ControllerBase
    {
        public StatesController(IMapper mapper)
        {
            Mapper = Guard.IsNotNull(mapper, nameof(mapper));
        }

        protected IMapper Mapper { get; }

        [HttpGet]
        public IActionResult Get()
        {
            var model = Mapper.Map<List<StateModel>>(State.AllStates);

            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var data = State.AllStates.SingleOrDefault(state =>
                    state.Id.Equals(id, StringComparison.CurrentCultureIgnoreCase));
                if (data != null)
                {
                    var model = Mapper.Map<StateModel>(data);
                    return Ok(model);
                }

                return NotFound();
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Post([FromBody] StateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var state = Mapper.Map<State>(model);
            state.Id = state.Id.ToUpper();
            if (State.AllStates.Any(item => item.Id == state.Id))
            {
                return Conflict();
            }

            State.AllStates.Add(state);

            model = Mapper.Map<StateModel>(state);
            return Created(new Uri($"/States/{state.Id}", UriKind.Relative), model);
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