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
            itemName = itemName.ToLower();
            Console.WriteLine(itemName);
            List<Item> items = new List<Item>();
            List<Item> newitems = new List<Item>();
            items = itemDAL.SearchItemName();

            foreach (var item in items)
            {
                if (item.ItemName.ToLower().Contains(itemName))
                {
                    newitems.Add(item);
                }
            }
            return newitems;
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
