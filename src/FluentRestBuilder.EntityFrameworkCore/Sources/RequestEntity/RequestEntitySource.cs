// <copyright file="RequestEntitySource.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.RequestEntity
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class RequestEntitySource<TEntity> : IOutputPipe<TEntity>
        where TEntity : class
    {
        private readonly TEntity requestEntity;
        private readonly IServiceProvider serviceProvider;
        private IInputPipe<TEntity> child;

        public RequestEntitySource(
            TEntity requestEntity,
            IServiceProvider serviceProvider)
        {
            this.requestEntity = requestEntity;
            this.serviceProvider = serviceProvider;
        }

        object IServiceProvider.GetService(Type serviceType) =>
            this.serviceProvider.GetService(serviceType);

        TPipe IOutputPipe<TEntity>.Attach<TPipe>(TPipe pipe)
        {
            this.child = pipe;
            return pipe;
        }

        Task<IActionResult> IPipe.Execute()
        {
            NoPipeAttachedException.Check(this.child);
            return this.child.Execute(this.requestEntity);
        }
    }
}
