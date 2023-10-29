using PersonalSoulBlog.DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalSoulBlog.BLL.ViewModels.Tags
{
    public class TagView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public User? User { get; set; }
    }
}
