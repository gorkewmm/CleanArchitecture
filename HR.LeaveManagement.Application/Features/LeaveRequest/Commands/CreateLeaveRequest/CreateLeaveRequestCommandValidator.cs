using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandValidator : BaseLeaveRequestValidator<CreateLeaveRequestCommand>
    {
     
        public CreateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository) : base(leaveTypeRepository)
        {
            
        }

    }
}
