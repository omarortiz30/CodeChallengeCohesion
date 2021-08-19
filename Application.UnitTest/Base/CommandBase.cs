using ServiceRequest.Persistence;
using System;

namespace Application.UnitTest.Base
{
    public class CommandBase : IDisposable
    {
        protected readonly AppDbContext _context;

        public CommandBase()
        {
            _context = DbContextFactory.Create();
        }

        public void Dispose()
        {
            DbContextFactory.Destroy(_context);
        }
    }
}
