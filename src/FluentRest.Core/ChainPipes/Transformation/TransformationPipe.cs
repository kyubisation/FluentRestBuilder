// <copyright file="TransformationPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.ChainPipes.Transformation
{
    using System;
    using System.Threading.Tasks;
    using Core.Common;
    using Microsoft.AspNetCore.Mvc;

    public class TransformationPipe<TInput, TOutput> :
        InputPipe<TInput>,
        IInputPipe<TInput>,
        IOutputPipe<TOutput>
        where TInput : class
        where TOutput : class
    {
        private readonly Func<TInput, TOutput> transformation;
        private IInputPipe<TOutput> child;

        public TransformationPipe(
            Func<TInput, TOutput> transformation,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.transformation = transformation;
        }

        Task<IActionResult> IInputPipe<TInput>.Execute(TInput input)
        {
            NoPipeAttachedException.Check(this.child);
            var transformedEntity = this.transformation(input);
            return this.child.Execute(transformedEntity);
        }

        TPipe IOutputPipe<TOutput>.Attach<TPipe>(TPipe pipe)
        {
            this.child = pipe;
            return pipe;
        }
    }
}
