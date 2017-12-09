// <copyright file="BadRequestWhenModelStateIsInvalidAlias.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using Json;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.DependencyInjection;
    using Operators.Exceptions;

    public static class BadRequestWhenModelStateIsInvalidAlias
    {
        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 400 (Bad Request).
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="modelState">
        /// The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary" /> that
        /// contains the state of the model and of model-binding validation.
        /// </param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> BadRequestWhenModelStateIsInvalid<TSource>(
            this IProviderObservable<TSource> observable, ModelStateDictionary modelState)
        {
            var nameResolver = new Lazy<IJsonPropertyNameResolver>(
                () => observable.ServiceProvider.GetService<IJsonPropertyNameResolver>());
            return observable.BadRequestWhen(
                () => !modelState.IsValid,
                s => modelState.ToDictionary(
                    m => nameResolver.Value.Resolve(m.Key),
                    m => m.Value.Errors));
        }
    }
}
