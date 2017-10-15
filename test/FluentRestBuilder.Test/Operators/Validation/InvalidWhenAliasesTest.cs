// <copyright file="InvalidWhenAliasesTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Operators.Exceptions;
    using Mocks.EntityFramework;
    using Xunit;

    public class InvalidWhenAliasesTest
    {
        private const string Error = "error";

        public static IEnumerable<object[]> ValidationFunctionsWithErrors() =>
            new List<Func<IProviderObservable<Entity>, IProviderObservable<Entity>>>
                {
                    o => o.BadRequestWhen(e => true, e => Error),
                    o => o.BadRequestWhen(e => true, Error),
                    o => o.BadRequestWhen(() => true, e => Error),
                    o => o.BadRequestWhen(() => true, Error),
                    o => o.BadRequestWhenAsync(e => Task.FromResult(true), e => Error),
                    o => o.BadRequestWhenAsync(e => Task.FromResult(true), Error),
                    o => o.BadRequestWhenAsync(() => Task.FromResult(true), e => Error),
                    o => o.BadRequestWhenAsync(() => Task.FromResult(true), Error),
                    o => o.ForbiddenWhen(e => true, e => Error),
                    o => o.ForbiddenWhen(e => true, Error),
                    o => o.ForbiddenWhen(() => true, e => Error),
                    o => o.ForbiddenWhen(() => true, Error),
                    o => o.ForbiddenWhenAsync(e => Task.FromResult(true), e => Error),
                    o => o.ForbiddenWhenAsync(e => Task.FromResult(true), Error),
                    o => o.ForbiddenWhenAsync(() => Task.FromResult(true), e => Error),
                    o => o.ForbiddenWhenAsync(() => Task.FromResult(true), Error),
                    o => o.GoneWhen(e => true, e => Error),
                    o => o.GoneWhen(e => true, Error),
                    o => o.GoneWhen(() => true, e => Error),
                    o => o.GoneWhen(() => true, Error),
                    o => o.GoneWhenAsync(e => Task.FromResult(true), e => Error),
                    o => o.GoneWhenAsync(e => Task.FromResult(true), Error),
                    o => o.GoneWhenAsync(() => Task.FromResult(true), e => Error),
                    o => o.GoneWhenAsync(() => Task.FromResult(true), Error),
                    o => o.NotFoundWhen(e => true, e => Error),
                    o => o.NotFoundWhen(e => true, Error),
                    o => o.NotFoundWhen(() => true, e => Error),
                    o => o.NotFoundWhen(() => true, Error),
                    o => o.NotFoundWhenAsync(e => Task.FromResult(true), e => Error),
                    o => o.NotFoundWhenAsync(e => Task.FromResult(true), Error),
                    o => o.NotFoundWhenAsync(() => Task.FromResult(true), e => Error),
                    o => o.NotFoundWhenAsync(() => Task.FromResult(true), Error),
                }
                .Select(f => new object[] { f });

        public static IEnumerable<object[]> ValidationFunctionsWithoutErrors() =>
            new List<Func<IProviderObservable<Entity>, IProviderObservable<Entity>>>
                {
                    o => o.BadRequestWhen(e => true),
                    o => o.BadRequestWhen(() => true),
                    o => o.BadRequestWhenAsync(e => Task.FromResult(true)),
                    o => o.BadRequestWhenAsync(() => Task.FromResult(true)),
                    o => o.ForbiddenWhen(e => true),
                    o => o.ForbiddenWhen(() => true),
                    o => o.ForbiddenWhenAsync(e => Task.FromResult(true)),
                    o => o.ForbiddenWhenAsync(() => Task.FromResult(true)),
                    o => o.GoneWhen(e => true),
                    o => o.GoneWhen(() => true),
                    o => o.GoneWhenAsync(e => Task.FromResult(true)),
                    o => o.GoneWhenAsync(() => Task.FromResult(true)),
                    o => o.NotFoundWhen(e => true),
                    o => o.NotFoundWhen(() => true),
                    o => o.NotFoundWhenAsync(e => Task.FromResult(true)),
                    o => o.NotFoundWhenAsync(() => Task.FromResult(true)),
                }
                .Select(f => new object[] { f });

        [Theory]
        [MemberData(nameof(ValidationFunctionsWithErrors))]
        public async Task TestValidationWithErrors(
            Func<IProviderObservable<Entity>, IProviderObservable<Entity>> function)
        {
            var entity = new Entity { Id = 1, Name = "name" };
            var exception = await Assert.ThrowsAsync<ValidationException>(
                async () => await function(Observable.Single(entity)));
            Assert.NotNull(exception.Error);
        }

        [Theory]
        [MemberData(nameof(ValidationFunctionsWithoutErrors))]
        public async Task TestValidationWithoutErrors(
            Func<IProviderObservable<Entity>, IProviderObservable<Entity>> function)
        {
            var entity = new Entity { Id = 1, Name = "name" };
            var exception = await Assert.ThrowsAsync<ValidationException>(
                async () => await function(Observable.Single(entity)));
            Assert.Null(exception.Error);
        }
    }
}
