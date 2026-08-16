using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
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
    public class GetLeaveAllocationQueryHandlerTests
    {
        private readonly Mock<ILeaveAllocationRepository> _mockRepo;
        private readonly IMapper _mapper;
        public GetLeaveAllocationQueryHandlerTests()
        {
            _mockRepo = MockLeaveAllocationRepository.GetMockLeaveAllocationRepository();
            var mapConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new LeaveAllocationProfile());
            }, NullLoggerFactory.Instance);

            _mapper = mapConfig.CreateMapper();
        }

        [Fact]
        public async Task GetLeaveAllocationTests()
        {
            var handler = new GetLeaveAllocationQueryHandler(_mockRepo.Object, _mapper);
            var result = await handler.Handle(new GetLeaveAllocationDetailQuery() { Id = 1 }, CancellationToken.None);

            result.ShouldNotBeNull();
            result.Period.ShouldBe(2024);
            result.Id.ShouldBe(1);
            result.NumberOfDays.ShouldBe(10);
            result.ShouldBeOfType<LeaveAllocationDetailsDto>();
        }
    }
}
