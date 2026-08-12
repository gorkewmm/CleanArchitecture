using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequest
{
    public class ChangeLeaveRequestApprovalCommand : BaseLeaveRequest, IRequest<Unit>
    {
        public int Id { get; set; }
        public bool Approved { get; set; }
    }
}
