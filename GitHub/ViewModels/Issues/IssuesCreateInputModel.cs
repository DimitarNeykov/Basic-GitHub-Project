namespace GitHub.ViewModels.Issues
{
    using System.ComponentModel.DataAnnotations;

    public class IssuesCreateInputModel
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(10)]
        public string Comment { get; set; }

        public string RepositoryId { get; set; }

        public string RepositoryName { get; set; }
    }
}
