using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Runtime.CompilerServices;


namespace UrlBuilder
{
    /// <summary>
    /// used to generate urls 
    /// </summary>
    public class UrlBuilder
    {
        private string Host { get; } = null;

        public Dictionary<string, string> QueryParameters { get; } =  new Dictionary<string, string>(); 
        private Uri Path { get; }

        /// <summary>
        /// Takes the host and path as separate urls 
        /// </summary>
        /// <param name="host">http://www.examnple.com</param>
        /// <param name="path">/foo/bar/ba/</param>
        public UrlBuilder(string host, string path = "")
        {
            Host = new Uri(host).Host;
            var uri = new Uri(Host + path);
            BuildParamsFromUrl(uri.Query);
            Path = new Uri(uri.OriginalString.Split('?')[0]);

        }

        public UrlBuilder(string url)
        {
            var uri = new Uri(url); 
            Host = uri.Host;
            BuildParamsFromUrl(uri.Query);
            Path = new Uri(uri.OriginalString.Split('?')[0]);

        }

        /// <summary>
        /// Url as a string
        /// </summary>
        /// <returns>url as a string</returns>
        public string GetUrl()
        {
            return Path + BuildQueryString();
        }

        /// <summary>
        /// Adds a string query parameter 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>UrlBuilder</returns>
        public UrlBuilder AddQueryParameter(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
            {
                return this; 
            }
            
            QueryParameters.Add(key,value);

            return this;
        }

        /// <summary>
        /// Adds a integer query parameter 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>UrlBuilder</returns>
        public UrlBuilder AddQueryParameter(string key, int? value)
        {
            if (string.IsNullOrWhiteSpace(key) || ! value.HasValue)
            {
                return this; 
            }
            
            QueryParameters.Add(key,value.Value.ToString());
            return this;
        }
       /// <summary>
       /// Adds a boolean query parameter 
       /// </summary>
       /// <param name="key"></param>
       /// <param name="value"></param>
       /// <returns>UrlBuilder</returns>
        public UrlBuilder AddQueryParameter(string key, bool? value)
        {
            if (string.IsNullOrWhiteSpace(key) || ! value.HasValue)
            {
                return this; 
            }
            QueryParameters.Add(key,value.Value.ToString());
            return this;
        }

        /// <summary>
        /// Adds a UnixTimestamp query parameter 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public UrlBuilder AddQueryParameter(string key, DateTime? value)
        {
            if (string.IsNullOrWhiteSpace(key) || !value.HasValue)
            {
                return this; 
            }

            QueryParameters.Add(key, GetTimeStamp(value.Value).ToString());
            return this; 
        }


        private int GetTimeStamp(DateTime value)
        {
            return (int)Math.Truncate(value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }
        
        private string BuildQueryString()
        {
            if (QueryParameters.Count <= 0)
                return string.Empty;
                
            return "?" + string.Join("&", QueryParameters.Select(q => $"{  HttpUtility.UrlEncode(q.Key)}={HttpUtility.UrlEncode(q.Value)}"));
        }
        
        private  void BuildParamsFromUrl(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return;
            }
            
            var q  = query.ToString();
            q = q.Replace("?", string.Empty);
            foreach (var item in q.Split('&'))
            {
                var value = item.Split('=');
                QueryParameters.Add(value[0],value[1]);
            }

        }

    }
}
