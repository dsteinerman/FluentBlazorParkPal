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

        public Task AddBookingAsync(Booking booking)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> CreateBookingAsync(Booking booking)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBookingAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetBookingByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Booking>> GetBookingsBySpaceIdAsync(string spaceId)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetBookingStatusByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetPaymentStatusByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetSpacePriceByIdAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBookingAsync(Booking booking, string Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBookingPriceBasedOnSpace(string bookingId)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePaymentStatusPaidByIdAsync(string bookingId, BookingStatus bookingstatus)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePaymentStatusUnpaidByIdAsync(string bookingId, BookingStatus bookingstatus)
        {
            throw new NotImplementedException();
        }
    }
}
