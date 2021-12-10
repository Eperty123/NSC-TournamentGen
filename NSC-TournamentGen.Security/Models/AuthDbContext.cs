using Microsoft.EntityFrameworkCore;
using NSC_TournamentGen.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSC_TournamentGen.Security.Models
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<LoginUserEntity> LoginUsers { get; set; }
    }
}
