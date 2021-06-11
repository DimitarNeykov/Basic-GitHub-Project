namespace GitHub.Controllers
{
    using System.Threading.Tasks;
    using GitHub.Services;
    using GitHub.ViewModels.Issues;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class IssuesController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IRepositoriesService repositoriesService;
        private readonly IIssuesService issuesService;

        public IssuesController(
            UserManager<IdentityUser> userManager, 
            IRepositoriesService repositoriesService,
            IIssuesService issuesService)
        {
            this.userManager = userManager;
            this.repositoriesService = repositoriesService;
            this.issuesService = issuesService;
        }

        [Authorize]
        public IActionResult Create(string repositoryId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var repository = this.repositoriesService.GetById(repositoryId);

            if (repository.IsPublic == false && repository.UserId != userId)
            {
                return this.RedirectToAction("PublicRepo", "Repositories");
            }

            var model = new IssuesCreateInputModel()
            {
                RepositoryId = repositoryId,
                RepositoryName = repository.Name,
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(IssuesCreateInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var repository = this.repositoriesService.GetById(input.RepositoryId);

            if (repository.IsPublic == false && repository.UserId != userId)
            {
                return this.RedirectToAction("PublicRepo", "Repositories");
            }

            await this.issuesService.AddAsync(input, userId);

            return this.RedirectToAction("MyIssues");
        }

        [Authorize]
        public IActionResult MyIssues()
        {
            var userId = this.userManager.GetUserId(this.User);

            var issues = this.issuesService.GetMyIssues(userId);

            return this.View(issues);
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var issues = this.issuesService.GetById(id);

            if (userId == issues.UserId)
            {
                await this.issuesService.DeleteAsync(id);
            }

            return this.RedirectToAction("MyIssues");
        }
    }
}
