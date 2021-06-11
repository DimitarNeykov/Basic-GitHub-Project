namespace GitHub.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GitHub.ViewModels.PullRequests;

    public interface IPullRequestsService
    {
        Task AddAsync(string repositoryId, string userId);

        IEnumerable<PullRequestViewModel> GetMyPullRequests(string userId);

        PullRequestViewModel GetById(string id);

        Task DeleteAsync(string id);
    }
}
