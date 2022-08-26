using Microsoft.EntityFrameworkCore;
using MVCPractica2.Models;
using MVCPractica2.Mapper.DTO.UserDTO;

namespace MVCPractica2.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Simpson> Simpsons { get; set; }
    }
}
