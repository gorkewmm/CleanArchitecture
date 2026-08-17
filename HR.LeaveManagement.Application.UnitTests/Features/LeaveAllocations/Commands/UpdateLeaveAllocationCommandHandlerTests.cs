using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;


namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveAllocations.Commands
{
    public class UpdateLeaveAllocationCommandHandlerTests
    {
        private readonly Mock<ILeaveAllocationRepository> _mockRepo;
        private readonly Mock<ILeaveTypeRepository> _mockRepo2;
        private readonly IMapper _mapper;
        public UpdateLeaveAllocationCommandHandlerTests()
        {
            _mockRepo = MockLeaveAllocationRepository.GetMockLeaveAllocationRepository();
            _mockRepo2 = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

            var mapConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new LeaveAllocationProfile());
            }, NullLoggerFactory.Instance);

            _mapper = mapConfig.CreateMapper();
        }

        [Fact]
        public async Task UpdateLeaveAllocationTests()
        {
            var handler = new UpdateLeaveAllocationCommandHandler(_mockRepo2.Object, _mapper, _mockRepo.Object);
            await handler.Handle(new UpdateLeaveAllocationCommand() { Id = 2, LeaveTypeId = 1, NumberOfDays = 17, Period = 2027 }, CancellationToken.None);

            var leaveType = await _mockRepo.Object.GetByIdAsync(2);
            leaveType.NumberOfDays.ShouldBe(17);
        }
    }
}
