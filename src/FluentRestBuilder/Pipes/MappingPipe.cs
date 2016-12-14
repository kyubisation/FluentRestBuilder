// <copyright file="MappingPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes
{
    using System;
    using System.Threading.Tasks;
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public abstract class MappingPipe<TInput, TOutput> : OutputPipe<TOutput>, IInputPipe<TInput>
    {
        protected MappingPipe(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        Task<IActionResult> IInputPipe<TInput>.Execute(TInput input)
        {
            throw new NotImplementedException();
        }
    }
}
