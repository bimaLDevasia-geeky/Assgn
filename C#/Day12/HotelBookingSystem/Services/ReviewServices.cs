using HotelBookingSystem.Context;
using HotelBookingSystem.Models;
using HotelBookingSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Services
{
    public class ReviewServices : IReview
    {
        private readonly HotelBookingSystemContext _context;

        public ReviewServices(HotelBookingSystemContext context)
        {
            _context = context;
        }

        public async Task<Review> AddReview(Review review)
        {
            // Verify if hotel and customer exist
            var hotel = await _context.Hotels.FindAsync(review.HostelId);
            if (hotel == null)
                throw new KeyNotFoundException("Hotel not found");

            var customer = await _context.Customers.FindAsync(review.CustomerId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            review.ReviewDate = DateTime.Now;
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            return await _context.Reviews
                .Include(r => r.Hotel)
                .Include(r => r.Customer)
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingForHotel(int hotelId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.HostelId == hotelId)
                .ToListAsync();

            if (!reviews.Any())
                return 0;

            return reviews.Average(r => r.Rating);
        }

        public async Task<Review> GetReviewById(int id)
        {
            var review = await _context.Reviews
                .Include(r => r.Hotel)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
                throw new KeyNotFoundException("Review not found");

            return review;
        }

        public async Task<IEnumerable<Review>> GetReviewsByCustomer(int customerId)
        {
            return await _context.Reviews
                .Include(r => r.Hotel)
                .Include(r => r.Customer)
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByHotel(int hotelId)
        {
            return await _context.Reviews
                .Include(r => r.Hotel)
                .Include(r => r.Customer)
                .Where(r => r.HostelId == hotelId)
                .ToListAsync();
        }

        public async Task RemoveReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                throw new KeyNotFoundException("Review not found");

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task<Review> UpdateReview(Review review)
        {
            var existingReview = await _context.Reviews.FindAsync(review.Id);
            if (existingReview == null)
                throw new KeyNotFoundException("Review not found");

            existingReview.Rating = review.Rating;
            existingReview.Comment = review.Comment;

            await _context.SaveChangesAsync();
            return existingReview;
        }
    }
}