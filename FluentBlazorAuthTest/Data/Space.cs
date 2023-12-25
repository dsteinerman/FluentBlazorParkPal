﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using CSharpVitamins;

namespace FluentBlazorAuthTest.Data
{
    /// <summary>
    /// Represents a space that can be booked or rented.
    /// </summary>
    public class Space
    {
        /// <summary>
        /// The unique identifier for the space.
        /// Generated with Short (URL-Safe) GUID and cast to a string.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The identifier of the user hosting the space.
        /// Each individual Space only has one Host (ApplicationUser)
        /// </summary>
        [ForeignKey(nameof(HostUser))]
        public string? HostId { get; set; }

        /// <summary>
        /// EF Core Navigation property for the hosting user.
        /// </summary>
        public virtual ApplicationUser? HostUser { get; set; }

        /// <summary>
        /// Latitude value of the space location, constrained within valid geographical coordinates.
        /// </summary>
        [Range(-90.0, 90.0)]
        [Column(TypeName = "decimal(10, 6)")]
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Longitude value of the space location, constrained within valid geographical coordinates.
        /// </summary>
        [Range(-180.0, 180.0)]
        [Column(TypeName = "decimal(10, 6)")]
        public decimal? Longitude { get; set; }

        /// <summary>
        /// The price set for the space. Consider implementing a pricing algorithm for standardization.
        /// </summary>
        [Required]
        [DataType(DataType.Currency)]
        [RegularExpression(@"[0-9]{1,3}(,[0-9]{3})*(\.[0-9]{2})?",
            ErrorMessage = "Price must be in [##.##] format.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal? Price { get; set; }

        /// <summary>
        /// The size of the space. See the referenced Enum for fixed size categories.
        /// </summary>
        public SizeEnum Size { get; set; }

        /// <summary>
        /// A description of the space. (Max Length: 4000)
        /// </summary>
        [MaxLength(4000)]
        public string? Description { get; set; }

        /// <summary>
        /// Indicates whether the space is available for public booking.
        /// The default is set to true.
        /// </summary>
        public bool IsPublic { get; set; } = true;

        /// <summary>
        /// The date and time when the space was created.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// The date and time of the latest transaction for the space, if any.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? LatestTransaction { get; set; }

        /// <summary>
        /// Navigation property for Bookings
        /// </summary>
        public virtual ICollection<Booking> Bookings { get; set; }

        // Default Constructor for Space
        public Space()
        {
            Id = ShortGuid.NewGuid();
            DateCreated = DateTime.UtcNow;
            Bookings = new HashSet<Booking>();
        }
    }


    public enum SizeEnum
    {
        Compact = 1,
        Standard = 2,
        Xl = 3
    }
}