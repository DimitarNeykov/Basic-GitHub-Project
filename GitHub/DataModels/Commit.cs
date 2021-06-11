namespace GitHub.DataModels
{
    using System;
    using Microsoft.AspNetCore.Identity;

    public class Commit : BaseDeletableModel<string>
    {
        public Commit()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Message { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public string RepositoryId { get; set; }

        public Repository Repository { get; set; }
    }
}
