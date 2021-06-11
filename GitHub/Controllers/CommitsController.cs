namespace GitHub.Controllers
{
    using System.Threading.Tasks;
    using GitHub.Services;
    using GitHub.ViewModels.Commits;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CommitsController : Controller
    {
        private readonly IRepositoriesService repositoriesService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ICommitsService commitsService;

        public CommitsController(
            IRepositoriesService repositoriesService, 
            UserManager<IdentityUser> userManager,
            ICommitsService commitsService)
        {
            this.repositoriesService = repositoriesService;
            this.userManager = userManager;
            this.commitsService = commitsService;
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

            var model = new CommitCreateInputModel
            {
                RepositoryId = repositoryId,
                RepositoryName = repository.Name,
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CommitCreateInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var repository = this.repositoriesService.GetById(input.RepositoryId);

            if (repository.IsPublic == false && repository.UserId != userId)
            {
                return this.RedirectToAction("PublicRepo", "Repositories");
            }

            await this.commitsService.AddAsync(input, userId);

            return this.RedirectToAction("MyCommits");
        }

        [Authorize]
        public IActionResult MyCommits()
        {
            var userId = this.userManager.GetUserId(this.User);

            var commits = this.commitsService.GetMyCommits(userId);
            
            return this.View(commits);
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var commit = this.commitsService.GetById(id);

            if (userId == commit.UserId)
            {
                await this.commitsService.DeleteAsync(id);
            }

            return this.RedirectToAction("MyCommits");
        }
    }
}
