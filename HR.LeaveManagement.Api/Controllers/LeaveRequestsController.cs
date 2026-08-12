using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<LeaveRequestsController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDto>>> Get()
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestListQuery());
            return Ok(leaveRequests);
        }

        // GET api/<LeaveRequestsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestListDto>> Get(int id)
        {
            var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailQuery()
            {
                Id = id
            });
            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequestsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Post(CreateLeaveRequestCommand leaveRequest)
        {
            await _mediator.Send(leaveRequest);
            return CreatedAtAction(nameof(Get), new {nesne = leaveRequest});
        }

        // PUT api/<LeaveRequestsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateLeaveRequestCommand leaveRequest)
        {
            await _mediator.Send(leaveRequest);
            return NoContent();
        }

        // PUT api/<LeaveRequestsController>/CancelRequest
        [HttpPut]
        [Route("CancelRequest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand cancelLeaveRequest)
        {
            await _mediator.Send(cancelLeaveRequest);
            return NoContent();
        }

        // PUT api/<LeaveRequestsController>/CancelRequest
        [HttpPut]
        [Route("UpdateApproval")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand updateApprovalRequest)
        {
            await _mediator.Send(updateApprovalRequest);
            return NoContent();
        }

        // DELETE api/<LeaveRequestsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task Delete(int id)
        {
            var command = new DeleteLeaveRequestCommand()
            {
                Id = id
            };
            await _mediator.Send(command);
        }
    }
}
