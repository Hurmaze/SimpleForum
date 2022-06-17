using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Forum
{
    public class Theme : BaseEntity
    {
        public string ThemeName { get; set; }
        public ICollection<ForumThread> ForumThreads { get; set; }
    }
}
