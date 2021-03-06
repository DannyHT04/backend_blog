using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_blog.Models
{
    public class BlogitemModel
    {
        public int Id {get; set;}
        public int Userid {get; set;}
        public string? PublisherName {get; set;}
        public string? Title {get; set;}
        public string? Image {get; set;}
        public string? Description {get; set;}
        public string? Date {get; set;}
        public string? Category {get; set;}
        public string? Tags {get; set;}
        public bool isDeleted {get; set;}
        public bool IsPublished {get; set;}
        public BlogitemModel(){}
    }

}