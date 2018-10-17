using Microsoft.EntityFrameworkCore;

namespace UserAuth.API.Helpers
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options) {}
    }
}