using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSharpVitamins;

namespace FluentBlazorAuthTest.Data
{
    /// <summary>
    /// Represents a booking or rental transaction for a space.
    /// </summary>
    public class Booking
    {
        /// <summary>
        /// Unique identifier for the booking.
        /// </summary>
        [Key]
        public string Id { get; set; }
        [ForeignKey(nameof(Space))]
        public string? SpaceId { get; set; }
        public virtual Space Space { get; set; }

        [ForeignKey(nameof(ClientUser))]
        public string? ClientUserId { get; set; }
        public virtual ApplicationUser ClientUser { get; set; }

        /// <summary>
        /// Total price for the booking.
        /// </summary>
        [DataType(DataType.Currency)]
        [RegularExpression(@"[0-9]{1,3}(,[0-9]{3})*(\.[0-9]{2})?",
            ErrorMessage = "Price must be in [##.##] format.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// Identifier for a Booking's current activity.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Current status of the booking (e.g., Pending, Confirmed, Cancelled).
        /// </summary>
        public BookingStatus BookingStatus { get; set; }

        /// <summary>
        /// Status of the payment for the booking (e.g., Paid, Partially Paid, Unpaid).
        /// </summary>
        public PaymentStatus PaymentStatus { get; set; }

        /// <summary>
        /// Date and time when the booking was initially created.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date and time when the booking was last updated.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Start date and time of the booking.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// End date and time of the booking.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime EndDateTime { get; set; } 

        /// <summary>
        /// Any notes or special requests made by the customer.
        /// </summary>
        public string? CustomerNotes { get; set; }

        /// <summary>
        /// Internal notes or comments added by administrators or staff.
        /// </summary>
        public string? AdminNotes { get; set; }

        public Booking()
        {
            Id = ShortGuid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Enum representing the various possible statuses of a booking.
    /// </summary>
    public enum BookingStatus
    {
        Pending = 1,    // Active
        Confirmed = 2,  // Active
        Cancelled = 3,  // Inactive
        Completed = 4   // Inactive
        // Add additional statuses as needed
    }

    /// <summary>
    /// Enum representing the various possible payment statuses for a booking.
    /// </summary>
    public enum PaymentStatus
    {
        Paid = 1,
        PartiallyPaid = 2,
        Unpaid = 3
        // Add additional payment statuses as needed
    }
}
