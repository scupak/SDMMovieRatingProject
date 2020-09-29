using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using MovieRating.Core.ApplicationService.Impl;
using MovieRating.Core.DomainService;
using MovieRating.Entity;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    public class MovieRatingServiceTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        // Fake store for repository
        private List<Rating> dataStore;
        private Mock<IRepository<Rating>> repoMock;

        public MovieRatingServiceTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            dataStore = new List<Rating>();

            repoMock = new Mock<IRepository<Rating>>();
            repoMock.SetupAllProperties();

            repoMock.Setup(x => x.GetAll()).Returns(() => dataStore);
        }

        //tests if the setup class works

        [Fact]
        public void GenericSetupTest()
        {
            GenericSetup();
            _testOutputHelper.WriteLine(dataStore.Count.ToString());

            for(int i = 0; i < dataStore.Count; i++){

                _testOutputHelper.WriteLine(dataStore[i].ToString());

            }
            Assert.True(dataStore.Count > 0);

        }
        [Fact]
        public void CreateMovieRatingService()
        {
            IRepository<Rating> repo = repoMock.Object;

            MovieRatingService mrs = new MovieRatingService(repo);

            Assert.Empty(dataStore);
        }

        //1. On input N, what are the number of reviews from reviewer N?
        [Theory]
        [InlineData(1,4)]
        [InlineData(2, 2)]
        public void GetNumberOfReviewsFromReviewerTest(int reviewerid, int expected)
        {
            //arrange
            IRepository<Rating> repo = repoMock.Object;
            MovieRatingService mrs = new MovieRatingService(repo);

            dataStore = new List<Rating>
            {
                new Rating(1,1,2,new DateTime(2003,6,6)),
                new Rating(1, 3, 3, new DateTime(2005,9,6)),
                new Rating(1, 4, 4, new DateTime(2004,12,23)),
                new Rating(1, 6, 2, new DateTime(2005,1,23)),
                new Rating(3, 5, 2, new DateTime(2005,2,23)),
                new Rating(4, 5, 2, new DateTime(2005,3,23)),
                new Rating(5, 5, 2, new DateTime(2005,4,23)),
                new Rating(6, 5, 2, new DateTime(2005,5,23)),
                new Rating(2,2,3,new DateTime(2002,1,22)),
                new Rating(2,3,4,new DateTime(2001,12,1))

            };

            //act
            int NumberOfReviews = mrs.GetNumberOfReviewsFromReviewer(reviewerid);

            //assert
            Assert.Equal(expected, NumberOfReviews);

            repoMock.Verify(rep => rep.GetAll(), Times.Once);
        }

        // 2. On input N, what is the average rate that reviewer N had given?
        [Theory]
        [InlineData(1,2.75)]
        public void GetAverageRateFromReviewer(int reviewer, double expected)
        {
            //arrange
            IRepository<Rating> repo = repoMock.Object;
            MovieRatingService mrs = new MovieRatingService(repo);

            dataStore = new List<Rating>
            {
                new Rating(1,1,2,new DateTime(2003,6,6)),
                new Rating(1, 3, 3, new DateTime(2005,9,6)),
                new Rating(1, 4, 4, new DateTime(2004,12,23)),
                new Rating(1, 6, 2, new DateTime(2005,1,23)),
                new Rating(3, 5, 2, new DateTime(2005,2,23)),
                new Rating(4, 5, 2, new DateTime(2005,3,23)),
                new Rating(5, 5, 2, new DateTime(2005,4,23)),
                new Rating(6, 5, 2, new DateTime(2005,5,23)),
                new Rating(2,2,3,new DateTime(2002,1,22)),
                new Rating(2,3,4,new DateTime(2001,12,1))

            };

            //act
            double averageRateFromReviewer = mrs.GetAverageRateFromReviewer(reviewer);

            //assert
            Assert.True(Math.Abs(expected - averageRateFromReviewer) < 0);

            repoMock.Verify(rep => rep.GetAll(),Times.Once);


        }

        // 3. On input N and R, how many times has reviewer N given rate R?
        [Theory]
        [InlineData(1,2,2)]
        public void GetNumberOfRatesByReviewer(int reviewer, int rate,int expected)
        {
            //arrange
            IRepository<Rating> repo = repoMock.Object;
            MovieRatingService mrs = new MovieRatingService(repo);
            dataStore = new List<Rating>
            {
                new Rating(1,1,2,new DateTime(2003,6,6)),
                new Rating(1, 3, 3, new DateTime(2005,9,6)),
                new Rating(1, 4, 4, new DateTime(2004,12,23)),
                new Rating(1, 6, 2, new DateTime(2005,1,23)),
                new Rating(3, 5, 2, new DateTime(2005,2,23)),
                new Rating(4, 5, 2, new DateTime(2005,3,23)),
                new Rating(5, 5, 2, new DateTime(2005,4,23)),
                new Rating(6, 5, 2, new DateTime(2005,5,23)),
                new Rating(2,2,3,new DateTime(2002,1,22)),
                new Rating(2,3,4,new DateTime(2001,12,1))

            };


            //act
            int numberOfRatesByReviewer = mrs.GetNumberOfRatesByReviewer(reviewer,rate);


            //assert 
            Assert.Equal(expected,numberOfRatesByReviewer);


        }

        // 4. On input N, how many have reviewed movie N?
        [Theory]
        [InlineData(1,1)]
        public void GetNumberOfReviews(int movie, int expected)
        {

            //arrange
            IRepository<Rating> repo = repoMock.Object;
            MovieRatingService mrs = new MovieRatingService(repo);
            dataStore = new List<Rating>
            {
                new Rating(1,1,2,new DateTime(2003,6,6)),
                new Rating(1, 3, 3, new DateTime(2005,9,6)),
                new Rating(1, 4, 4, new DateTime(2004,12,23)),
                new Rating(1, 6, 2, new DateTime(2005,1,23)),
                new Rating(3, 5, 2, new DateTime(2005,2,23)),
                new Rating(4, 5, 2, new DateTime(2005,3,23)),
                new Rating(5, 5, 2, new DateTime(2005,4,23)),
                new Rating(6, 5, 2, new DateTime(2005,5,23)),
                new Rating(2,2,3,new DateTime(2002,1,22)),
                new Rating(2,3,4,new DateTime(2001,12,1))

            };

            //act 
            int NumberOfReviews = mrs.GetNumberOfReviews(movie);


            //assert 
            Assert.Equal(expected, NumberOfReviews);


        }

        // 5. On input N, what is the average rate the movie N had received?
        [Theory]
        [InlineData(1,2.5)]
        public void GetAverageRateOfMovie(int movie, double expected)
        {
            //arrange
            IRepository<Rating> repo = repoMock.Object;
            MovieRatingService mrs = new MovieRatingService(repo);
            dataStore = new List<Rating>
            {
                new Rating(1, 1, 2, new DateTime(2003, 6, 6)),
                new Rating(1, 3, 3, new DateTime(2005, 9, 6)),
                new Rating(1, 4, 4, new DateTime(2004, 12, 23)),
                new Rating(1, 6, 2, new DateTime(2005, 1, 23)),
                new Rating(3, 5, 2, new DateTime(2005, 2, 23)),
                new Rating(4, 5, 2, new DateTime(2005, 3, 23)),
                new Rating(5, 5, 3, new DateTime(2005, 4, 23)),
                new Rating(6, 5, 3, new DateTime(2005, 5, 23)),
                new Rating(2, 2, 3, new DateTime(2002, 1, 22)),
                new Rating(2, 3, 4, new DateTime(2001, 12, 1))
            };


            //act 
                double AverageRateOfMovie = mrs.GetAverageRateFromReviewer(movie);

                Assert.Equal(expected, AverageRateOfMovie);


        
        }

        // 6. On input N and R, how many times had movie N received rate R?
        [Theory]
        [InlineData(1,1,1)]
        public void GetNumberOfRates(int movie, int rate, int expected)
        {
            //arrange
            IRepository<Rating> repo = repoMock.Object;
            MovieRatingService mrs = new MovieRatingService(repo);
            dataStore = new List<Rating>
            {
                new Rating(1, 1, 2, new DateTime(2003, 6, 6)),
                new Rating(1, 3, 3, new DateTime(2005, 9, 6)),
                new Rating(1, 4, 4, new DateTime(2004, 12, 23)),
                new Rating(1, 6, 2, new DateTime(2005, 1, 23)),
                new Rating(3, 5, 2, new DateTime(2005, 2, 23)),
                new Rating(4, 5, 2, new DateTime(2005, 3, 23)),
                new Rating(5, 5, 3, new DateTime(2005, 4, 23)),
                new Rating(6, 5, 3, new DateTime(2005, 5, 23)),
                new Rating(2, 2, 3, new DateTime(2002, 1, 22)),
                new Rating(2, 3, 4, new DateTime(2001, 12, 1))
            };


            //act 
            int NumberOfRates = mrs.GetNumberOfRates(movie,rate);

            Assert.Equal(expected, NumberOfRates);
        }

        // 7. What is the id(s) of the movie(s) with the highest number of top rates (5)?
        [Fact]
        public void GetMoviesWithHighestNumberOfTopRates()
        {
        }

        // 8. What reviewer(s) had done most reviews?
        [Fact]
        public void GetMostProductiveReviewers()
        {
        }

        // 9. On input N, what is top N of movies? The score of a movie is its average rate.
        [Theory]
        [InlineData(1)]
        public void GetTopRatedMovies(int amount)
        {
        }

        // 10. On input N, what are the movies that reviewer N has reviewed? The list should be sorted decreasing by rate first, and date secondly.
        [Theory]
        [InlineData(1)]
        public void GetTopMoviesByReviewer(int reviewer)
        {
        }

        // 11. On input N, who are the reviewers that have reviewed movie N? The list should be sorted decreasing by rate first, and date secondly.
        [Theory]
        [InlineData(1)]
        public void GetReviewersByMovie(int movie)
        {
        }

        private void GenericSetup()
        {
            dataStore.Clear();
            
            dataStore = new List<Rating>
            {
                new Rating(1,1,2,new DateTime(2003,6,6)),
                new Rating(1, 3, 3, new DateTime(2005,9,6)),
                new Rating(1, 4, 4, new DateTime(2004,12,23)),
                new Rating(1, 4, 2, new DateTime(2005,1,23)),
                new Rating(3, 5, 2, new DateTime(2005,2,23)),
                new Rating(4, 5, 2, new DateTime(2005,3,23)),
                new Rating(5, 5, 2, new DateTime(2005,4,23)),
                new Rating(6, 5, 2, new DateTime(2005,5,23)),
                new Rating(2,2,3,new DateTime(2002,1,22)),
                new Rating(2,2,4,new DateTime(2001,12,1))

            };
            

            /*
            using (StreamReader r = File.OpenText("ratings.json"))
            {
                string json = r.ReadToEnd();
                dataStore = JsonConvert.DeserializeObject<List<Rating>>(json);
            }
            */

        }
    }
}
