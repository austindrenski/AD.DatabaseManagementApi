using System.Collections.Generic;
using System.Data.Entity;
using JetBrains.Annotations;

namespace AD.DatabaseManagementApi
{
    [PublicAPI]
    public abstract class DbManagementService
    {
        protected readonly DbContext Context;

        protected readonly IDictionary<string, string> Parameters;

        protected DbManagementService(DbContext context, IDictionary<string, string> parameters = null)
        {
            Context = context;
            Parameters = parameters;
        }

        public int Execute()
        {
            return Invoke();
        }

        protected abstract int Invoke();
    }
}