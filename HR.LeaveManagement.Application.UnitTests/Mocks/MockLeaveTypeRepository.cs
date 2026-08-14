using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
        {
            var leaveTypes = new List<LeaveType> //Aslında küçük bir database oluşturduk.
            {
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Test Vacation"
                },
                new LeaveType
                {
                    Id = 2,
                    DefaultDays = 15,
                    Name = "Test Sick"
                },
                new LeaveType
                {
                    Id = 3,
                    DefaultDays = 15,
                    Name = "Test Maternity"
                },
            };
            #region
            //"Sevgili Mock nesnesi, eğer test koşulurken birisi senin GetAsync() metodunu çağırırsa, veritabanına falan gitmeye çalışma; onun yerine al bu benim yukarıda elimle hazırladığım leaveTypes listesini asenkron olarak (ReturnsAsync) onlara ver."
            #endregion

            var mockRepo = new Mock<ILeaveTypeRepository>(); //Bana ILeaveTypeRepository gibi davranabilecek sahte bir nesne oluştur.

            mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveTypes);//Bu mock repository'nin GetAsync() metodu çağrıldığında ne olacağını söyleriz.

            mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
                .Returns((LeaveType leaveType) =>
                {
                    leaveTypes.Add(leaveType);
                    return Task.CompletedTask;
                });

            return mockRepo;
            
        }
    }
}