using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FluentBlazorAuthTest.Data
{
    /// <summary>
    /// Represents a space that can be booked or rented.
    /// </summary>
    public class Space
    {
        //public Space()
        //{
        //    Id = Guid.NewGuid().ToString(); // Initialize with a new GUID as string
        //    DateCreated = DateTime.UtcNow; // Set creation date to current date and time
        //}

        /// <summary>
        /// The unique identifier for the space.
        /// </summary>
        [Key]
        public string Id { get; set; } = default!;

        /// <summary>
        /// The identifier of the user hosting the space.
        /// </summary>
        [Required]
        [ForeignKey("HostUser")]
        public string? HostId { get; set; }

        /// <summary>
        /// EF Core Navigation property for the hosting user.
        /// </summary>
        public ApplicationUser HostUser { get; set; }

        /// <summary>
        /// The identifier of the client user. If NULL, the space is vacant.
        /// </summary>
        [ForeignKey("ClientUser")]
        public string? ClientId { get; set; }

        /// <summary>
        /// EF Core Navigation property for the client user.
        /// </summary>
        public ApplicationUser ClientUser { get; set; }

        /// <summary>
        /// Latitude value of the space location, constrained within valid geographical coordinates.
        /// </summary>
        [Range(-90.0, 90.0)]
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Longitude value of the space location, constrained within valid geographical coordinates.
        /// </summary>
        [Range(-180.0, 180.0)]
        public decimal? Longitude { get; set; }

        /// <summary>
        /// The price set for the space. Consider implementing a pricing algorithm for standardization.
        /// </summary>
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        public decimal? Price { get; set; }

        /// <summary>
        /// The size of the space. See the referenced Enum for fixed size categories.
        /// </summary>
        public SizeEnum? Size { get; set; }

        /// <summary>
        /// A description of the space, with a maximum length of 4000 characters.
        /// </summary>
        [MaxLength(4000)]
        public string? Description { get; set; }

        /// <summary>
        /// Indicates whether the space is available for public booking.
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// The date and time when the space was created.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The date and time of the latest transaction for the space, if any.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? LatestTransaction { get; set; }

        // Considerations for future enhancements:
        // - Fields for photos and other media associated with the space.
        // - Integration with Maps API for location-based functionalities.
    }

    public enum SizeEnum
    {
        Compact,
        Standard,
        XL
    }
}
