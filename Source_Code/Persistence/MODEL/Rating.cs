using System;

namespace Persistence.MODEL
{
    public class Rating
    {
        public Rating() {}

       

        public int ItemId {get;set;}
        public int UserId {get;set;}
        public int RatingStart {get;set;}
        public string RatingTitle {get;set;}
        public string RatingContent {get;set;}
        public DateTime RatingDate{get;set;}
        
    }
}
