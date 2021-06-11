namespace GitHub.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GitHub.ViewModels.Commits;

    public interface ICommitsService
    {
        Task AddAsync(CommitCreateInputModel inputModel, string userId);

        IEnumerable<CommitViewModel> GetMyCommits(string userId);

        CommitViewModel GetById(string id);

        Task DeleteAsync(string id);
    }
}
