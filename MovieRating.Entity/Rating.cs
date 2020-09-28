using System;
using System.Security.Principal;

namespace MovieRating.Entity
{
    //{Reviewer:563, Movie:781196, Grade:2, Date:'2003-06-06'}
    public class Rating
    {
        public int ReviewerID { get; set; }

        public int MovieID { get; set; }

        public int Grade { get; set; }

        public DateTime Date { get; set; }
    }
}
