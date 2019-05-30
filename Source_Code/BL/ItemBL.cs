using System;
using DAL;
using Persistence.MODEL;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace BL
{
    public class ItemBl
    {
        private ItemDAL itemDAL;

        public ItemBl()
        {
            itemDAL = new ItemDAL();
        }

        public List<Item> GetListItems()
        {
          
            return itemDAL.GetListItems();
        }
        public List<Item> SearchItemName(string itemName)
        {
            List<Item> items = new List<Item>();
            List<Item> news = new List<Item>();
            items = itemDAL.SearchItemName();
            int ok = 0;
            foreach (var item in items)
            {
                for (int i = 0; i < item.ItemName.Length; i++)
                {
                    for (int j = 0; j < itemName.Length; j++)
                    {
                        if(item.ItemName[i] == itemName[j]){
                            ok++;
                        }
                    }
                }
                if(ok >= 5){
                    news.Add(item);
                }
            }
            return news;
        }
        // public List<Item> GetListsItems(int numberPage)
        // {
        //     return itemDAL.GetListsItems(numberPage);
        // }
        public Item GetAnItemById(int? itemId)
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
            return itemDAL.GetAnItemById(itemId);
        }
    }

}
