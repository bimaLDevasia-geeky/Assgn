using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services.Interfaces
{
    public interface IReview
    {
        Task<Review> AddReview(Review review);
        Task<Review> GetReviewById(int id);
        Task<IEnumerable<Review>> GetAllReviews();
        Task<IEnumerable<Review>> GetReviewsByHotel(int hotelId);
        Task<IEnumerable<Review>> GetReviewsByCustomer(int customerId);
        Task<double> GetAverageRatingForHotel(int hotelId);
        Task<Review> UpdateReview(Review review);
        Task RemoveReview(int id);
    }
}