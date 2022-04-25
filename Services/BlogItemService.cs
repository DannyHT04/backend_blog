using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_blog.Models;
using backend_blog.Services.Context;

namespace backend_blog.Services
{
    public class BlogItemService
    {
         private readonly DataContext _context;
        public BlogItemService(DataContext context)
        {
            _context = context;
        }
        
           public bool AddBlogItem(BlogitemModel newBlogItem)
        {
           _context.Add(newBlogItem);
          return _context.SaveChanges() != 0;
        }

         public IEnumerable<BlogitemModel> GetAllBlogItems()
        {
            return _context.BlogInfo;
        }
        public IEnumerable<BlogitemModel> GetItemsByUserID(int UserID)
        {
            return _context.BlogInfo.Where(item => item.Userid == UserID);
        }

         public IEnumerable<BlogitemModel> GetItemsByCategory(string Category)
        {
            return _context.BlogInfo.Where(item => item.Category == Category);
        }

           public List<BlogitemModel> GetItemsByTag(string Tag)
        {
            //"Tag1, Tag2, Tag3,Tag4"
            List<BlogitemModel> AllBlogsWithTag = new List<BlogitemModel>();//[]
            var allItems = GetAllBlogItems().ToList();//{Tag:"Tag1, Tag2",Tag:"Tag2",Tag:"tag3"}
            for(int i=0; i <= allItems.Count; i++)
            {
                BlogitemModel Item = allItems[i];//{Tag:"Tag1, Tag2"}
                var itemArr = Item.Tags.Split(",");//["Tag1","Tag2"]
                for(int j = 0; j < itemArr.Length; j++)
                {   //Tag1 j = 0
                    //Tag2 j = 1
                    if(itemArr[i].Contains(Tag))
                    {// Tag1               Tag1
                        AllBlogsWithTag.Add(Item);//{Tag:"Tag1, Tag2"}
                    }
                }
            }
            return AllBlogsWithTag;
        }

        public IEnumerable<BlogitemModel> GetItemsByDate(string Date)
        {
            return _context.BlogInfo.Where(item => item.Date == Date);
        }


          public IEnumerable<BlogitemModel> GetPublishedItems()
        {
            return _context.BlogInfo.Where(item => item.IsPublished);
        }

           public bool UpdateBlogItem (BlogitemModel BlogUpdate)
        {
            _context.Update<BlogitemModel>(BlogUpdate);
            return _context.SaveChanges() !=0;
        }

         public bool DeleteBlogItem(BlogitemModel BlogDelete)
        {
            BlogDelete.isDeleted = true;
            _context.Update<BlogitemModel>(BlogDelete);
            return _context.SaveChanges() != 0;
        }
           public BlogitemModel GetBlogItemById(int Id)
        {
            return _context.BlogInfo.SingleOrDefault(item => item.Id == Id);
        }









        // public BlogitemModel getItemByID(int ID)
        // {
        //     return _context.BlogInfo.Where(user => user.Userid == ID);
        // }

    }
}