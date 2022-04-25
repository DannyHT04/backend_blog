using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_blog.Models;
using backend_blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_blog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogItemController : ControllerBase
    {
        private readonly BlogItemService _data;

        public BlogItemController(BlogItemService dataFromService )
        {
            _data = dataFromService;
        }

        [HttpPost("AddBlogItem")]

        public bool AddBlogItem(BlogitemModel newBlogItem)
        {
            return _data.AddBlogItem(newBlogItem);
        }

          [HttpGet("GetBlogItems")]
        public IEnumerable<BlogitemModel> GetAllBlogItems()
        {
            return _data.GetAllBlogItems();
        }

        [HttpGet("GetItemsByUserID/{UserID}")]
        public IEnumerable<BlogitemModel> GetItemsByUserID(int UserID)
        {
            return _data.GetItemsByUserID(UserID);
        }

          [HttpGet("GetItemsByCategory/{Category}")]
        public IEnumerable<BlogitemModel> GetItemsByCategory(string Category)
        {
            return _data.GetItemsByCategory(Category);
        }

         [HttpGet("GetItemsByTag/{Tag}")]
        public List<BlogitemModel> GetItemsByTag(string Tag)
        {
            return _data.GetItemsByTag(Tag);
        }

        [HttpGet("GetItemsByDate/{Date}")]
        public IEnumerable<BlogitemModel> GetItemsByDate(string Date)
        {
            return _data.GetItemsByDate(Date);
        }

         [HttpGet("GetPublishedItems")]
        public IEnumerable<BlogitemModel> GetPublishedItems()
        {
            return _data.GetPublishedItems();
        }

         [HttpPost("UpdateBlogItem")]
        public bool UpdateBlogItem (BlogitemModel BlogUpdate)
        {
            return _data.UpdateBlogItem(BlogUpdate);
        }
    
        
        [HttpPost("DeleteBlogItem")]
         public bool DeleteBlogItem (BlogitemModel BlogDelete)
        {
           return _data.DeleteBlogItem(BlogDelete);
        }
        
         [HttpGet("GetBlogItemById/{Id}")]
        public BlogitemModel GetBlogItemById(int Id)
        {
            return _data.GetBlogItemById(Id);
        }
    }
}