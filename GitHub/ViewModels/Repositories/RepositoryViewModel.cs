namespace GitHub.ViewModels.Repositories
{
    public class RepositoryViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public bool IsPublic { get; set; }

        public string UserId { get; set; }
    }
}
