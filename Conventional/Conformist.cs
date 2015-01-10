using System;
using System.Collections.Generic;
using System.Linq;

namespace Conventional
{
    public static class Conformist
    {
        public static ConventionResult MustConformTo(this Type type, IConventionSpecification conventionSpecification)
        {
            return conventionSpecification.IsSatisfiedBy(type);
        }

        public static IEnumerable<ConventionResult> MustConformTo(this IEnumerable<Type> types, IConventionSpecification conventionSpecification)
        {
            return types.Select(conventionSpecification.IsSatisfiedBy);
        }

        public static void WithFailureAssertion(this IEnumerable<ConventionResult> results, Action<string> failureAssertion)
        {
            if (results.All(x => x.IsSatisfied))
            {
                return;
            }

            var result =                    
                results.First().TypeName +
                    Environment.NewLine +
                    StringConstants.Underline +
                    Environment.NewLine +
                    results.Where(x => x.IsSatisfied == false).SelectMany(x => x.Failures).Aggregate(string.Empty, (s, x) => s + x + Environment.NewLine);

            failureAssertion(result);
        }
    }
}