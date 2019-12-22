namespace GitHubAPIDemo.Configuration
{
    public class GitHubConfiguration
    {
        public string Owner { get; set; }
        public string Repository { get; set; }
        public string RestUri { get; set; }
        public string GraphQLUri { get; set; }

        public string Token { get; set; }
    }
}