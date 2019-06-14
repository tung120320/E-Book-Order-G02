using System;
using DAL;
using Persistence.MODEL;
using System.Collections.Generic;
namespace BL
{
    public class RatingBL
    {
        RatingDAL ratingDAL;

        public RatingBL()
        {
            ratingDAL = new RatingDAL();
        }

        public bool RateItem(Rating rating)
        {
            return ratingDAL.RateItem(rating);
        }
        public bool UpdateRateItem(Rating rating)
        {
            return ratingDAL.UpdateRateItem(rating);
        }
        public Rating CheckItemRatedByUserId(int? userId, int? itemId){
            return ratingDAL.CheckItemRatedByUserId(userId, itemId);
        }
        public List<Rating> GetAllRating(int? itemId)
        {
            return ratingDAL.GetAllRating(itemId);
        }

    }
}