using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubAPIDemo.Models
{

    public class CommitDetails
    {
        [JsonProperty(PropertyName ="sha")]
        public string Sha { get; set; }

        [JsonProperty(PropertyName = "node_id")]
        public string Node_id { get; set; }

        [JsonProperty(PropertyName = "commit")]
        public Commit Commit { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "html_url")]
        public string Html_url { get; set; }

        [JsonProperty(PropertyName = "comments_url")]
        public string Comments_url { get; set; }

        [JsonProperty(PropertyName = "author")]
        public AuthorDetails Author { get; set; }

        [JsonProperty(PropertyName = "committer")]
        public AuthorDetails CommitterDetails { get; set; }

        [JsonProperty(PropertyName = "parents")]
        public Parent[] Parents { get; set; }
    }

    public class Commit
    {
        [JsonProperty(PropertyName = "author")]
        public Author Author { get; set; }

        [JsonProperty(PropertyName = "committer")]
        public Author Committer { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "tree")]
        public Tree Tree { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "comment_count")]
        public int Comment_count { get; set; }

        [JsonProperty(PropertyName = "verification")]
        public Verification Verification { get; set; }
    }

    public class Author
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
    }

    //public class Committer
    //{
    //    [JsonProperty(PropertyName = "name")]
    //    public string Name { get; set; }

    //    [JsonProperty(PropertyName = "email")]
    //    public string email { get; set; }
    //    public DateTime date { get; set; }
    //}

    public class Tree
    {
        [JsonProperty(PropertyName = "sha")]
        public string Sha { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    public class Verification
    {
        [JsonProperty(PropertyName = "verified")]
        public bool Verified { get; set; }

        [JsonProperty(PropertyName = "reason")]
        public string Reason { get; set; }

        [JsonProperty(PropertyName = "signature")]
        public object Signature { get; set; }

        [JsonProperty(PropertyName = "payload")]
        public object Payload { get; set; }
    }

    public class AuthorDetails
    {
        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "node_id")]
        public string Node_id { get; set; }

        [JsonProperty(PropertyName = "avatar_url")]
        public string Avatar_url { get; set; }

        [JsonProperty(PropertyName = "gravatar_id")]
        public string Gravatar_id { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "html_url")]
        public string Html_url { get; set; }

        [JsonProperty(PropertyName = "followers_url")]
        public string Followers_url { get; set; }

        [JsonProperty(PropertyName = "following_url")]
        public string Following_url { get; set; }

        [JsonProperty(PropertyName = "gists_url")]
        public string Gists_url { get; set; }

        [JsonProperty(PropertyName = "starred_url")]
        public string Starred_url { get; set; }

        [JsonProperty(PropertyName = "subscriptions_url")]
        public string Subscriptions_url { get; set; }

        [JsonProperty(PropertyName = "organizations_url")]
        public string Organizations_url { get; set; }

        [JsonProperty(PropertyName = "repos_url")]
        public string Repos_url { get; set; }

        [JsonProperty(PropertyName = "events_url")]
        public string Events_url { get; set; }

        [JsonProperty(PropertyName = "received_events_url")]
        public string Received_events_url { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "site_admin")]
        public bool Site_admin { get; set; }

        [JsonProperty(PropertyName = "ldap_on")]
        public string Ldap_dn { get; set; }
    }

    //public class CommitterDetails
    //{
    //    public string login { get; set; }
    //    public int id { get; set; }
    //    public string node_id { get; set; }
    //    public string avatar_url { get; set; }
    //    public string gravatar_id { get; set; }
    //    public string url { get; set; }
    //    public string html_url { get; set; }
    //    public string followers_url { get; set; }
    //    public string following_url { get; set; }
    //    public string gists_url { get; set; }
    //    public string starred_url { get; set; }
    //    public string subscriptions_url { get; set; }
    //    public string organizations_url { get; set; }
    //    public string repos_url { get; set; }
    //    public string events_url { get; set; }
    //    public string received_events_url { get; set; }
    //    public string type { get; set; }
    //    public bool site_admin { get; set; }
    //    public string ldap_dn { get; set; }
    //}

    public class Parent
    {
        [JsonProperty(PropertyName = "sha")]
        public string Sha { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "html_url")]
        public string Html_url { get; set; }
    }

}
