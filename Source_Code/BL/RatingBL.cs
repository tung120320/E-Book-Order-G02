using System;
using DAL;
using Persistence.MODEL;
using System.Collections.Generic;
namespace BL
{
    public class RatingBL
    {
        RatingDAL ratingDAL = new RatingDAL();
        public bool Rating(Rating rate){
            return ratingDAL.Rating(rate);
        }
        public List<Rating> GetAllRating(int? itemId){
            return ratingDAL.GetAllRating(itemId);
        }
       
    }
}