using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandValidator : BaseLeaveRequestValidator<UpdateLeaveRequestCommand>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        public UpdateLeaveRequestCommandValidator(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository) 
            : base(leaveTypeRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;

            RuleFor(q => q.Id)
                .NotNull()
                .MustAsync(LeaveRequestMustExist)
                .WithMessage("{PropertyName} must be present");
        }

        private async Task<bool> LeaveRequestMustExist(int id, CancellationToken arg2)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(id);

            return leaveRequest != null; 

        }
    }
}
