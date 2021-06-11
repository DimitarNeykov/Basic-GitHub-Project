namespace GitHub.DataModels
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class Repository : BaseDeletableModel<string>
    {
        public Repository()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Issues = new HashSet<Issue>();
            this.Commits = new HashSet<Commit>();
            this.PullRequests = new HashSet<PullRequest>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public ICollection<Issue> Issues { get; set; }

        public ICollection<Commit> Commits { get; set; }

        public ICollection<PullRequest> PullRequests { get; set; }
    }
}
