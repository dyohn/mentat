using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;


namespace Mentat.UI.Models
{
    public class MongoHelper
    {
        public static IMongoClient client { get; set; }

        public static IMongoDatabase database { get; set; }
        public static string MongoConnection =  "mongodb+srv://cdb:eN9qAv7forAKSnEt@flashcardapp.4yggo3z.mongodb.net/?retryWrites=true&w=majority";
        public static string MongoDatabase = "fcApp"; //name of Database Instance
   
        public static IMongoCollection<Models.Card> FlashCards { get; set; }
        internal static void ConnectToMongoService() 
        {
            try 
            {
                client = new MongoClient(MongoConnection);
     
                database = client.GetDatabase(MongoDatabase);

            }
            catch (Exception) 
            
            {
                throw;
            }
        
        }
    
    
    }
}
