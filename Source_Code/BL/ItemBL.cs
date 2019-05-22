using System;
using DAL;
using Persistence.MODEL;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace BL
{
    public class ItemBl
    {
        private ItemDAL itemDAL = new ItemDAL();
        public List<Item> GetListsItems()
        {
            return itemDAL.GetListsItems();
        }
        public List<Item> GetListsItems(int numberPage)
        {
            return itemDAL.GetListsItems(numberPage);
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
