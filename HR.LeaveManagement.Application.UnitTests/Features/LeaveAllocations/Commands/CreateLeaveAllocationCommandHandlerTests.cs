using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using HR.LeaveManagement.Domain;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveAllocations.Commands
{
    public class CreateLeaveAllocationCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeaveAllocationRepository> _mockAllocationRepo;
        private readonly Mock<ILeaveTypeRepository> _leaveTypeRepository;

        public CreateLeaveAllocationCommandHandlerTests()
        {
            var mapConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new LeaveAllocationProfile());
            },NullLoggerFactory.Instance);

            _mapper = mapConfig.CreateMapper();

            _mockAllocationRepo = MockLeaveAllocationRepository.GetMockLeaveAllocationRepository();
            _leaveTypeRepository = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
        }

        [Fact]
        public async Task CreateLeaveAllocationTests()
        {
            var handler = new CreateLeaveAllocationCommandHandler(_leaveTypeRepository.Object, _mapper, _mockAllocationRepo.Object);

            await handler.Handle(new CreateLeaveAllocationCommand()
            {
                LeaveTypeId = 1
            }, CancellationToken.None);

            var leaveAllocations = await _mockAllocationRepo.Object.GetAsync();
            leaveAllocations.Count.ShouldBe(4);
            leaveAllocations[leaveAllocations.Count - 1].LeaveTypeId.ShouldBe(1);
        }

        
    }
}
