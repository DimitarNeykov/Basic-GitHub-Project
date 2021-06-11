namespace GitHub.ViewModels.Repositories
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class RepositoryCreateInputModel
    {
        [MinLength(3)]
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Public")]
        public bool IsPublic { get; set; }
    }
}
