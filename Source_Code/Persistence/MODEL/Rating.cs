using System;

namespace Persistence.MODEL
{
    public class Rating
    {
         public Rating(){}

        public Rating(int? itemId, int? userId, int ratingStars, string ratingTitle, string ratingContent)
        {
            ItemId = itemId;
            UserId = userId;
            RatingStars = ratingStars;
            RatingTitle = ratingTitle;
            RatingContent = ratingContent;
           
        }

        public int? ItemId {get;set;}
        public int? UserId {get;set;}
        public int RatingStars {get;set;}
        public string RatingTitle {get;set;}
        public string RatingContent {get;set;}
        public DateTime? RatingDate{get;set;}
        
    }
}
