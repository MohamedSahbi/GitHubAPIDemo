using GitHubAPIDemo.Configuration;
using GitHubAPIDemo.Extensions;
using GitHubAPIDemo.Models;
using GraphQL.Client;
using GraphQL.Common.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace GitHubAPIDemo.Services
{
    public class GitHubService
    {
        private readonly GitHubConfiguration _gitHubConfiguration;
        private string _commitHash;
        private Dictionary<string, string> _filesNames;

        public GitHubService(GitHubConfiguration gitHubConfiguration)
        {
            _gitHubConfiguration = gitHubConfiguration;
            _filesNames = new Dictionary<string, string>();
        }

        #region public


        /// <summary>
        /// Get the files' content  of the last commit
        /// </summary>
        /// <returns></returns>
        public async Task<GitHubServiceOutput> GetLastCommitFilesContent(string directory, DateTime? startingFrom = null)
        {
            var result = new GitHubServiceOutput();
            bool isSuccessful = false;

            // Get last Commit if exists
            var lastCommit = await GetLastCommitUsingREST(startingFrom);
            if (lastCommit == null)
            {
                result.OutputMessage = "something went wrong when checking the last commit.";
                return result;
            }

            string message;
            if (lastCommit.Sha.IsNotEmptyString())
            {
                _commitHash = lastCommit.Sha;
                // Get Commit Content
                var commitContent = await GetCommitContentUsingREST(_commitHash);

                // Check if last commit have files
                if (commitContent != null)
                {
                    if (commitContent.files.Any())
                    {
                        // Get files content
                        var articles = await GetArticlesBulk(commitContent.files, directory);
                        result.Articles = articles;

                        //DO your work
                        message = "registry was updated with the last atricles.";
                    }
                    else
                    {
                        message = "New commit was found without any new articles.";

                    }
                    isSuccessful = true;

                }
                else
                {
                    message = $"GitHub REST API could not find the commit {_commitHash}.";
                }
            }
            else
            {
                message = "No new commits were found.";
            }

            result.OutputMessage = message;
            result.IsSuccessful = isSuccessful;
            result.CommitHash = _commitHash;

            return result;
        }

        /// <summary>
        /// Get commit files' content for a given commit 
        /// </summary>
        /// <param name="commitHash">The commit SHA</param>
        /// <param name="directory">Folder name in GitHub repository. 
        /// If not defined, it will return all the files from the different folders.</param>
        /// <returns></returns>
        public async Task<GitHubServiceOutput> GetCommitFilesContent(string commitHash, string directory)
        {
            var result = new GitHubServiceOutput();
            _commitHash = commitHash;
            // Get Commit Content
            var commitContent = await GetCommitContentUsingREST(_commitHash);

            string message;
            bool isSuccessful;
            // Check if last commit have files 
            if (commitContent != null)
            {
                //GitHub will accept partial value of the commit Hash.
                //We reassign the correct value to the commit hash to use it in the output object
                _commitHash = commitContent.Sha;


                if (commitContent.files.Any())
                {
                    // Get files content
                    var articles = await GetArticlesBulk(commitContent.files, directory);
                    result.Articles = articles;

                    //DO your work
                    message = "registry was updated with the last articles.";
                }
                else
                {
                    message = "New commit was found without any new files.";
                }

                isSuccessful = true;

            }
            else
            {
                message = $"Commit with commitHash {_commitHash} was not found";
                isSuccessful = false;

            }

            result.OutputMessage = message;
            result.IsSuccessful = isSuccessful;
            result.CommitHash = _commitHash;

            return result;
        }

        #endregion


        #region Private
        private async Task<CommitDetails> GetLastCommitUsingREST(DateTime? startingFrom = null)
        {
            using (var httpClient = new HttpClient())
            {
                var uriBuilder = new UriBuilder($"{_gitHubConfiguration.RestUri}/{_gitHubConfiguration.Owner}/{_gitHubConfiguration.Repository}/commits");

                if (startingFrom != null)
                {
                    string date = startingFrom?.ToString("o", CultureInfo.InvariantCulture);
                    uriBuilder.Query = $"since={date}";
                }

                httpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {_gitHubConfiguration.Token}");

                // This is a mandatory header.Please refer to the official documentation at:
                // https://developer.github.com/v3/#user-agent-required
                // GitHub recommends the use of GitHub username or 
                // the name of the application for the User-Agent header value
                httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHubAPIDemo");
                var httpResponseMessage = await httpClient.GetAsync(uriBuilder.Uri);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var content = await httpResponseMessage.Content.ReadAsStringAsync();
                    CommitDetails[] commits = JsonConvert.DeserializeObject<CommitDetails[]>(content);

                    if (commits.Any()) return commits[0];
                    else return new CommitDetails();
                }
                //This section is optional.
                else
                {
                    if ((int)httpResponseMessage.StatusCode >= 500)
                    {

                        // do something here
                        // example: log warning - 500 error in 3rd party app
                    }
                    else
                    {
                        // do something here
                        // example: log warning - error  in 3rd party app
                    }
                }
            }

            return null;
        }


        private async Task<CommitContent> GetCommitContentUsingREST(string commitHash)
        {
            using var httpClient = new HttpClient();
            var uri = new Uri($"{_gitHubConfiguration.RestUri}/{_gitHubConfiguration.Owner}/{_gitHubConfiguration.Repository}/commits/{commitHash}");

            httpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {_gitHubConfiguration.Token}");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHubAPIDemo");

            var httpResponseMessage = await httpClient.GetAsync(uri);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                CommitContent commit = JsonConvert.DeserializeObject<CommitContent>(content);

                if (commit is null) return new CommitContent();
                else return commit;
            }
            //This section is optional.
            else
            {
                if ((int)httpResponseMessage.StatusCode >= 500)
                {
                    // do something here
                    // example: log warning - 500 error in 3rd party app

                }
                else
                {
                    // do something here
                    // example: log warning - error in 3rd party app

                }

                return null;

            }
        }


        private async Task<List<ArticleDto>> GetArticlesUsingGraphQL(string[] paths)
        {
            var articles = new List<ArticleDto>();

            using (var graphQLClient = new GraphQLClient(_gitHubConfiguration.GraphQLUri))
            {
                graphQLClient.DefaultRequestHeaders.Add("Authorization", $"bearer {_gitHubConfiguration.Token}");
                graphQLClient.DefaultRequestHeaders.Add("User-Agent", "GitHubAPIDemo");

                // Build graphQL query

                var query = BuildGraphQLRequest(paths);

                // Make API Call
                var response = await graphQLClient.PostAsync(query);

                // Generate Article list from JSON
                JObject repository = JObject.FromObject(response.Data);

                foreach (var name in _filesNames)
                {
                    var articleDto = new ArticleDto
                    {
                        Name = name.Key,
                        BlogSectionName = name.Value,
                        ObjectId = (string)repository["repository"][name.Key]["oid"],
                        Text = (string)repository["repository"][name.Key]["text"]

                    };
                    articles.Add(articleDto);
                }

            }
            return articles;
        }


        private GraphQLRequest BuildGraphQLRequest(string[] paths)
        {
            var queryBuilder = new StringBuilder();
            string queryHeader = @"
                        query($repository: String!, $owner: String!) {
                    repository(name: $repository, owner: $owner) {";

            queryBuilder.Append(queryHeader);

            foreach (var path in paths)
            {
                var (blogSectionName, fileName, queryAppendium) = AddFieldAliasToGraphQLQuery(path);
                _filesNames.Add(fileName, blogSectionName);
                queryBuilder.Append(queryAppendium);
            }

            string queryFooter = "\r\n  } \r\n}";

            queryBuilder.Append(queryFooter);

            var graphQLRequest = new GraphQLRequest
            {
                Query = queryBuilder.ToString(),
                Variables = new
                {
                    repository = _gitHubConfiguration.Repository,
                    owner = _gitHubConfiguration.Owner
                }
            };

            return graphQLRequest;
        }


        private async Task<List<ArticleDto>> GetArticlesBulk(GitHubFile[] files, string directory)
        {
            var result = new List<ArticleDto>();
            string[] paths;
            if (directory.IsNotEmptyString())
            {
                paths = files.Select(x => x.blob_url).Where(x => x.Contains($"/{directory}/")).ToArray();
            }
            else
            {
                paths = files.Select(x => x.blob_url).ToArray();
            }

            if (paths.Any())
                result = await GetArticlesUsingGraphQL(paths);

            return result;
        }


        private (string blogSectionName, string fileName, string queryAppendium) AddFieldAliasToGraphQLQuery(string filePath)
        {

            var pathParts = filePath.Split("/");
            int pathPartsNumber = pathParts.Length;
            string blogSectionName = pathParts[pathParts.Length - 2];

            // fileName transormation
            // name must only contain alphanumeric or underscore due to the restriction of the Alias naming in GraphQL
            // hyphens (-) are not allowed, and it is commonly used in GitHub
            string fileName = pathParts.LastOrDefault();
            string fileExtension = Path.GetExtension(fileName);

            fileName = pathParts.LastOrDefault().Replace(fileExtension, "");
            fileName = new string(fileName.Where(c => char.IsLetterOrDigit(c) || c == '_').ToArray());

            var indexOfBlob = Array.IndexOf(pathParts, "blob");
            var newArray = pathParts.Skip(indexOfBlob + 2).ToArray();
            string expression = "master:" + string.Join('/', newArray);

            string queryAppendium = $"\r\n    {fileName} : object(expression: \"{expression}\" ) {{ " +
                "\r\n ... on Blob {" +
                "\r\n  oid" +
                "\r\n  text" +
                "\r\n}" +
                "\r\n} ";

            return (blogSectionName, fileName, queryAppendium);
        }

        #endregion
    }

    public class GitHubServiceOutput
    {
        public bool IsSuccessful { get; set; }
        public string OutputMessage { get; set; }
        public string CommitHash { get; set; }
        public List<ArticleDto> Articles { get; set; }
    }
}
