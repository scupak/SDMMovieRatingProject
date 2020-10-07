using JsonReaderProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieRating.Core.ApplicationService.Impl;
using MovieRating.Core.DomainService;
using MovieRating.Entity;
using System;
using System.Collections.Generic;

namespace UnitTestProjectPerformance
{
    [TestClass]
    public class MovieRatingServiceTest
    {

        private static IRepository<Rating> repo;

        [ClassInitialize]

        public static void SetupTest(TestContext tc) {


            repo = new JsonFIleReader("ratings.json");
        
        }



        // 1. On input N, what are the number of reviews from reviewer N?
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [Timeout(4000)]
        public void GetNumberOfReviewsFromReviewerTest(int reviewer)
        {
            MovieRatingService movieRatingService = new MovieRatingService(repo);
            int result = movieRatingService.GetNumberOfReviewsFromReviewer(reviewer);
            Console.WriteLine(result);

        }

        // 2. On input N, what is the average rate that reviewer N had given?
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [Timeout(4000)]
        public void GetAverageRateFromReviewerTest(int reviewer)
        {
            MovieRatingService movieRatingService = new MovieRatingService(repo);
            double result = movieRatingService.GetAverageRateFromReviewer(reviewer);
            Console.WriteLine(result);

        }

        // 3. On input N and R, how many times has reviewer N given rate R?
        [TestMethod]
        [DataRow(1,1)]
        [DataRow(2,2)]
        [DataRow(3,3)]
        [DataRow(4,4)]
        [DataRow(5,5)]
        [Timeout(4000)]
        public void GetNumberOfRatesByReviewerTest(int reviewer, int rate)
        {
            MovieRatingService movieRatingService = new MovieRatingService(repo);
            double result = movieRatingService.GetNumberOfRatesByReviewer(reviewer,rate);
            Console.WriteLine(result);

        }

        // 4. On input N, how many have reviewed movie N?
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [Timeout(4000)]
        public void GetNumberOfReviewsTest(int movie)
        {
            MovieRatingService movieRatingService = new MovieRatingService(repo);
            double result = movieRatingService.GetNumberOfReviews(movie);
            Console.WriteLine(result);

        }

        // 5. On input N, what is the average rate the movie N had received?
        [TestMethod]
        [DataRow(1488844)]
        [DataRow(822109)]
        [DataRow(885013)]
        [DataRow(30878)]
        [DataRow(823519)]
        [Timeout(4000)]
        public void GetAverageRateOfMovieTest(int movie)
        {
            MovieRatingService movieRatingService = new MovieRatingService(repo);
            double result = movieRatingService.GetAverageRateOfMovie(movie);
            Console.WriteLine(result);

        }

        // 6. On input N and R, how many times had movie N received rate R?
        [TestMethod]
        [DataRow(1488844, 1)]
        [DataRow(822109, 2)]
        [DataRow(885013, 3)]
        [DataRow(30878, 4)]
        [DataRow(823519, 5)]
        [Timeout(4000)]
        public void GetNumberOfRatesTest(int movie, int rate)
        {
            MovieRatingService movieRatingService = new MovieRatingService(repo);
            double result = movieRatingService.GetNumberOfRates(movie,rate);
            Console.WriteLine(result);

        }

        // 7. What is the id(s) of the movie(s) with the highest number of top rates (5)?
        [TestMethod]
        [Timeout(4000)]
        public void GetMoviesWithHighestNumberOfTopRatesTest()
        {
            MovieRatingService movieRatingService = new MovieRatingService(repo);
            IEnumerable<int> result = movieRatingService.GetMoviesWithHighestNumberOfTopRates();
            //Console.WriteLine(result);

        }


        /*

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
        */
    }
}
