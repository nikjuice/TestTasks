using GoGreenService.Models;
using Microsoft.Extensions.Configuration;
using Raven.Client.Documents;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using Raven.Embedded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GoGreenService.RavenDBUtils
{
    internal class DocumentStoreHolder
    {
        
        public static IDocumentStore CreateStore(Settings settings)
        {

            EmbeddedServer.Instance.StartServer(new ServerOptions
            {
                ServerUrl = settings.Database.Urls.FirstOrDefault(),
                FrameworkVersion = RuntimeInformation.FrameworkDescription.Split(' ').Reverse().FirstOrDefault()

            });

            EmbeddedServer.Instance.OpenStudioInBrowser();

            var store = new DocumentStore
            {
                Urls = settings.Database.Urls,
                Database = settings.Database.DatabaseName,

            };

            store.Conventions.IdentityPartsSeparator = '-';


            store.Initialize();


            var result = store.Maintenance.Server.Send(new GetDatabaseRecordOperation(store.Database));
            if (result == null)
                store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(store.Database)));

            return store;
        }
    }
}
