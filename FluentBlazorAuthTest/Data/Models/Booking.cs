using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentBlazorAuthTest.Data.Models
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
        public Guid BookingId { get; set; }

        /// <summary>
        /// Foreign key for the space being booked.
        /// </summary>
        [ForeignKey("NewSpace")]
        public string SpaceId { get; set; }

        /// <summary>
        /// Navigation property for the space being booked.
        /// </summary>
        public Space Space { get; set; }

        /// <summary>
        /// Foreign key for the client user who is making the booking.
        /// </summary>
        [ForeignKey("ClientUser")]
        public string ClientId { get; set; }

        /// <summary>
        /// Navigation property for the client user.
        /// </summary>
        public ApplicationUser? ClientUser { get; set; }

        /// <summary>
        /// Start date and time of the booking.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date and time of the booking.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Total price for the booking.
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        /// <summary>
        /// Current status of the booking (e.g., Pending, Confirmed, Cancelled).
        /// </summary>
        public BookingStatus Status { get; set; }

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
        /// Any notes or special requests made by the customer.
        /// </summary>
        public string? CustomerNotes { get; set; }

        /// <summary>
        /// Internal notes or comments added by administrators or staff.
        /// </summary>
        public string? AdminNotes { get; set; }
    }

    /// <summary>
    /// Enum representing the various possible statuses of a booking.
    /// </summary>
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled
        // Add additional statuses as needed
    }

    /// <summary>
    /// Enum representing the various possible payment statuses for a booking.
    /// </summary>
    public enum PaymentStatus
    {
        Paid,
        PartiallyPaid,
        Unpaid
        // Add additional payment statuses as needed
    }
}
