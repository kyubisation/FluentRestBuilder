// <copyright file="NoPipeAttachedException.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core
{
    using System;

    public class NoPipeAttachedException : InvalidOperationException
    {
        public NoPipeAttachedException()
            : base("Must have a pipe attached!")
        {
        }

        public static void Check(object check)
        {
            if (check == null)
            {
                throw new NoPipeAttachedException();
            }
        }
    }
}
