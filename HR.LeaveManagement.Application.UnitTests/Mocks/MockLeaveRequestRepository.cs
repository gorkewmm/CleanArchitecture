using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public class MockLeaveRequestRepository
    {
        List<LeaveType> leaveTypes = new List<LeaveType>()
        {
            new LeaveType { Id = 1, Name = "Vacation", DefaultDays = 10 },
            new LeaveType { Id = 2, Name = "Sick Leave", DefaultDays = 15 }
        };

        List<LeaveRequest> leaveRequests;

        public MockLeaveRequestRepository()
        {
            leaveRequests = new List<LeaveRequest>()
            {
                new LeaveRequest
                {
                    Id = 1,
                    StartDate = DateTime.Now.AddDays(5),
                    EndDate = DateTime.Now.AddDays(10),
                    LeaveTypeId = 1,
                    LeaveType = leaveTypes.First(q => q.Id == 1),
                    DateRequested = DateTime.Now,
                    RequestComments = "Family vacation",
                    Approved = null,
                    Cancelled = false,
                    RequestingEmployeeId = "employee-guid-1"
                },
                new LeaveRequest
                {
                    Id = 2,
                    StartDate = DateTime.Now.AddDays(15),
                    EndDate = DateTime.Now.AddDays(16),
                    LeaveTypeId = 2,
                    LeaveType = leaveTypes.First(q => q.Id == 2),
                    DateRequested = DateTime.Now.AddDays(-2),
                    RequestComments = "Doctor appointment",
                    Approved = true,
                    Cancelled = false,
                    RequestingEmployeeId = "employee-guid-2"
                },
                new LeaveRequest
                {
                    Id = 3,
                    StartDate = DateTime.Now.AddDays(-10),
                    EndDate = DateTime.Now.AddDays(-5),
                    LeaveTypeId = 1,
                    LeaveType = leaveTypes.First(q => q.Id == 1),
                    DateRequested = DateTime.Now.AddDays(-20),
                    RequestComments = "Personal reasons",
                    Approved = false,
                    Cancelled = true,
                    RequestingEmployeeId = "employee-guid-1"
                }
            };
        }
        public Mock<ILeaveRequestRepository> GetMockLeaveRequestRepository()
        {
            var _mockRepo = new Mock<ILeaveRequestRepository>();

            //For GetLeaveRequestListQueryHandler
            _mockRepo.Setup(r => r.GetAsync())
                .ReturnsAsync(leaveRequests);

            //For GetLeaveRequestDetailQueryHandler
            _mockRepo.Setup(r => r.GetLeaveRequestWithDetails(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    return leaveRequests.Find(q => q.Id == id);
                });

            return _mockRepo;
        }
    }
}
