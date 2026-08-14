using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries
{
    public class GetLeaveTypeDetailsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeaveTypeRepository> _mockRepo;

        public GetLeaveTypeDetailsQueryHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new LeaveTypeProfile());
            }, NullLoggerFactory.Instance);

            _mapper = mapperConfig.CreateMapper();

            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
        }

        [Fact]
        public async Task GetLeaveTypeDetailsTests()
        {
            var handler = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object);

            var result = await handler.Handle(new GetLeaveTypeDetailsQuery(1), CancellationToken.None);

            result.ShouldBeOfType<LeaveTypeDetailsDto>();
            result.Id.ShouldBe(1);
            result.Name.ShouldBe("Test Vacation");
            result.DefaultDays.ShouldBe(10);
        }
    }
}
