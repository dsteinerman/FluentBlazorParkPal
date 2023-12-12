using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FluentBlazorAuthTest.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the user's first name / given name.
        /// </summary>
        [Required]
        [ProtectedPersonalData]
        public string? FirstName { get; set; }   // Adds a column for a first name in the user table of the database
        /// <summary>
        /// Gets or sets the user's last name / surname.
        /// </summary>
        [Required]
        [ProtectedPersonalData]
        public string? LastName { get; set; }   // Adds a column for a last name in the user table of the database
    }

}
