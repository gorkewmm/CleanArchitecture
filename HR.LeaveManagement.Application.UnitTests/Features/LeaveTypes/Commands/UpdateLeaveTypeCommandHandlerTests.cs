using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Commands
{
    public class UpdateLeaveTypeCommandHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private readonly Mock<IAppLogger<UpdateLeaveTypeCommandHandler>> _appLogger;

        private readonly IMapper _mapper;
        public UpdateLeaveTypeCommandHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            _appLogger = new Mock<IAppLogger<UpdateLeaveTypeCommandHandler>>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new LeaveTypeProfile());
            }, NullLoggerFactory.Instance);

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task UpdateLeaveTypeTests()
        {
            var handler = new UpdateLeaveTypeCommandHandler(_mapper, _mockRepo.Object, _appLogger.Object);
            var result = await handler.Handle(new UpdateLeaveTypeCommand()
            {
                Id = 3,
                Name = "SelamGuncellendi",
                DefaultDays = 99
            },CancellationToken.None);

            var leaveType = await _mockRepo.Object.GetByIdAsync(3);

            leaveType.Name.ShouldBe("SelamGuncellendi");
        
        }
    }
}
