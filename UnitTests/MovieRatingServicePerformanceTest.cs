using JsonReaderProject;
using MovieRating.Core.DomainService;
using MovieRating.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
  public class MovieRatingServicePerformanceTest
    {
        IRepository<Rating> repo;
        public MovieRatingServicePerformanceTest() {

            repo = new JsonFIleReader("ratings.json");


        }
    }
}
