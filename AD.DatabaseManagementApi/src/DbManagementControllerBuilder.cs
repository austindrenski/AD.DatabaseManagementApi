using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JetBrains.Annotations;

namespace AD.DatabaseManagementApi
{
    [PublicAPI]
    public class DbManagementControllerBuilder
    {
        private readonly IList<Func<DbManagementServiceContainer>> _messages = new List<Func<DbManagementServiceContainer>>();

        public DbManagementControllerBuilder()
        {
        }

        [NotNull]
        public DbManagementController Build([NotNull] DbContext context)
        {
            return new DbManagementController(_messages.Where(x => x != null).Select(x => x()));
        }

        [NotNull]
        public DbManagementControllerBuilder AddMessage([NotNull] string message, [NotNull] DbManagementServiceContainer serviceContainer)
        {
            Func<DbManagementServiceContainer> action = () =>
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine(message);
                    string response = Console.ReadLine() ?? "";
                    if (response.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        return serviceContainer;
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