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
        [InlineData(3, 1)]
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
        [InlineData(2, 3.5)]
        [InlineData(6, 3)]
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
                new Rating(6, 5, 5, new DateTime(2005,5,23)),
                new Rating(6, 5, 2, new DateTime(2005,5,23)),
                new Rating(2,2,3,new DateTime(2002,1,22)),
                new Rating(2,3,4,new DateTime(2001,12,1))

            };

            //act
            double averageRateFromReviewer = mrs.GetAverageRateFromReviewer(reviewer);

            _testOutputHelper.WriteLine(averageRateFromReviewer.ToString());

            //assert
            Assert.Equal(expected,averageRateFromReviewer);

            repoMock.Verify(rep => rep.GetAll(),Times.AtLeastOnce);


        }

        // 2. On input N, what is the average rate that reviewer N had given Expect exeption?
        [Fact]
        public void GetAverageRateFromReviewerArgumentException()
        {
            //arrange
            IRepository<Rating> repo = repoMock.Object;
            MovieRatingService mrs = new MovieRatingService(repo);

            dataStore = new List<Rating>
            {
                

            };

            // act
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                double AverageRateFromReviewer = mrs.GetAverageRateFromReviewer(1);
            });


            //assert
            // assert
            Assert.Equal("List is empty", ex.Message);

            repoMock.Verify(rep => rep.GetAll(), Times.Once);


        }

        // 3. On input N and R, how many times has reviewer N given rate R?
        [Theory]
        [InlineData(1,2,2)]
        [InlineData(2, 2, 0)]
        [InlineData(5, 2, 1)]
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

            repoMock.Verify(rep => rep.GetAll(), Times.Once);


        }

        // 4. On input N, how many have reviewed movie N?
        [Theory]
        [InlineData(1,1)]
        [InlineData(3, 2)]
        [InlineData(5, 4)]
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

            repoMock.Verify(rep => rep.GetAll(), Times.Once);

        }

        // 5. On input N, what is the average rate the movie N had received?
        [Theory]
        [InlineData(1,2.5)]
        [InlineData(3, 3.5)]
        [InlineData(4, 4)]
        [InlineData(5, 2.5)]
        public void GetAverageRateOfMovie(int movie, double expected)
        {
            //arrange
            IRepository<Rating> repo = repoMock.Object;
            MovieRatingService mrs = new MovieRatingService(repo);
            dataStore = new List<Rating>
            {
                new Rating(1, 1, 2, new DateTime(2003, 6, 6)),
                new Rating(10, 1, 3, new DateTime(2003, 6, 6)),
                new Rating(2, 3, 3, new DateTime(2005, 9, 6)),
                new Rating(3, 4, 4, new DateTime(2004, 12, 23)),
                new Rating(4, 6, 2, new DateTime(2005, 1, 23)),
                new Rating(3, 5, 2, new DateTime(2005, 2, 23)),
                new Rating(4, 5, 2, new DateTime(2005, 3, 23)),
                new Rating(5, 5, 3, new DateTime(2005, 4, 23)),
                new Rating(6, 5, 3, new DateTime(2005, 5, 23)),
                new Rating(2, 2, 3, new DateTime(2002, 1, 22)),
                new Rating(2, 3, 4, new DateTime(2001, 12, 1))
            };
            

            //act 
                double AverageRateOfMovie = mrs.GetAverageRateOfMovie(movie);

                //assert
                Assert.Equal(expected, AverageRateOfMovie);

                repoMock.Verify(rep => rep.GetAll(), Times.AtLeastOnce);


        }

        // 5. On input N, what is the average rate the movie N had received? Expect exeption,
        [Fact]
        public void GetAverageRateOfMovieExpectArgumentException()
        {
            //arrange
            IRepository<Rating> repo = repoMock.Object;
            MovieRatingService mrs = new MovieRatingService(repo);
            dataStore = new List<Rating>
            {
                
            };


            
            // act
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                double AverageRateOfMovie = mrs.GetAverageRateOfMovie(1);
            });


            //assert
            // assert
            Assert.Equal("List is empty", ex.Message);

            repoMock.Verify(rep => rep.GetAll(), Times.Once);


        }

        // 6. On input N and R, how many times had movie N received rate R?
        [Theory]
        [InlineData(1,2,1)]
        [InlineData(5, 2, 2)]
        [InlineData(6, 2, 2)]
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
                new Rating(2, 3, 4, new DateTime(2001, 12, 1)),
                new Rating(8, 6, 2, new DateTime(2005, 1, 23)),
            };


            //act 
            int NumberOfRates = mrs.GetNumberOfRates(movie,rate);

            Assert.Equal(expected, NumberOfRates);

            repoMock.Verify(rep => rep.GetAll(), Times.Once);
        }

        // 7. What is the id(s) of the movie(s) with the highest number of top rates (5)?
        [Fact]
        public void GetMoviesWithHighestNumberOfTopRates()
        {
            //arrange
            IRepository<Rating> repo = repoMock.Object;
            MovieRatingService mrs = new MovieRatingService(repo);
            dataStore = new List<Rating>
            {
                new Rating(1, 1, 5, new DateTime(2003, 6, 6)),
                new Rating(1, 3, 3, new DateTime(2005, 9, 6)),
                new Rating(1, 4, 4, new DateTime(2004, 12, 23)),
                new Rating(1, 6, 2, new DateTime(2005, 1, 23)),
                new Rating(3, 5, 2, new DateTime(2005, 2, 23)),
                new Rating(4, 5, 2, new DateTime(2005, 3, 23)),
                new Rating(5, 5, 5, new DateTime(2005, 4, 23)),
                new Rating(6, 5, 3, new DateTime(2005, 5, 23)),
                new Rating(2, 2, 3, new DateTime(2002, 1, 22)),
                new Rating(2, 3, 4, new DateTime(2001, 12, 1))
            };

            List<int> MoviesWithHighestNumberOfTopRatesExpected = new List<int>
            {
                1, 
                5
            };

            //act 
            List<int> MoviesWithHighestNumberOfTopRates = mrs.GetMoviesWithHighestNumberOfTopRates();

            Assert.Equal(MoviesWithHighestNumberOfTopRatesExpected, MoviesWithHighestNumberOfTopRates);

            repoMock.Verify(rep => rep.GetAll(), Times.Once);

        }

        // 8. What reviewer(s) had done most reviews?
        [Fact]
        public void GetMostProductiveReviewers()
        {

            //arrange
            IRepository<Rating> repo = repoMock.Object;
            MovieRatingService mrs = new MovieRatingService(repo);
            dataStore = new List<Rating>
            {
                new Rating(1, 1, 5, new DateTime(2003, 6, 6)),
                new Rating(1, 3, 3, new DateTime(2005, 9, 6)),
                new Rating(1, 4, 4, new DateTime(2004, 12, 23)),
                new Rating(1, 6, 2, new DateTime(2005, 1, 23)),
                new Rating(3, 5, 2, new DateTime(2005, 2, 23)),
                new Rating(4, 5, 2, new DateTime(2005, 3, 23)),
                new Rating(5, 5, 5, new DateTime(2005, 4, 23)),
                new Rating(6, 5, 3, new DateTime(2005, 5, 23)),
                new Rating(2, 2, 3, new DateTime(2002, 1, 22)),
                new Rating(2, 3, 4, new DateTime(2001, 12, 1)),
                new Rating(7, 1, 5, new DateTime(2003, 6, 6)),
                new Rating(7, 3, 3, new DateTime(2005, 9, 6)),
                new Rating(7, 4, 4, new DateTime(2004, 12, 23)),
                new Rating(7, 6, 2, new DateTime(2005, 1, 23))
            };

            List<int> MostProductiveReviewersExpected = new List<int>
            {
                1,
                7
            };


            //act 
            List<int> MostProductiveReviewers = mrs.GetMostProductiveReviewers();

            Assert.Equal(MostProductiveReviewersExpected, MostProductiveReviewers);

            repoMock.Verify(rep => rep.GetAll(), Times.Once);
        }

        // 9. On input N, what is top N of movies? The score of a movie is its average rate.
      

        [Theory]
        [InlineData(1, new[] { 1 }) ]
        [InlineData(2, new[] { 1,2 })]
        [InlineData(3, new[] { 1, 2,3 })]
        public void GetTopRatedMovies(int amount , int[] expected )
        {
            //arrange
            IRepository<Rating> repo = repoMock.Object;
            MovieRatingService mrs = new MovieRatingService(repo);
            dataStore = new List<Rating>
            {
                new Rating(1, 1, 5, new DateTime(2003, 6, 6)),
                new Rating(1, 1, 5, new DateTime(2003, 6, 6)),
                new Rating(1, 1, 5, new DateTime(2003, 6, 6)),
                new Rating(1, 2, 4, new DateTime(2003, 6, 6)),
                new Rating(1, 2, 4, new DateTime(2003, 6, 6)),
                new Rating(1, 2, 4, new DateTime(2003, 6, 6)),
                new Rating(1, 3, 4, new DateTime(2003, 6, 6)),
                new Rating(1, 3, 4, new DateTime(2003, 6, 6)),
                new Rating(1, 3, 4, new DateTime(2003, 6, 6)),

            };

            List<int> TopRatedMoviesExpected = new List<int>(expected);


            //act 
            List<int> MostProductiveReviewers = mrs.GetTopRatedMovies(amount);

            //assert
            Assert.Equal(TopRatedMoviesExpected, MostProductiveReviewers);

            repoMock.Verify(rep => rep.GetAll(), Times.Once);
        }

        // 10. On input N, what are the movies that reviewer N has reviewed? The list should be sorted decreasing by rate first, and date secondly.
        [Theory]
        [InlineData(1, new[] { 1, 2, 5, 4 ,3 })]
        [InlineData(2, new[] { 2, 1, 4, 3 })]
        public void GetTopMoviesByReviewer(int reviewer , int[] expected)
        {
            //arrange
            IRepository<Rating> repo = repoMock.Object;
            MovieRatingService mrs = new MovieRatingService(repo);
            dataStore = new List<Rating>
            {
                new Rating(1, 1, 5, new DateTime(2003, 6, 6)),
                new Rating(1, 2, 4, new DateTime(2003, 6, 6)),
                new Rating(1, 3, 3, new DateTime(2003, 6, 6)),
                new Rating(1, 4, 3, new DateTime(2004, 6, 6)),
                new Rating(1, 5, 3, new DateTime(2005, 6, 6)),


                new Rating(2, 1, 5, new DateTime(2003, 6, 6)),
                new Rating(2, 2, 5, new DateTime(2004, 6, 6)),
                new Rating(2, 3, 3, new DateTime(2003, 6, 6)),
                new Rating(2, 4, 4, new DateTime(2003, 6, 6)),
               

            };

            List<int> TopRatedMoviesExpected = new List<int>(expected);


            //act 
            List<int> MostProductiveReviewers = mrs.GetTopMoviesByReviewer(reviewer);

            //assert
            Assert.Equal(TopRatedMoviesExpected, MostProductiveReviewers);

            repoMock.Verify(rep => rep.GetAll(), Times.Once);

        }

        // 11. On input N, who are the reviewers that have reviewed movie N? The list should be sorted decreasing by rate first, and date secondly.
        [Theory]
        [InlineData(1 , new[] { 1, 2, 5, 4, 3 })]
        [InlineData(2, new[] { 5, 4, 3, 2, 1 })]
        public void GetReviewersByMovie(int movie, int[] expected)
        {
            //arrange
            IRepository<Rating> repo = repoMock.Object;
            MovieRatingService mrs = new MovieRatingService(repo);
            dataStore = new List<Rating>
            {
                new Rating(1, 1, 5, new DateTime(2003, 6, 6)),
                new Rating(2, 1, 4, new DateTime(2003, 6, 6)),
                new Rating(3, 1, 3, new DateTime(2003, 6, 6)),
                new Rating(4, 1, 3, new DateTime(2004, 6, 6)),
                new Rating(5, 1, 3, new DateTime(2005, 6, 6)),


                new Rating(1, 2, 1, new DateTime(2003, 6, 6)),
                new Rating(2, 2, 2, new DateTime(2003, 6, 6)),
                new Rating(3, 2, 3, new DateTime(2003, 6, 6)),
                new Rating(4, 2, 4, new DateTime(2004, 6, 6)),
                new Rating(5, 2, 5, new DateTime(2005, 6, 6)),




            };

            List<int> ReviewersByMovieExpected = new List<int>(expected);


            //act 
            List<int> ReviewersByMovie = mrs.GetReviewersByMovie(movie);

            //assert
            Assert.Equal(ReviewersByMovieExpected, ReviewersByMovie);

            repoMock.Verify(rep => rep.GetAll(), Times.Once);
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
