// <copyright file="CreatedEntityResultPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Results.CreatedEntityResult
{
    using System;
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class CreatedEntityResultPipe<TInput> : ResultPipe<TInput>
        where TInput : class
    {
        private readonly string routeName;
        private readonly Func<TInput, object> routeValuesGenerator;

        public CreatedEntityResultPipe(
            Func<IPipe, object> routeValuesGenerator,
            string routeName,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.routeValuesGenerator = t => routeValuesGenerator(this);
            this.routeName = routeName;
        }

        public CreatedEntityResultPipe(
            Func<TInput, object> routeValuesGenerator,
            string routeName,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.routeValuesGenerator = routeValuesGenerator;
            this.routeName = routeName;
        }

        protected override IActionResult CreateResult(TInput source) =>
            new CreatedAtRouteResult(this.routeName, this.routeValuesGenerator(source), source);
    }
}
