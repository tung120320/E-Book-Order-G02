using System;
using DAL;
using Persistence.MODEL;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace BL
{
    public class itemBl
    {
        private ItemDAL itemDAL = new ItemDAL();
        public List<Item> GetListsItems()
        {
            return itemDAL.GetListsItems();
        }
        public Item GetAnItem(int? itemId)
        {
            if (itemId == null)
            {
                return null;
            }
            Regex regex = new Regex("[0-9]");
            MatchCollection matchCollectionId = regex.Matches(itemId.ToString());
            if (matchCollectionId.Count < itemId.ToString().Length)
            {
                return null;
            }
            return itemDAL.GetAnItem(itemId);
        }
    }

}
