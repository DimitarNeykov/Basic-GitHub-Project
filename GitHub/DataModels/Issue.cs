namespace GitHub.DataModels
{
    using System;
    using Microsoft.AspNetCore.Identity;

    public class Issue : BaseDeletableModel<string>
    {
        public Issue()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Title { get; set; }

        public string Comment { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public string RepositoryId { get; set; }

        public Repository Repository { get; set; }

    }
}
