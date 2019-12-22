namespace GitHubAPIDemo.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string ObjectId { get; set; }

        public BlogSection BlogSection { get; set; }
    }


    public class ArticleDto
    {
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public string ObjectId { get; set; }

        public string Text { get; set; }

        public string BlogSectionName { get; set; }
    }
}
