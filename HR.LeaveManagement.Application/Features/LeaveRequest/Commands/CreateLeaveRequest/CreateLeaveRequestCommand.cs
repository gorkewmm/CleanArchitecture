using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommand : BaseLeaveRequest, IRequest<Unit>
    {
        public string RequestComments { get; set; } = string.Empty;
    }
}
