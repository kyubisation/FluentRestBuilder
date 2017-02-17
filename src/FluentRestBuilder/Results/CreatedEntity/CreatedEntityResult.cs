// <copyright file="CreatedEntityResult.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.CreatedEntity
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    public class CreatedEntityResult<TInput> : ResultBase<TInput>
        where TInput : class
    {
        private readonly string routeName;
        private readonly Func<TInput, object> routeValuesFactory;

        public CreatedEntityResult(
            Func<TInput, object> routeValuesFactory,
            string routeName,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.routeValuesFactory = routeValuesFactory;
            this.routeName = routeName;
        }

        protected override IActionResult CreateResult(TInput source) =>
            new CreatedAtRouteResult(
                this.routeName, this.routeValuesFactory.Invoke(source), source);
    }
}
