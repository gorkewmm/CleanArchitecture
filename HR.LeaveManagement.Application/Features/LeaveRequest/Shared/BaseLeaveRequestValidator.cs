using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Shared
{
    public class BaseLeaveRequestValidator<T> : AbstractValidator<T> where T : BaseLeaveRequest
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(q => q.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist)
                .WithMessage("{PropertyName} does not exist.");

            RuleFor(q => q.StartDate)
                .LessThan(q => q.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}");

            RuleFor(q => q.EndDate)
                .GreaterThan(q => q.StartDate).WithMessage("{PropertyName}  must be after {ComparisonValue}");

            _leaveTypeRepository = leaveTypeRepository;
        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken arg2)
        {
            return await _leaveTypeRepository.LeaveTypeDoesExist(id);
        }
    }
}
