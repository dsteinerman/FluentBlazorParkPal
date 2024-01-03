using Microsoft.EntityFrameworkCore;
using static FluentBlazorAuthTest.Components.Pages.Book;

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
                dbBooking.IsActive = booking.IsActive;
                dbBooking.SpaceId = booking.SpaceId;

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

        public async Task<(IEnumerable<Booking>, int)> GetBookingPageAsync(int pageNumber, int pageSize)
        {
            var query = _context.Bookings.AsQueryable();

            // Filter the query to include only public spaces
            //var query = _context.Bookings.Where(s => s.IsPublic).AsQueryable();

            int totalBookings = await query.CountAsync();

            var bookings = await query
                .OrderBy(s => s.StartDateTime) //order
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (bookings, totalBookings);
        }

        public async Task CancelBookingAsync(string Id)
        {

            var booking = await GetBookingByIdAsync(Id);

            booking.IsActive = false;

            await UpdateBookingAsync(booking, Id);

            // Retrieve the associated space
            var space = await _spaceService.GetSpaceByIdAsync((string)booking.SpaceId);
            if (space != null)
            {
                space.IsAvailable = true;

                await _spaceService.UpdateSpaceAsync(space, Id);
            }



        }
    }
}
