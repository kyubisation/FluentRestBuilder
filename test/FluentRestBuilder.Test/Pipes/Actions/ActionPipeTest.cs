﻿// <copyright file="ActionPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.Actions
{
    using System;
    using System.Threading.Tasks;
    using Common.Mocks;
    using FluentRestBuilder.Pipes.Actions;
    using Xunit;

    public class ActionPipeTest : TestBaseWithServiceProvider
    {
        private const string NewName = "ActionPipeTest";
        private readonly Entity entity;

        public ActionPipeTest()
        {
            this.entity = new Entity { Id = 1, Name = "test" };
        }

        [Fact]
        public async Task TestAsyncAction()
        {
            await this.TestActionPipe(
                source => new ActionPipe<Entity>(
                    async e => await Task.Run(() => e.Name = NewName), source));
        }

        private async Task TestActionPipe(
            Func<IOutputPipe<Entity>, IOutputPipe<Entity>> actionPipeCreator)
        {
            var name = this.entity.Name;
            Assert.NotEqual(name, NewName);
            var resultPipe = MockSourcePipe<Entity>.CreateCompleteChain(
                this.entity,
                this.ServiceProvider,
                actionPipeCreator);
            await resultPipe.Execute();
            Assert.Equal(NewName, resultPipe.Input.Name);
        }
    }
}