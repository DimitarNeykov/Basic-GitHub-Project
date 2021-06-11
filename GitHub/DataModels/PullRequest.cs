namespace GitHub.DataModels
{
    using System;
    using Microsoft.AspNetCore.Identity;

    public class PullRequest : BaseDeletableModel<string>
    {
        public PullRequest()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public string RepositoryId { get; set; }

        public Repository Repository { get; set; }
    }
}
