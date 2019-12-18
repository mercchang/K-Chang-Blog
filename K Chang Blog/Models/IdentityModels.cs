using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace K_Chang_Blog.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "First name must have a minimum length of 1 and maximum length of 50.")]
        public string FirstName { get; set; }

        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Last name must have a minimum length of 1 and maximum length of 50.")]
        public string LastName { get; set; }
        [Display(Name = "Display Name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Display name must have a minimum length of 1 and maximum length of 50.")]
        public string DisplayName { get; set; }

        public string AvatarPath { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }
        }

        //Navigation
        public virtual ICollection<Comment> Comments { get; set; }

        public ApplicationUser()
        {
            Comments = new HashSet<Comment>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<BlogPost> BlogPosts { get; set; }  //created property of ApplicationDbContext class
        public DbSet<Comment> Comments { get; set; }
    }
}