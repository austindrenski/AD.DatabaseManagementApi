using System;
using System.Collections.Generic;
using System.Data.Entity;
using JetBrains.Annotations;

namespace DatabaseManagementApi
{
    [PublicAPI]
    public sealed class DbManagementServiceContainer : IDisposable
    {
        private readonly Func<DbContext, IDictionary<string, string>, int> _service;

        public string Name { get; }

        public IDictionary<string, string> Parameters { get; set; }

        public DbManagementServiceContainer(Func<DbContext, IDictionary<string, string>, int> service, string name)
        {
            _service = service;
            Name = name;
            Parameters = new Dictionary<string, string>();
        }

        public int Invoke(DbContext context)
        {
            return _service(context, Parameters);
        }

        void IDisposable.Dispose() { }
    }
}
