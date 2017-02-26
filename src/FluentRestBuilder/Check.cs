// <copyright file="Check.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder
{
    using System;

    public static class Check
    {
        public static void IsPipeAttached(object value)
        {
            if (value == null)
            {
                throw new NoPipeAttachedException();
            }
        }
    }
}
