# GitHubAPIDemo
## Description
This repository contains a sample demo for my blog post <b>Fix a gap in GitHub API v4</b>. 

## Solution Architecture
- Configuration folder: It contains the configuration classes used for the injection of settings from appsettings.json file with the IOptions pattern
- Controllers folder: It contains the API controllers
- Services folder: It contains the main service of this demo; GitHub Service.
- Models folder: It contains the models used by the GitHub Service. Most of them are models to map the Json response of GitHub.
- Extensions folder: It contains stringExtensions that are needed for the GitHub Service.

## Third Party libraries
- GraphQL.Client https://www.nuget.org/packages/GraphQL.Client/
- Newtonsoft JSON https://www.nuget.org/packages/Newtonsoft.Json/

## Compatibility / Versions
This solution was made for .NET Core 2.1 and then tested with .NET Core 3.0. The current solution's configuration is .NET Core 3.1. That is why I am using Newtonsoft.Json and not the built-in [System.Text.Json](https://docs.microsoft.com/en-us/dotnet/api/system.text.json?view=netcore-3.1) Namespace.

## How to use it?
1. Clone the solution
2. Find a GitHub repository that you are going to use with this demo. Add the repos name and the owner's name to the appsettings.json. It is even better to use the users secrets
3. GitHub oAuth Token is needed. You can generate a new personal access token for GitHub from here https://github.com/settings/tokens and add it to the appsettings.json
4. This solution works also with GitHub entreprise. If you are using it with GitHub entreprise, make sure to change the RestUri and GraphQLUri values.

Example of the configuration filled in the appsettings.json

```json
  "GitHubConfiguration": {
    "Owner": "MohamedSahbi",
    "Repository": "GitHubAPIDemo",
    "RestUri": "https://api.github.com/repos",
    "GraphQLUri": "https://api.github.com/graphql",
    "Token": "your personal access token"
  }

```

<br/> <br/>
## License
### MIT License

Copyright (c) 2019 Mohamed Sahbi

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
