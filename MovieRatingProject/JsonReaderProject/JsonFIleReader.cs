using System;
using System.Collections.Generic;
using System.IO;
using MovieRating.Core.DomainService;
using MovieRating.Entity;
using Newtonsoft.Json;

namespace JsonReaderProject
{
    public class JsonFIleReader : IRepository<Rating> 
    {
       public string _fileName { get; set; }

       public Rating[] movieRatings { get; private set; }

        public JsonFIleReader(string fileName)
        {
            _fileName = fileName;
            movieRatings = ReadAllMovieRatings(fileName).ToArray();

        }

        public List<Rating> ReadAllMovieRatings(string filename) 
        {
            List<Rating> Ratings = new List<Rating>();
            /*
            using (StreamReader sr = new StreamReader(_fileName))
            using (JsonReader Jreader = new JsonTextReader(sr))
            {

                while (Jreader.Read()) {


                    Rating r = GetOneRating(Jreader);
                    Ratings.Add(r);
                
                }
            
            }
            */
            using (StreamReader r = File.OpenText(filename))
            {
                string json = r.ReadToEnd();
                Ratings = JsonConvert.DeserializeObject<List<Rating>>(json);
            }

            return Ratings;
        
        }

        public Rating[] GetAll()
        {
            return movieRatings;
        }

        /*
       private Rating GetOneRating(JsonReader sr)
       {
           Rating rating = new Rating();



       }
*/
    }
}
