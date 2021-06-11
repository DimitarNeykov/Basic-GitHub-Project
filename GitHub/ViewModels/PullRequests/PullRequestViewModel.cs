namespace GitHub.ViewModels.PullRequests
{
    using System;

    public class PullRequestViewModel
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string RepositoryName { get; set; }

        public string UserId { get; set; }
    }
}
