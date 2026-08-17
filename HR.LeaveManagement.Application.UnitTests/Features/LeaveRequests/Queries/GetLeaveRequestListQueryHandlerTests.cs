using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using HR.LeaveManagement.Domain;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveRequests.Queries
{
    public class GetLeaveRequestListQueryHandlerTests
    {
        private readonly Mock<ILeaveRequestRepository> _mockRepo;
        private readonly IMapper _mapper;
        public GetLeaveRequestListQueryHandlerTests()
        {
            _mockRepo = new MockLeaveRequestRepository().GetMockLeaveRequestRepository();

            var mapConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new LeaveRequestProfile());
            },NullLoggerFactory.Instance);

            _mapper = mapConfig.CreateMapper();
        }

        [Fact]
        public async Task GetLeaveRequestListTests()
        {
            var handler = new GetLeaveRequestListQueryHandler(_mapper, _mockRepo.Object);
            var leaveRequestListDtos = await handler.Handle(new GetLeaveRequestListQuery(), CancellationToken.None);

            leaveRequestListDtos.Count.ShouldBe(3);

            
        }
    }
}
