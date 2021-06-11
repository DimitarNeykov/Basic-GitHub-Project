namespace GitHub.Data
{
    using System;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using GitHub.DataModels;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Repository> Repositories { get; set; }

        public virtual DbSet<Issue> Issues { get; set; }

        public virtual DbSet<Commit> Commits { get; set; }

        public virtual DbSet<PullRequest> PullRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            string roleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                Id = roleId,
                ConcurrencyStamp = roleId
            });
        }
    }
}
