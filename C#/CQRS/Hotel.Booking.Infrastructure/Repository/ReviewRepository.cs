using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hotel.Booking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Booking.Infrastructure.Repository
{
    public class ReviewRepository(HotelBookingDbContext _context) : IReviewRepository
    {
        public async Task<domain.Review?> GetByIdAsync(Guid id)
        {
            return await _context.Reviews
                .Include(r => r.Customer)
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<domain.Review>> GetAllAsync()
        {
            return await _context.Reviews
                .Include(r => r.Customer)
                .Include(r => r.Hotel)
                .ToListAsync();
        }

        public async Task AddAsync(domain.Review review)
        {
            await _context.Reviews.AddAsync(review);
        }

        public void Delete(domain.Review review)
        {
            _context.Reviews.Remove(review);
        }
    }
}
