using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Server.Entities;

namespace TheatricalPlayersRefactoringKata.Server.ContextDb
{
    public class ContextDtaBase : DbContext
    {
        public ContextDtaBase(DbContextOptions<ContextDtaBase> options) : base(options) { }

        public DbSet<Play> Play { get; set; }
    }
}
