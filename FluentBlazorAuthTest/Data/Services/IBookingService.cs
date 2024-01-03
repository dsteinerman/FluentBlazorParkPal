
namespace FluentBlazorAuthTest.Data.Services;

public interface IBookingService
{
    Task<Booking> CreateBookingAsync(Booking booking);

    Task<Booking> GetBookingByIdAsync(string Id);

    //Task<Booking> GetBookingByClientIdAsync(int clientUserId);

    Task<Booking> GetPaymentStatusByIdAsync(string Id);

    Task<Booking> GetBookingStatusByIdAsync(string Id);

    Task<decimal> GetSpacePriceByIdAsync(string Id);

    // Task<Booking> GetBookingByHostAsync(String ClientUserId);

    Task<IEnumerable<Booking>> GetAllBookingsAsync();

    Task UpdateBookingAsync(Booking booking, string Id);

    Task DeleteBookingAsync(string Id);

    Task AddBookingAsync(Booking booking);

    Task UpdateBookingPriceBasedOnSpace(string bookingId);

    Task UpdatePaymentStatusUnpaidByIdAsync(string bookingId, BookingStatus bookingstatus);

    Task UpdatePaymentStatusPaidByIdAsync(string bookingId, BookingStatus bookingstatus);

    Task<IEnumerable<Booking>> GetBookingsBySpaceIdAsync(string spaceId);

}
