namespace GitHub.ViewModels.Issues
{
    using System;

    public class IssuesViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedOn { get; set; }

        public string RepositoryName { get; set; }

        public string UserId { get; set; }
    }
}
