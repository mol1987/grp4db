using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ProjectDatabaseTrial
{// the CRUD parameters that will be implemented on the SQLproject
   public interface IArticklesRepository
    {
        Artickles Add(Artickles artickle);
      IEnumerable <Artickles> GetAllArtickles();
        Artickles Update(Artickles ChangedArtickle);
        Artickles Delete(int ArticklesId);
            
    }
}
