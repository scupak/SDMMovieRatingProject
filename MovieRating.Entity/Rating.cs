using System;
using System.Security.Principal;

namespace MovieRating.Entity
{
    //{Reviewer:563, Movie:781196, Grade:2, Date:'2003-06-06'}
    public class Rating
    {
        public int Reviewer { get; set; }

        public int Movie { get; set; }

        public int Grade { get; set; }

        public DateTime Date { get; set; }

        public Rating(int reviewerId, int movieId, int grade, DateTime date)
        {
            Reviewer = reviewerId;
            Movie = movieId;
            Grade = grade;
            Date = date;
        }

        public Rating()
        {
        }
    }
}
