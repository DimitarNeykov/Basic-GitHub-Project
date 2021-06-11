namespace GitHub.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GitHub.ViewModels.Repositories;

    public interface IRepositoriesService
    {
        Task AddAsync(RepositoryCreateInputModel inputModel, string userId);

        RepositoryViewModel GetById(string id);

        IEnumerable<RepositoryViewModel> GetAllByUser(string userId);

        IEnumerable<RepositoryViewModel> GetAllPublic();

        Task DeleteAsync(string id);
    }
}
