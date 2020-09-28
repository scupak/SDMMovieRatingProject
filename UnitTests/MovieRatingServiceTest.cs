using System;
using System.Collections.Generic;
using Moq;
using MovieRating.Core.DomainService;
using MovieRating.Entity;
using Xunit;

namespace UnitTests
{
    public class MovieRatingServiceTest
    {
        // Fake store for repository
        private Dictionary<int, Rating> dataStore;
        private Mock<IRepository<Rating>> repoMock;

        public MovieRatingServiceTest(Dictionary<int, Rating> dataStore, Mock<IRepository<Rating>> repoMock)
        {
            this.dataStore = dataStore;
            this.repoMock = repoMock;
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
