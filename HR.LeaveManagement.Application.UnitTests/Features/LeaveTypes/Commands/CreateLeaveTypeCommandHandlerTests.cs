using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeaveTypeRepository> _mockRepo;

        public CreateLeaveTypeCommandHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new LeaveTypeProfile());
            },NullLoggerFactory.Instance);

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task CreateLeaveTypeTests()
        {
            var handler = new CreateLeaveTypeCommandHandler(_mapper, _mockRepo.Object);

            var result = await handler.Handle(new CreateLeaveTypeCommand()
            {
                Name = "abc deneme",
                DefaultDays = 17
            }, CancellationToken.None);

            var allLeaveTypes = await _mockRepo.Object.GetAsync();

            allLeaveTypes.Count().ShouldBe(4);
            

        }
    }
}
