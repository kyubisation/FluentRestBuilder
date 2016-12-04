// <copyright file="EntityValidationPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Test.ChainPipes.EntityValidation
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Core.ChainPipes.EntityValidation;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using Xunit;

    public class EntityValidationPipeTest : TestBaseWithServiceProvider
    {
        [Fact]
        public async Task TestInvalidValidation()
        {
            var result = await this.TestValidation(e => e.Id == 1);
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Debug.Assert(objectResult != null, "objectResult != null");
            Assert.Equal(401, objectResult.StatusCode.GetValueOrDefault());
        }

        [Fact]
        public async Task TestValidValidation()
        {
            var result = await this.TestValidation(e => e.Id != 1);
            Assert.IsType<MockActionResult>(result);
        }

        private async Task<IActionResult> TestValidation(Func<Entity, bool> invalidCheck)
        {
            var entity = new Entity { Id = 1 };
            var resultPipe = MockSourcePipe<Entity>.CreateCompleteChain(
                entity,
                this.ServiceProvider,
                source => new EntityValidationPipe<Entity>(
                    invalidCheck, 401, "error", source));
            return await resultPipe.Execute();
        }
    }
}
