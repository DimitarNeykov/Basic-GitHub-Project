namespace GitHub.Services
{
    using System;
    using GitHub.ViewModels.Commits;
    using System.Threading.Tasks;
    using GitHub.Data;
    using GitHub.DataModels;
    using System.Collections.Generic;
    using System.Linq;

    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext dbContext;

        public CommitsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(CommitCreateInputModel inputModel, string userId)
        {
            var commit = new Commit
            {
                CreatedOn = DateTime.UtcNow,
                Message = inputModel.Message,
                RepositoryId = inputModel.RepositoryId,
                UserId = userId,
            };


            await this.dbContext.AddAsync(commit);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<CommitViewModel> GetMyCommits(string userId)
        {
            var commits = this.dbContext
                .Commits
                .Where(c => c.UserId == userId && c.IsDelete == false)
                .Select(c => new CommitViewModel
                {
                    Id = c.Id,
                    CreatedOn = c.CreatedOn,
                    Message = c.Message,
                    RepositoryName = c.Repository.Name,
                })
                .ToList();

            return commits;
        }

        public async Task DeleteAsync(string id)
        {
            var commit = this.dbContext
                .Commits
                .FirstOrDefault(c => c.Id == id);

            commit.IsDelete = true;
            commit.DeletedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        public CommitViewModel GetById(string id)
        {
            var commit = this.dbContext
                .Commits
                .Where(c => c.Id == id && c.IsDelete == false)
                .Select(c => new CommitViewModel
                {
                    Id = c.Id,
                    UserId = c.UserId,
                })
                .ToList()
                .FirstOrDefault();

            return commit;
        }
    }
}
