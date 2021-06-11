namespace GitHub.Controllers
{
    using System.Threading.Tasks;
    using GitHub.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PullRequestsController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IRepositoriesService repositoriesService;
        private readonly IPullRequestsService pullRequestsService;

        public PullRequestsController(
            UserManager<IdentityUser> userManager,
            IRepositoriesService repositoriesService,
            IPullRequestsService pullRequestsService)
        {
            this.userManager = userManager;
            this.repositoriesService = repositoriesService;
            this.pullRequestsService = pullRequestsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(string repositoryId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var repository = this.repositoriesService.GetById(repositoryId);

            if (repository.IsPublic == false && repository.UserId != userId)
            {
                return this.RedirectToAction("PublicRepo", "Repositories");
            }

            await this.pullRequestsService.AddAsync(repositoryId, userId);

            return this.RedirectToAction("MyPullRequests");
        }

        [Authorize]
        public IActionResult MyPullRequests()
        {
            var userId = this.userManager.GetUserId(this.User);

            var pullRequests = this.pullRequestsService.GetMyPullRequests(userId);

            return this.View(pullRequests);
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var pullRequests = this.pullRequestsService.GetById(id);

            if (userId == pullRequests.UserId)
            {
                await this.pullRequestsService.DeleteAsync(id);
            }

            return this.RedirectToAction("MyPullRequests");
        }
    }
}
