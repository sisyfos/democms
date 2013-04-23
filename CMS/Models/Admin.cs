using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class Admin
    {
        public List<Category> category { get; set; }
        public List<Text> text { get; set; }
        public List<Picture> picture { get; set; }
        public List<Video> video { get; set; }
        public List<Link> link { get; set; }
    }
}