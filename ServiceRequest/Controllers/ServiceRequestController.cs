using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceRequest.Application;
using ServiceRequest.Application.ServiceRequest.Commands;
using ServiceRequest.Application.ServiceRequest.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IList<ServiceRequestDTO>>> GetAllAsync()
        {
            var srList = await Mediator.Send(new GetServiceRequestListQuery());

            if (srList.Count == 0)
                return NoContent();

            return Ok(srList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRequestDTO>> GetAsync(Guid id)
        {
            var srDto = await Mediator.Send(new GetServiceRequestQuery { Id = id });

            return Ok(srDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateServiceRequestCommand command)
        {
            var serviceRequestId = await Mediator.Send(command);
            return Created(string.Format("~api/servicerequest/{0}", serviceRequestId), serviceRequestId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateServiceRequestCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteServiceRequestCommand { Id = id });

            return NoContent();
        }
    }
}
