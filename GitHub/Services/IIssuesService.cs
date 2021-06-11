namespace GitHub.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GitHub.ViewModels.Issues;

    public interface IIssuesService
    {
        Task AddAsync(IssuesCreateInputModel inputModel, string userId);

        IEnumerable<IssuesViewModel> GetMyIssues(string userId);

        IssuesViewModel GetById(string id);

        Task DeleteAsync(string id);
    }
}
