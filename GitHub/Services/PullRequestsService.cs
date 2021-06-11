namespace GitHub.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GitHub.Data;
    using GitHub.DataModels;
    using GitHub.ViewModels.PullRequests;

    public class PullRequestsService : IPullRequestsService
    {
        private readonly ApplicationDbContext dbContext;

        public PullRequestsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(string repositoryId, string userId)
        {
            var pullRequest = new PullRequest
            {
                CreatedOn = DateTime.UtcNow,
                RepositoryId = repositoryId,
                UserId = userId,
            };


            await this.dbContext.AddAsync(pullRequest);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<PullRequestViewModel> GetMyPullRequests(string userId)
        {
            var pullRequest = this.dbContext
                .PullRequests
                .Where(r => r.UserId == userId && r.IsDelete == false)
                .Select(r => new PullRequestViewModel
                {
                    Id = r.Id,
                    CreatedOn = r.CreatedOn,
                    RepositoryName = r.Repository.Name,
                })
                .ToList();

            return pullRequest;
        }

        public async Task DeleteAsync(string id)
        {
            var pullRequest = this.dbContext
                .PullRequests
                .FirstOrDefault(r => r.Id == id);

            pullRequest.IsDelete = true;
            pullRequest.DeletedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        public PullRequestViewModel GetById(string id)
        {
            var pullRequest = this.dbContext
                .PullRequests
                .Where(r => r.Id == id && r.IsDelete == false)
                .Select(r => new PullRequestViewModel
                {
                    Id = r.Id,
                    UserId = r.UserId,
                })
                .ToList()
                .FirstOrDefault();

            return pullRequest;
        }
    }
}
