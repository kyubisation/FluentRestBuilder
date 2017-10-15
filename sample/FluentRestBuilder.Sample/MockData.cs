// <copyright file="MockData.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sample
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public static class MockData
    {
        private static readonly Random Random = new Random();

        public static void CreateMockUser(DbContextOptions<ApplicationDbContext> options)
        {
            using (var context = new ApplicationDbContext(options))
            {
                var user1 = context.Add(new User { Id = 1, Name = "Mock User" }).Entity;
                var user2 = context.Add(new User { Id = 2, Name = "Mock User 2" }).Entity;
                var commentId = 1;
                foreach (var postId in Enumerable.Range(1, 100))
                {
                    var post = context.Add(new Post
                    {
                        Id = postId,
                        Author = Random.NextDouble() > 0.5 ? user1 : user2,
                        Title = RandomString(10 + Random.Next(20)),
                        Content = RandomString(1 + Random.Next(500)),
                    }).Entity;
                    foreach (var unused in Enumerable.Range(0, Random.Next(30)))
                    {
                        context.Add(new Comment
                        {
                            Id = commentId++,
                            Author = Random.NextDouble() > 0.5 ? user1 : user2,
                            Post = post,
                            Title = RandomString(10 + Random.Next(20)),
                            Text = RandomString(1 + Random.Next(500)),
                        });
                    }
                }

                context.SaveChanges();
            }
        }

        public static int GetUserId(this ClaimsPrincipal principal)
        {
            var value = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return value != null && int.TryParse(value, out var userId) ? userId : 1;
        }

        public static IApplicationBuilder MockUser(this IApplicationBuilder app) =>
            app.Use(SetUser);

        private static Task SetUser(HttpContext context, Func<Task> done)
        {
            var id = context.Request.Headers.TryGetValue("X-Current-User", out var value)
                     && int.TryParse(value, out var userId)
                ? userId : 1;
            context.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            }));

            return done();
        }

        private static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
