using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Excepitons;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Models.Email;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;
        public CreateLeaveRequestCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender, IAppLogger<CreateLeaveRequestCommandHandler> logger)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _emailSender = emailSender;
            _logger = logger;
        }
        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Request",validationResult);
            }

            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
            await _leaveRequestRepository.CreateAsync(leaveRequest);

            try
            {
                //Send Confirmation email
                var email = new EmailMessage()
                {
                    To = string.Empty,
                    Body = $"Your leave request for {request.StartDate} to {request.EndDate:D} " +
                           $"has been created successfully.",
                    Subject = "Leave Request Created"
                };
                await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message,"s","a");
            }

            return Unit.Value;
        }
    }
}
