using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRating.Core.DomainService
{
   public interface IRepository<T>
    {
        //how th read the json file. 
        /*
            using (StreamReader r = File.OpenText("ratings.json"))
            {
                string json = r.ReadToEnd();
                dataStore = JsonConvert.DeserializeObject<List<Rating>>(json);
            }
            */

        //Returns a list containing all the objects in the repository.
        T[] GetAll();
    }
}
