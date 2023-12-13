using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentBlazorAuthTest.Data.Models
{
    /// <summary>
    /// Represents a space that can be booked or rented.
    /// </summary>
    public class Space
    {
        /// <summary>
        /// The unique identifier for the space.
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// The identifier of the user hosting the space.
        /// </summary>
        [ForeignKey("HostUser")]
        public string? HostId { get; set; }

        /// <summary>
        /// EF Core Navigation property for the hosting user.
        /// </summary>
        public ApplicationUser? HostUser { get; set; }

        /// <summary>
        /// The identifier of the client user. If NULL, the space is vacant.
        /// </summary>
        [ForeignKey("ClientUser")]
        public string? ClientId { get; set; }

        /// <summary>
        /// EF Core Navigation property for the client user.
        /// </summary>
        public ApplicationUser? ClientUser { get; set; }

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
        [Required]
        [DataType(DataType.Currency)]
        [RegularExpression(@"[0-9]{1,3}(,[0-9]{3})*(\.[0-9]{2})?",
         ErrorMessage = "Price must be in [##.##] format.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        public double? Price { get; set; }

        /// <summary>
        /// The size of the space. See the referenced Enum for fixed size categories.
        /// Currently set as a string for testing purposes
        /// </summary>
        public string? Size { get; set; } = "Standard";

        /// <summary>
        /// A description of the space. (Max Length: 4000)
        /// </summary>
        [MaxLength(4000)]
        public string? Description { get; set; }

        /// <summary>
        /// URLs of images associated with the space.
        /// </summary>
        public List<string>? ImageUrls { get; set; } = [];

        /// <summary>
        /// Indicates whether the space is available for public booking.
        /// The default is set to true.
        /// </summary>
        /// 
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

        // Considerations for future enhancements:
        // - Fields for photos and other media associated with the space.
        // - Integration with Maps API for location-based functionality.
    }

    public enum SizeEnum
    {
        Compact,
        Standard,
        Xl
    }
}
