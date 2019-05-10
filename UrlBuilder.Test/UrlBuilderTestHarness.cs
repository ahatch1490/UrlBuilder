using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using Xunit;

namespace UrlBuilder.Test
{
    public class UrlBuilderTestHarness
    {
        [Fact]
        public void ShouldBeAbleToCreateABuilderWithAUrl()
        {
            const string expected = "http://www.example.com/foo/bar";
            var builder = new UrlBuilder(expected);
            Assert.Equal(expected, builder.GetUrl());
        }
        
        [Fact]
        public void ShouldBeAbleToIncludeParams()
        {
            const string expected = "http://www.example.com/foo/bar?test=value1&test2=value2";
            var builder = new UrlBuilder(expected);
            Assert.Equal(expected, builder.GetUrl());
        }
        
        [Fact]
        public void ShouldBeAbleToAddBasicParams()
        {
            var host = "http://www.example.com/foo/bar";
            const string expected = "http://www.example.com/foo/bar?test=value1&test2=value2";
            var builder = new UrlBuilder(host);
            builder.AddQueryParameter("test", "value1");
            builder.AddQueryParameter("test2", "value2");
            Assert.Equal(expected, builder.GetUrl());
        }

        [Fact]
        public void ShouldBeParseAddParamsIfTheyExist()
        {
            const string expected = "http://www.example.com/foo/bar?test=value1&test2=value2";
            var builder = new UrlBuilder(expected);
            Assert.True(2 == builder.QueryParameters.Count);
            Assert.Equal(expected, builder.GetUrl());
        }

        [Fact]
        public void ShouldBeaBleToAddAUnixTimeStamp()
        {
            const string expected = "http://www.example.com/foo/bar?time=1556524800";
            const string host = "http://www.example.com/foo/bar";
            var date = new DateTime(2019, 4, 29, 1,0,0);
            var builder = new UrlBuilder(host);
            builder.AddQueryParameter("time",date);
            
            Assert.Equal(expected,builder.GetUrl());
        }

        [Fact]
        public void ShouldBeAbleToCreateBoolean()
        {
            const string expected = "http://www.example.com/foo/bar?bool=True";
            const string host = "http://www.example.com/foo/bar";
            var builder = new UrlBuilder(host);
            builder.AddQueryParameter("bool",true);
            
            Assert.Equal(expected,builder.GetUrl());
        }
        
        [Fact]
        public void ShouldBeAbleToCreateInt()
        {
            const string expected = "http://www.example.com/foo/bar?integer=2";
            const string host = "http://www.example.com/foo/bar";
            var builder = new UrlBuilder(host);
            builder.AddQueryParameter("integer",2);
            
            Assert.Equal(expected,builder.GetUrl());
        }
        
    }
}