using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Forum
{
    /// <summary>
    /// Theme
    /// </summary>
    public class Theme : BaseEntity
    {
        /// <summary>
        /// Name of the theme
        /// </summary>
        public string ThemeName { get; set; }

        /// <summary>
        /// Threads with this theme
        /// </summary>
        public ICollection<ForumThread> ForumThreads { get; set; }
    }
}
