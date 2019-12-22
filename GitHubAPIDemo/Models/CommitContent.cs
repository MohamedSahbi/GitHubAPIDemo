using GitHubAPIDemo.Models;

namespace GitHubAPIDemo.Models
{
    public class CommitContent : CommitDetails
    {
        public Stats stats { get; set; }
        public GitHubFile[] files { get; set; }
    }

    public class GitHubFile
    {
        public string sha { get; set; }
        public string filename { get; set; }
        public string status { get; set; }
        public int additions { get; set; }
        public int deletions { get; set; }
        public int changes { get; set; }
        public string blob_url { get; set; }
        public string raw_url { get; set; }
        public string contents_url { get; set; }
        public string patch { get; set; }
        public string content { get; set; }
    }

    public class Stats
    {
        public int total { get; set; }
        public int additions { get; set; }
        public int deletions { get; set; }
    }

}
