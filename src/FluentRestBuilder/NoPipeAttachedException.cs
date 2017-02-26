// <copyright file="NoPipeAttachedException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder
{
    using System;

    public class NoPipeAttachedException : InvalidOperationException
    {
        public NoPipeAttachedException()
            : base("Must have a pipe attached!")
        {
        }
    }
}
