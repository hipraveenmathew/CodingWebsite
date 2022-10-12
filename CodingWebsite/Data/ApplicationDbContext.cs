using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodingWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CodingWebsite.Models.Answers> Answers { get; set; } = default!;
        public DbSet<CodingWebsite.Models.Questions> Questions { get; set; } = default!;

        public DbSet<CodingWebsite.Models.Scoreboard> Scoreboard { get; set; } = default!;

        public DbSet<CodingWebsite.Models.ScoreCard> ScoreCard { get; set; } = default!;
        public DbSet<CodingWebsite.Models.UserDetails> UserDetails { get; set; } = default!;



    }
}