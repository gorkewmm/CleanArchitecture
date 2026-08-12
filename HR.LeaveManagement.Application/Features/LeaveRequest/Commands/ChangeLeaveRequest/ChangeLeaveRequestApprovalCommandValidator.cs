using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequest
{
    public class ChangeLeaveRequestApprovalCommandValidator : BaseLeaveRequestValidator<ChangeLeaveRequestApprovalCommand>
    {
        public ChangeLeaveRequestApprovalCommandValidator(ILeaveTypeRepository leaveTypeRepository) : base(leaveTypeRepository)
        {
            RuleFor(q => q.Approved)
                .NotNull()
                .WithMessage("Approval status can not be null");
        }
    }
}
