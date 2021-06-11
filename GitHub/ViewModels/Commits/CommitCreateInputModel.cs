namespace GitHub.ViewModels.Commits
{
    using System.ComponentModel.DataAnnotations;

    public class CommitCreateInputModel
    {
        [Required]
        [MaxLength(100)]
        [MinLength(10)]
        public string Message { get; set; }

        public string RepositoryId { get; set; }

        public string RepositoryName { get; set; }
    }
}
