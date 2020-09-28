using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRating.Core.DomainService
{
   public interface IRepository<T>
    {
        //Returns a list containing all the objects in the repository.
        List<T> GetAll();
    }
}
