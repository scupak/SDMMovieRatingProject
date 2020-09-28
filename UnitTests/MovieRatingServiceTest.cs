using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using MovieRating.Core.DomainService;
using MovieRating.Entity;
using Newtonsoft.Json;
using Xunit;

namespace UnitTests
{
    public class MovieRatingServiceTest
    {
        // Fake store for repository
        private List<Rating> dataStore;
        private Mock<IRepository<Rating>> repoMock;

        public MovieRatingServiceTest()
        {
            dataStore = new List<Rating>();

            repoMock = new Mock<IRepository<Rating>>();
            repoMock.SetupAllProperties();

            repoMock.Setup(x => x.GetAll()).Returns(dataStore);
        }

        //1. On input N, what are the number of reviews from reviewer N?

        [Fact]
        public void GetNumberOfReviewsFromReviewerTest()
        {
            GenericSetup();
            Assert.True(dataStore.Count > 0);

        }

        private void GenericSetup()
        {
            dataStore.Clear();
            /*
            dataStore = new List<Rating>
            {
                new Rating(1,1,2,new DateTime(2003,6,6)),
                new Rating(2,2,3,new DateTime(2002,1,22)),
                new Rating(2,2,4,new DateTime(2001,12,1)),
                new Rating(1, 1488844, 3, new DateTime(2005,9,6))



            };
            */

            using (StreamReader r = File.OpenText("ratings.json"))
            {
                string json = r.ReadToEnd();
                dataStore = JsonConvert.DeserializeObject<List<Rating>>(json);
            }


        }
    }
}
