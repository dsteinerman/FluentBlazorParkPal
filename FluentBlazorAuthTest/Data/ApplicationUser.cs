using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FluentBlazorAuthTest.Data
{
    /// <summary>
    /// Add profile data for application users by adding properties to the ApplicationUser class
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the user's first name / given name.
        /// </summary>
        [Required]
        [ProtectedPersonalData]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name / surname.
        /// </summary>
        [Required]
        [ProtectedPersonalData]
        public string LastName { get; set; }

        /// <summary>
        /// Navigation property for the bookings associated with the user.
        /// </summary>
        public virtual ICollection<Booking> Bookings { get; set; }

        /// <summary>
        /// Navigation property for the spaces hosted by the user.
        /// </summary>
        public virtual ICollection<Space> HostedSpaces { get; set; }

        // Overloads the constructor of the parent class (IdentityUser) to include information related to the user's relationship with other entities
        public ApplicationUser()
        {
            Bookings = new HashSet<Booking>();
            HostedSpaces = new HashSet<Space>();
        }
    }
}