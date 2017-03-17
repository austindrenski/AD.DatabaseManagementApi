using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using System.Collections.Immutable;

namespace AD.DatabaseManagementApi
{
    [PublicAPI]
    public class DbManagementControllerBuilder
    {
        private readonly IList<Func<IDbManagementService>> _messages = new List<Func<IDbManagementService>>();

        [NotNull]
        public DbManagementController Build()
        {
            return new DbManagementController(_messages.Select(x => x()).Where(x => x != null).ToImmutableArray());
        }

        [NotNull]
        public DbManagementControllerBuilder AddMessage([NotNull] string message, [NotNull] IDbManagementService service)
        {
            Func<IDbManagementService> action = () =>
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine(message);
                    string response = Console.ReadLine() ?? "";
                    if (response.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        return service;
                    }
                    if (response.Equals("N", StringComparison.OrdinalIgnoreCase))
                    {
                        return null;
                    }
                }
            };
            _messages.Add(action);
            return this;
        }
    }
}