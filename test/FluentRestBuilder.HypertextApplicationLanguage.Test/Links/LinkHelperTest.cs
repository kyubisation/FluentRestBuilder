// <copyright file="LinkHelperTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Links
{
    using System;
    using System.Collections.Generic;
    using FluentRestBuilder.Storage;
    using HypertextApplicationLanguage.Links;
    using Microsoft.AspNetCore.Http;
    using Xunit;

    public class LinkHelperTest
    {
        private readonly ScopedStorage<HttpContext> httpContextStorage = new ScopedStorage<HttpContext>();

        [Fact]
        public void TestModifyingQueryParams()
        {
            const string url = "http://www.test.ch/asdf?para1=asdf";
            const string paramKey = "test2";
            const string paramValue = "qwer";
            var linkHelper = this.AssignUrl(url);
            var result = linkHelper
                .ModifyCurrentUrl(new Dictionary<string, string> { [paramKey] = paramValue });
            Assert.Equal($"{url}&{paramKey}={paramValue}", result);
        }

        [Fact]
        public void TestModifiyingAndDeletingQueryParams()
        {
            const string url = "http://www.test.ch/asdf?para1=asdf&para2=qwr&para3=dfh";
            var linkHelper = this.AssignUrl(url);
            var result = linkHelper
                .ModifyCurrentUrl(new Dictionary<string, string>
                {
                    ["test2"] = "qwer",
                    ["para2"] = null,
                    ["para3"] = "yxcv",
                });
            Assert.Equal("http://www.test.ch/asdf?para1=asdf&test2=qwer&para3=yxcv", result);
        }

        private LinkHelper AssignUrl(string url)
        {
            var urlBuilder = new UriBuilder(url);
            this.httpContextStorage.Value = new DefaultHttpContext
            {
                Request =
                {
                    Host = new HostString(urlBuilder.Host),
                    PathBase = PathString.Empty,
                    Path = urlBuilder.Path,
                    QueryString = new QueryString(urlBuilder.Query),
                    Scheme = urlBuilder.Scheme,
                },
            };
            return new LinkHelper(this.httpContextStorage);
        }
    }
}
