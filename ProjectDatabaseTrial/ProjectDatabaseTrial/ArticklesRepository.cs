using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace ProjectDatabaseTrial
{// implementation of the CRUD and repository for database tables 
   public  class ArticklesRepository:IArticklesRepository
    {
        private string ConnectionString { get;  }
        private SqlConnection connection { get;  }
        public List<Artickles> ArtickleList { get; set; }
        public ArticklesRepository()
        {
            ConnectionString = "Data Source=SQL6009.site4now.net;Initial Catalog=DB_A53DDD_DesbeleMeles;User Id=DB_A53DDD_DesbeleMeles_admin;Password=Desu574138;";
            connection = new SqlConnection(ConnectionString);
            /* my plan here is to implement the CRUD by connecting 
             * the IArticle repository 
             * with the SQL project and will make it possible to see any update, delete or
             * adding
             */
            ArtickleList = new List<Artickles>() 
            {
                new Artickles(){ArticklesId= 1,Name= "Vesvio",BasePrice= 99,Type= "Pizza"},
                new Artickles(){ArticklesId= 2,Name= "Margarita",BasePrice= 85,Type= "Pizza"},
                new Artickles(){ArticklesId= 3,Name= "Hawaii",BasePrice= 85,Type= "Pizza"},
                new Artickles(){ArticklesId= 4,Name= "Calzone",BasePrice= 85,Type= "Pizza"}
            };
        }
        public IEnumerable<Artickles> artickles()
        {
            return connection.Query<Artickles>("spGetAllArtickles", commandType: CommandType.StoredProcedure);
        }

        public Artickles Add(Artickles artickles)
        {
            artickles.ArticklesId = ArtickleList.Max(a => a.ArticklesId) + 1;
            ArtickleList.Add(artickles);
            return artickles;
        }

        public IEnumerable<Artickles> GetAllArtickles()
        {
            return connection.Query<Artickles>("spGetAllArtickles", commandType: CommandType.StoredProcedure);
        }

        public Artickles Update(Artickles changedArtickle)
        {
            Artickles artickle = ArtickleList.FirstOrDefault(a => a.ArticklesId == changedArtickle.ArticklesId);
            if (artickle != null)
            {
                artickle.Name = changedArtickle.Name;
                artickle.BasePrice = changedArtickle.BasePrice;
                artickle.Type = changedArtickle.Type;
            }
            return artickle;
        }

        public Artickles Delete(int articklesId)
        {
            Artickles artickle = ArtickleList.FirstOrDefault(a => a.ArticklesId == articklesId);
           if(artickle != null)
           {
                ArtickleList.Remove(artickle);
           }
            return artickle;
        }
    }
}
