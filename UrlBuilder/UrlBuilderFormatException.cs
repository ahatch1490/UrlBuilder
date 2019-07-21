using System;

namespace UrlBuilder
{
    public class UrlBuilderFormatException : Exception
    {
        public UrlBuilderFormatException(string url)
            : base($"Could not build URI from given url: '{url}'")
        {
        }
    }
}