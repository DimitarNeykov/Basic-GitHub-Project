namespace GitHub.ViewModels.Commits
{
    using System;

    public class CommitViewModel
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public DateTime CreatedOn { get; set; }

        public string RepositoryName { get; set; }

        public string UserId { get; set; }
    }
}
