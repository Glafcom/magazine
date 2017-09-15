using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AutoMapper;

namespace MagazineApp.Common.Extensions {
    public static class MappingExtensions {
        /// <summary>
        /// Method of ignor NotMapped entity's fields
        /// </summary>
        /// <typeparam name="TSource">Source entity</typeparam>
        /// <typeparam name="TDestination">Destination entity</typeparam>
        /// <param name="expression">Expression</param>
        /// <returns>Expression</returns>
        // from http://stackoverflow.com/a/35376631
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression) {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            foreach (var property in destinationProperties) {
                if (sourceType.GetProperty(property.Name, flags) == null) {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;

            /*foreach (var property in expression.TypeMap.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;*/
        }
    }
}
