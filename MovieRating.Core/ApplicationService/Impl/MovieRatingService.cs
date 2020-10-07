using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieRating.Core.DomainService;
using MovieRating.Entity;

namespace MovieRating.Core.ApplicationService.Impl
{
  public class MovieRatingService : IMovieRatingService
  {
      public IRepository<Rating> RatingRepo { get; set; }

      public MovieRatingService(IRepository<Rating> ratingRepo)
      {
          RatingRepo = ratingRepo ?? throw new ArgumentException("Missing RatingRepo");
      }
      // 1. On input N, what are the number of reviews from reviewer N?
        public int GetNumberOfReviewsFromReviewer(int reviewer)
      {
          return RatingRepo.GetAll().Where((r => r.Reviewer == reviewer)).Count();
      }

        // 2. On input N, what is the average rate that reviewer N had given?
        public double GetAverageRateFromReviewer(int reviewer)
        {

            if (RatingRepo.GetAll().Count == 0)
            {
                throw new ArgumentException("List is empty");

            }

            List<Rating> ratingsfromReviewer = RatingRepo.GetAll().Where(rating => rating.Reviewer == reviewer).ToList();

            double sumRating = 0;

            foreach (Rating rating in ratingsfromReviewer)
            {

                sumRating += rating.Grade;


            }

            


            double AverageRateFromReviewer = sumRating / ratingsfromReviewer.Count;

            return AverageRateFromReviewer;


        }

        // 3. On input N and R, how many times has reviewer N given rate R?
        public int GetNumberOfRatesByReviewer(int reviewer, int rate)
        {
            List<Rating> ratingsfromReviewer = RatingRepo.GetAll().Where(rating => rating.Reviewer == reviewer).ToList();

            int numberOfRates = 0;

            foreach (Rating rating in ratingsfromReviewer)
            {
                if (rating.Grade == rate)
                {
                    numberOfRates++;
                }



            }

            return numberOfRates;

        }

        // 4. On input N, how many have reviewed movie N?
        public int GetNumberOfReviews(int movie)
        {

            List<Rating> ratingsForMovie = RatingRepo.GetAll().Where(rating => rating.Movie == movie).ToList();

            
            return ratingsForMovie.Count;

        }

        // 5. On input N, what is the average rate the movie N had received?
        public double GetAverageRateOfMovie(int movie)
        {
            if (RatingRepo.GetAll().Count == 0)
            {
                throw new ArgumentException("List is empty");

            }

            List<Rating> ratingsForMovie = RatingRepo.GetAll().Where(rating => rating.Movie == movie).ToList();

            double sumRating = 0;

            foreach (Rating rating in ratingsForMovie)
            {

                sumRating += rating.Grade;


            }




            double AverageRateFromReviewer = sumRating / ratingsForMovie.Count;

            return AverageRateFromReviewer;
        }
        // 6. On input N and R, how many times had movie N received rate R?
        public int GetNumberOfRates(int movie, int rate)
        {
            List<Rating> ratingsForMovie = RatingRepo.GetAll().Where(rating => rating.Movie == movie).ToList();

            int numberOfRates = 0;

            foreach (Rating rating in ratingsForMovie)
            {
                if (rating.Grade == rate)
                {
                    numberOfRates++;
                }



            }

            return numberOfRates;
        }

        // 7. What is the id(s) of the movie(s) with the highest number of top rates (5)?
        public IEnumerable<int> GetMoviesWithHighestNumberOfTopRates()
        {
            var movie5 = RatingRepo.GetAll()
                .Where(r => r.Grade == 5)
                .GroupBy(r => r.Movie)
                .Select(group => new {
                Movie = group.Key,
                MovieGrade5 = group.Count()
            });


            int max5 = movie5.Max(grp => grp.MovieGrade5);

            return movie5.Where(grp => grp.MovieGrade5 == max5).Select(grp => grp.Movie);
        }

        // 8. What reviewer(s) had done most reviews?
        public List<int> GetMostProductiveReviewers()
        {
            var reviewers = RatingRepo.GetAll()
                .GroupBy(r => r.Reviewer)
                .Select(group => new {
                    Reviewer = group.Key,
                    Reviewercount = group.Count()
                });

            int max5 = reviewers.Max(grp => grp.Reviewercount);

            return reviewers.Where(grp => grp.Reviewercount == max5).Select(grp => grp.Reviewer).ToList();

        }

        // 9. On input N, what is top N of movies? The score of a movie is its average rate.
        public List<int> GetTopRatedMovies(int amount)
        {

            return RatingRepo.GetAll()
                .GroupBy(r => r.Movie)
                .Select(grp => new
                {
                    Movie = grp.Key,
                    GradeAvg = grp.Average(x => x.Grade)
                })
                .OrderByDescending(grp => grp.GradeAvg)
                .OrderByDescending(grp => grp.GradeAvg)
                .Select(grp => grp.Movie)
                .Take(amount)
                .ToList();

        }

        // 10. On input N, what are the movies that reviewer N has reviewed? The list should be sorted decreasing by rate first, and date secondly.
        public List<int> GetTopMoviesByReviewer(int reviewer)
        {
            List<Rating> ratingsfromReviewer = RatingRepo.GetAll().Where(rating => rating.Reviewer == reviewer).ToList();

            return ratingsfromReviewer.OrderByDescending(rating => rating.Grade).ThenByDescending(rating => rating.Date)
                .Select(rating => rating.Movie).ToList();


        }

        // 11. On input N, who are the reviewers that have reviewed movie N? The list should be sorted decreasing by rate first, and date secondly.
        public List<int> GetReviewersByMovie(int movie)
        {
            List<Rating> ratingsForMovie = RatingRepo.GetAll().Where(rating => rating.Movie == movie).ToList();

            return ratingsForMovie.OrderByDescending(rating => rating.Grade).ThenByDescending(rating => rating.Date)
                .Select(rating => rating.Reviewer).ToList();
        }
    }
}
