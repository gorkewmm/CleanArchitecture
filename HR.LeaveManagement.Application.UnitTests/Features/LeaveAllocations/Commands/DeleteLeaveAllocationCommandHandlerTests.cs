using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveAllocations.Commands
{
    public class DeleteLeaveAllocationCommandHandlerTests
    {
        private readonly Mock<ILeaveAllocationRepository> _mockRepo;
        public DeleteLeaveAllocationCommandHandlerTests()
        {
            _mockRepo = MockLeaveAllocationRepository.GetMockLeaveAllocationRepository();  
        }


        [Fact]
        public async Task DeleteLeaveAllocationTests()
        {
            var handler = new DeleteLeaveAllocationCommandHandler(_mockRepo.Object);

            await handler.Handle(new DeleteLeaveAllocationCommand() { Id = 1 }, CancellationToken.None);

            var leaveAllocations = await _mockRepo.Object.GetAsync();
            leaveAllocations.Count.ShouldBe(2);

            var leaveAllocation = await _mockRepo.Object.GetByIdAsync(1);
            leaveAllocation.ShouldBeNull();
        }
    }
}
