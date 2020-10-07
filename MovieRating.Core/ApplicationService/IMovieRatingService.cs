using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRating.Core.ApplicationService
{
  public interface IMovieRatingService
    {
        /*
         *Each rating describes a review of one movie by one reviewer. The result of the review is grade between 1 and 5 both inclusive. The date of the review is also included. An example of a rating is
          {Reviewer:563, Movie:781196, Grade:2, Date:'2003-06-06'}
          This means, that reviewer with id 563 had reviewed film with id 781196 June 6, 2003. The reviewer rated the movie with a 2 (1 to 5 inclusive is a possible rating).
          Construct a class containing a function for each of the following questions (with proper signatures):
         *
         */

        // 1. On input N, what are the number of reviews from reviewer N?
        int GetNumberOfReviewsFromReviewer(int reviewer);

        // 2. On input N, what is the average rate that reviewer N had given?
        double GetAverageRateFromReviewer(int reviewer);

        // 3. On input N and R, how many times has reviewer N given rate R?
        int GetNumberOfRatesByReviewer(int reviewer, int rate);

        // 4. On input N, how many have reviewed movie N?
        int GetNumberOfReviews(int movie);

        // 5. On input N, what is the average rate the movie N had received?
        double GetAverageRateOfMovie(int movie);

        // 6. On input N and R, how many times had movie N received rate R?
        int GetNumberOfRates(int movie, int rate);

        // 7. What is the id(s) of the movie(s) with the highest number of top rates (5)?
        List<int> GetMoviesWithHighestNumberOfTopRates();

        // 8. What reviewer(s) had done most reviews?
        List<int> GetMostProductiveReviewers();

        // 9. On input N, what is top N of movies? The score of a movie is its average rate.
        List<int> GetTopRatedMovies(int amount);

        // 10. On input N, what are the movies that reviewer N has reviewed? The list should be sorted decreasing by rate first, and date secondly.
        List<int> GetTopMoviesByReviewer(int reviewer);

        // 11. On input N, who are the reviewers that have reviewed movie N? The list should be sorted decreasing by rate first, and date secondly.
        List<int> GetReviewersByMovie(int movie);
    }
}
