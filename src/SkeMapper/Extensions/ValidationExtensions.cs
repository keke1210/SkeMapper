using System;

namespace SkeMapper.Extensions
{
    internal static class ValidationExtensions
    {
        /// <summary>
        /// Is C# built-in type or struct
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool IsBuiltInType(this Type type)
        {
            return !(type.IsClass && string.IsNullOrEmpty(type.Namespace) ||
                   (!type.Namespace.Equals("System") && !type.Namespace.StartsWith("System.")));
        }
    }
}
