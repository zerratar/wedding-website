using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeddingWebsite.BusinessLogic.Instagram
{
    public class ScrapedInstagramApi : IInstagramApi
    {
        private const string tagsUrlFormat = "https://www.instagram.com/explore/tags/{0}/";
        public async Task<IInstagramResult> GetByTagAsync(string tagName)
        {
            var url = string.Format(tagsUrlFormat, tagName);
            var req = (HttpWebRequest)HttpWebRequest.CreateHttp(url);
            req.Method = WebRequestMethods.Http.Get;
            try
            {
                using (var response = await req.GetResponseAsync())
                using (var stream = response.GetResponseStream())
                using (var memoryStream = new MemoryStream())
                {
                    var buffer = new byte[4096];
                    var read = 0;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        memoryStream.Write(buffer, 0, read);
                    }
                    var result = UTF8Encoding.UTF8.GetString(memoryStream.GetBuffer());
                    return this.ParseResult(result);
                }
            }
            catch (Exception exc)
            {
                return new FailedResult(exc.Message);
            }
        }

        private IInstagramResult ParseResult(string result)
        {
            var pattern = @"<script type=""text\/javascript"">window._sharedData = (.*)<\/script>";
            var options = RegexOptions.Multiline;
            var matches = Regex.Matches(result, pattern, options);
            if (matches.Count == 0 || matches[0].Groups.Count == 0)
            {
                return new FailedResult("No results");
            }

            var match = matches[0].Groups[1].Value;
            if (match.EndsWith(";")) match = match.Remove(match.Length - 1);
            //var resultData = JObject.Parse(match);

            var resultData = JsonConvert.DeserializeObject<ResultData>(match);

            return new SuccessefulResult(resultData);
        }
    }
}