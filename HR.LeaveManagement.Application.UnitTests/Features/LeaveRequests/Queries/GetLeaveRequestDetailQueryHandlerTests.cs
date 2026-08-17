using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveRequests.Queries
{
    public class GetLeaveRequestDetailQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeaveRequestRepository> _mockRepo;

        public GetLeaveRequestDetailQueryHandlerTests()
        {
            _mockRepo = new MockLeaveRequestRepository().GetMockLeaveRequestRepository();

            var mapConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new LeaveRequestProfile());
            }, NullLoggerFactory.Instance);

            _mapper = mapConfig.CreateMapper();
        }

        [Fact]
        public async Task GetLeaveRequestHandlerTests()
        {
            var handler = new GetLeaveRequestDetailQueryHandler(_mapper, _mockRepo.Object);

            var leaveRequestDetailsDto = await handler.Handle(new GetLeaveRequestDetailQuery() { Id = 1 }, CancellationToken.None);

            leaveRequestDetailsDto.RequestComments.ShouldBe("Family vacation");
            leaveRequestDetailsDto.Cancelled.ShouldBe(false);
        }
    }
}
