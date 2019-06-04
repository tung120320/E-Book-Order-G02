using System;


namespace Persistence.MODEL
{
    public class Item
    {
        public Item() {}

      

        public int? ItemId {get;set;}
        public string ItemName {get;set;}
        public double ItemPrice{get;set;}
        public string ItemAuthor{get;set;}
        public string ItemCategory{get;set;}
        public string ItemDescription{get;set;}
        public int ItemISBN{get;set;}
        public DateTime ItemPublished{get;set;}
        public string  ItemPublisher{get;set;}
        public string  ItemLanguage{get;set;}
        public int  ItemPages{get;set;}
        
    }
}
