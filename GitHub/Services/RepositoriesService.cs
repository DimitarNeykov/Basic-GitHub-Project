namespace GitHub.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GitHub.Data;
    using GitHub.DataModels;
    using GitHub.ViewModels.Repositories;

    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext dbContext;

        public RepositoriesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(RepositoryCreateInputModel inputModel, string userId)
        {
            var repository = new Repository
            {
                Name = inputModel.Name,
                CreatedOn = DateTime.UtcNow,
                Description = inputModel.Description,
                IsPublic = inputModel.IsPublic,
                UserId = userId,
            };

            await this.dbContext.Repositories.AddAsync(repository);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<RepositoryViewModel> GetAllByUser(string userId)
        {
            var repository = this.dbContext
                .Repositories
                .Where(r => r.UserId == userId && r.IsDelete == false)
                .Select(r => new RepositoryViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    IsPublic = r.IsPublic,
                })
                .ToList();

            return repository;
        }

        public IEnumerable<RepositoryViewModel> GetAllPublic()
        {
            var repository = this.dbContext
                .Repositories
                .Where(r => r.IsPublic == true && r.IsDelete == false)
                .Select(r => new RepositoryViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    IsPublic = r.IsPublic,
                })
                .ToList();

            return repository;
        }

        public RepositoryViewModel GetById(string id)
        {
            var repository = this.dbContext
                .Repositories
                .Where(r => r.Id == id && r.IsDelete == false)
                .Select(r => new RepositoryViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    IsPublic = r.IsPublic,
                    UserId = r.UserId,
                })
                .ToList()
                .FirstOrDefault();

            return repository;
        }

        public async Task DeleteAsync(string id)
        {
            var repository = this.dbContext
                .Repositories
                .FirstOrDefault(r => r.Id == id);

            repository.IsDelete = true;
            repository.DeletedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }
    }
}
