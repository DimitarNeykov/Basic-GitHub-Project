namespace GitHub.Services
{
    using GitHub.ViewModels.Issues;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GitHub.Data;
    using GitHub.DataModels;

    public class IssuesService : IIssuesService
    {
        private readonly ApplicationDbContext dbContext;

        public IssuesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(IssuesCreateInputModel inputModel, string userId)
        {
            var issue = new Issue()
            {
                CreatedOn = DateTime.UtcNow,
                Title = inputModel.Title,
                Comment = inputModel.Comment,
                RepositoryId = inputModel.RepositoryId,
                UserId = userId,
            };


            await this.dbContext.AddAsync(issue);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<IssuesViewModel> GetMyIssues(string userId)
        {
            var issues = this.dbContext
                .Issues
                .Where(i => i.UserId == userId && i.IsDelete == false)
                .Select(i => new IssuesViewModel
                {
                    Id = i.Id,
                    CreatedOn = i.CreatedOn,
                    Title = i.Title,
                    Comment = i.Comment,
                    RepositoryName = i.Repository.Name,
                })
                .ToList();

            return issues;
        }

        public async Task DeleteAsync(string id)
        {
            var issue = this.dbContext
                .Issues
                .FirstOrDefault(i => i.Id == id);

            issue.IsDelete = true;
            issue.DeletedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        public IssuesViewModel GetById(string id)
        {
            var issue = this.dbContext
                .Issues
                .Where(i => i.Id == id && i.IsDelete == false)
                .Select(i => new IssuesViewModel
                {
                    Id = i.Id,
                    UserId = i.UserId,
                })
                .ToList()
                .FirstOrDefault();

            return issue;
        }
    }
}
