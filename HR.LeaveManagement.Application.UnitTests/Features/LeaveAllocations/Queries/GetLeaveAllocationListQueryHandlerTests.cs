using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveAllocations.Queries
{
    public class GetLeaveAllocationListQueryHandlerTests
    {
        private readonly Mock<ILeaveAllocationRepository> _mockRepo;
        private readonly IMapper _mapper;
        public GetLeaveAllocationListQueryHandlerTests()
        {
            _mockRepo = MockLeaveAllocationRepository.GetMockLeaveAllocationRepository();

            var mapConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new LeaveAllocationProfile());
            },NullLoggerFactory.Instance);

            _mapper = mapConfig.CreateMapper();
        }

        [Fact]
        public async Task GetLeaveAllocationListTests()
        {
            var handler = new GetLeaveAllocationListQueryHandler(_mockRepo.Object, _mapper);
            var result = await handler.Handle(new GetLeaveAllocationListQuery(), CancellationToken.None);

            result.ShouldBeOfType(typeof(List<LeaveAllocationDto>));
            result.Count().ShouldBe(3);

        }
    }
}
