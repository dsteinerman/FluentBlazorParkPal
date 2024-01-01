using Microsoft.EntityFrameworkCore;

namespace FluentBlazorAuthTest.Data.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;

        private readonly ISpaceService _spaceService;

        public BookingService(ApplicationDbContext context, ISpaceService spaceService)
        {
            _context = context;
            _spaceService = spaceService;
        }

        public async Task AddBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

        }

        public Task<Booking> CreateBookingAsync(Booking booking)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBookingAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            var result = await _context.Bookings.ToListAsync();
            return result;
        }

        public async Task<Booking> GetBookingByIdAsync(string Id)
        {
            var result = await _context.Bookings.FindAsync(Id);
            return result;
        }

        public async Task<Booking> GetBookingStatusByIdAsync(string Id)
        {
            var booking = await _context.Bookings.FindAsync(Id);
            return booking;

        }

        public async Task<Booking> GetPaymentStatusByIdAsync(string Id)
        {
            var booking = await _context.Bookings.FindAsync(Id);
            return booking;

        }

        public async Task UpdateBookingPriceBasedOnSpace(string bookingId)
        {
            // Find the booking by ID
            var booking = await _context.Bookings.FindAsync(bookingId) ?? throw new KeyNotFoundException($"Booking not found with ID: {bookingId}");

            // Fetch the price of the space associated with this booking
            decimal spacePrice = await _spaceService.GetSpacePriceByIdAsync(booking.SpaceId);

            // Update the booking's price
            booking.Price = spacePrice;

            // Save the changes in the database
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookingAsync(Booking booking, string Id)
        {
            var dbBooking = await _context.Bookings.FindAsync(Id);
            if (dbBooking != null)
            {
                dbBooking.StartDateTime = booking.StartDateTime;
                dbBooking.EndDateTime = booking.EndDateTime;
                dbBooking.UpdatedAt = DateTime.Now;
                dbBooking.AdminNotes = booking.AdminNotes;
                dbBooking.BookingStatus = booking.BookingStatus;
                dbBooking.CustomerNotes = booking.CustomerNotes;
                dbBooking.Price = booking.Price;
                dbBooking.PaymentStatus = booking.PaymentStatus;

                await _context.SaveChangesAsync();

            }
        }

        public Task<decimal> GetSpacePriceByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePaymentStatusUnpaidByIdAsync(string bookingId, BookingStatus bookingstatus)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);

            booking.PaymentStatus = PaymentStatus.Unpaid;

            await _context.SaveChangesAsync();


        }

        public async Task UpdatePaymentStatusPaidByIdAsync(string bookingId, BookingStatus bookingstatus)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);

            booking.PaymentStatus = PaymentStatus.Unpaid;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsBySpaceIdAsync(string spaceId)
        {
            return await _context.Bookings
                             .Where(b => b.SpaceId == spaceId)
                             .ToListAsync();
        }
    }
}
