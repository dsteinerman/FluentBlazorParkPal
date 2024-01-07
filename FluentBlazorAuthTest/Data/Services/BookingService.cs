using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using static FluentBlazorAuthTest.Components.Pages.Book;

namespace FluentBlazorAuthTest.Data.Services
{
    public class BookingService : IBookingService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;


        private readonly ISpaceService _spaceService;

        public BookingService(IDbContextFactory<ApplicationDbContext> contextFactory, ISpaceService spaceService)
        {
            _contextFactory = contextFactory;
            _spaceService = spaceService;
        }

        public async Task AddBookingAsync(Booking booking)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Bookings.Add(booking);
            await context.SaveChangesAsync();
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
            using var context = _contextFactory.CreateDbContext();
            return await context.Bookings.ToListAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(string Id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Bookings.FindAsync(Id);
        }

        public async Task<IEnumerable<Booking>> GetBookingsBySpaceIdAsync(string spaceId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Bookings
                             .Where(b => b.SpaceId == spaceId)
                             .ToListAsync();
        }

        public async Task<Booking> GetBookingStatusByIdAsync(string Id)
        {
            using var context = _contextFactory.CreateDbContext();
            var booking = await context.Bookings.FindAsync(Id);
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
            using var context = _contextFactory.CreateDbContext();
            var dbBooking = await context.Bookings.FindAsync(Id);
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

                context.Entry(dbBooking).CurrentValues.SetValues(booking);
                await context.SaveChangesAsync();
            }
        }

        public Task UpdateBookingPriceBasedOnSpace(string bookingId)
        {
            throw new NotImplementedException();
        }


        public async Task<(IEnumerable<Booking>, int)> GetBookingPageAsync(int pageNumber, int pageSize)
        {
            using var context = _contextFactory.CreateDbContext();
            var query = context.Bookings.AsQueryable();

            int totalBookings = await query.CountAsync();
            var bookings = await query
                .OrderBy(s => s.StartDateTime)
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

            var space = await _spaceService.GetSpaceByIdAsync(booking.SpaceId);
            if (space != null)
            {
                space.IsAvailable = true;
                await _spaceService.UpdateSpaceAsync(space, booking.SpaceId);
            }

        }
    }
}
