// <copyright file="OperatorMethods.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace OperatorsToRst
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using FluentRestBuilder;

    public class OperatorMethods : IEnumerable<IGrouping<string, MethodInfo>>
    {
        private static readonly List<Type> FluentRestBuilderTypes = new[]
            {
                typeof(DoAsyncOperator),
                typeof(CacheInDistributedCacheAliases),
                typeof(DeleteEntityOperator),
                typeof(MapToRestCollectionOperator),
            }
            .SelectMany(t => t.Assembly.ExportedTypes)
            .ToList();

        private readonly IEnumerable<IGrouping<string, MethodInfo>> operatorMethods;

        public OperatorMethods()
        {
            this.operatorMethods = FluentRestBuilderTypes
                .Where(t => t.IsAbstract && t.IsSealed)
                .SelectMany(t => t.GetMethods(
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                .Where(IsObservableExtensionMethod)
                .GroupBy(m => m.Name, m => m)
                .OrderBy(g => g.Key)
                .ToList();
        }

        IEnumerator<IGrouping<string, MethodInfo>> IEnumerable<IGrouping<string, MethodInfo>>.GetEnumerator() =>
            this.operatorMethods.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.operatorMethods.GetEnumerator();

        private static bool IsObservableExtensionMethod(MethodInfo method)
        {
            if (!method.IsDefined(typeof(ExtensionAttribute), false))
            {
                return false;
            }

            var parameterType = method.GetParameters()
                .FirstOrDefault()?.ParameterType;
            if (parameterType == null || !parameterType.IsGenericType)
            {
                return false;
            }

            return parameterType.GetGenericTypeDefinition() == typeof(IProviderObservable<>);
        }
    }
}
