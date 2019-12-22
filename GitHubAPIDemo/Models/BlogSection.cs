using System.Collections.Generic;

namespace GitHubAPIDemo.Models
{
    public class BlogSection
    {
        public int BlogSectionId { get; set; }
        public List<Article> Articles { get; set; }
    }
}
