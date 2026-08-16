using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public static class MockLeaveAllocationRepository
    {
        public static Mock<ILeaveAllocationRepository> GetMockLeaveAllocationRepository()
        {
            var leaveAllocations = new List<LeaveAllocation>()
            {
                new LeaveAllocation()
                {
                    Id = 1,
                    NumberOfDays = 10,
                    LeaveTypeId = 1,
                    Period = 2024,
                    EmployeeId = "employee-guid-1"
                },
                new LeaveAllocation()
                {
                    Id = 2,
                    NumberOfDays = 15,
                    LeaveTypeId = 2,
                    Period = 2024,
                    EmployeeId = "employee-guid-2"
                },
                new LeaveAllocation()
                {
                    Id = 3,
                    NumberOfDays = 20,
                    LeaveTypeId = 3,
                    Period = 2024,
                    EmployeeId = "employee-guid-3"
                },
            };

            var mockRepo = new Mock<ILeaveAllocationRepository>();

            mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveAllocations);

            //For GetLeaveAllocationListQueryHandler
            mockRepo.Setup(r => r.GetLeaveAllocationsWithDetails())
                .ReturnsAsync(() =>
                {
                    return leaveAllocations;
                });

            //For GetLeaveAllocationQueryHandler
            mockRepo.Setup(r => r.GetLeaveAllocationWithDetails(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    return leaveAllocations.Find(q => q.Id == id);
                });

            //For CreateLeaveAllocationCommandHandler

            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    return leaveAllocations.Find(q => q.Id == id);
                });

            mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveAllocation>()))
                .Returns((LeaveAllocation leaveAllocation) =>
                {
                    leaveAllocations.Add(leaveAllocation);
                    return Task.CompletedTask;
                });

            return mockRepo;
        }
    }
}
