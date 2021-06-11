namespace GitHub.Controllers
{
    using System.Threading.Tasks;
    using GitHub.Services;
    using GitHub.ViewModels.Repositories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;
        private readonly UserManager<IdentityUser> userManager;

        public RepositoriesController(IRepositoriesService repositoriesService, UserManager<IdentityUser> userManager)
        {
            this.repositoriesService = repositoriesService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(RepositoryCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.userManager.GetUserId(this.User);

            await this.repositoriesService.AddAsync(input, userId);

            return this.RedirectToAction("MyRepo");
        }

        [Authorize]
        public IActionResult PublicRepo()
        {
            var userId = this.userManager.GetUserId(this.User);

            var repo = this.repositoriesService.GetAllPublic();

            return this.View(repo);
        }

        [Authorize]
        public IActionResult MyRepo()
        {
            var userId = this.userManager.GetUserId(this.User);

            var repo = this.repositoriesService.GetAllByUser(userId);

            return this.View(repo);
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var repo = this.repositoriesService.GetById(id);

            if (repo.IsPublic == true || repo.UserId == userId)
            {
                return this.View(repo);
            }

            return this.RedirectToAction("MyRepo");
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var repo = this.repositoriesService.GetById(id);

            if (userId == repo.UserId)
            {
                await this.repositoriesService.DeleteAsync(id);
            }

            return this.RedirectToAction("MyRepo");
        }
    }
}
