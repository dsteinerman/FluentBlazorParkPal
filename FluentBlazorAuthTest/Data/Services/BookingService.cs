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

        public async Task<IEnumerable<Booking>> GetBookingsBySpaceIdAsync(string spaceId)
        {
            return await _context.Bookings
                             .Where(b => b.SpaceId == spaceId)
                             .ToListAsync();
        }

        public async Task<Booking> GetBookingStatusByIdAsync(string Id)
        {
            var booking = await _context.Bookings.FindAsync(Id);
            return booking;
        }

        public Task<Booking> GetPaymentStatusByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetSpacePriceByIdAsync(string Id)
        {
            throw new NotImplementedException();
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

        public Task UpdateBookingPriceBasedOnSpace(string bookingId)
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

    }
}
