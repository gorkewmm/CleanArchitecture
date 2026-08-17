using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests
{
    public class HrDatabaseContextTests
    {
        private HrDatabaseContext _context;

        public HrDatabaseContextTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HrDatabaseContext>();
            var options = optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _context = new HrDatabaseContext(options);
        }

        [Fact]
        public async Task Save_SetDateCreateValue()
        {
            var leaveType = new LeaveType()
            {
                Id = 1,
                Name = "görkem",
                DefaultDays = 10
            };

            await _context.Set<LeaveType>().AddAsync(leaveType);
            await _context.SaveChangesAsync();

            leaveType.DateCreated.ShouldNotBeNull();
            leaveType.DateModified.ShouldNotBeNull();


        }

        [Fact]
        public async Task Save_SetDateModifiedValue()
        {
            var leaveType = new LeaveType()
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Halil"
            };

            await _context.AddAsync(leaveType);
            await _context.SaveChangesAsync();

            var oldModifiedDate = leaveType.DateModified;

            leaveType.Name = "Tarık";
            leaveType.DefaultDays = 16;

            await _context.SaveChangesAsync();


            leaveType.DateModified.ShouldNotBeNull();
            (leaveType.DateModified > oldModifiedDate).ShouldBeTrue();
        }
    }
}