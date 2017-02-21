using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace DataAccess.Storage
{
    class DocumentDBSession
    {
        private Uri uri;
        private string authorizationKey;
        private DocumentClient dc;

        public DocumentDBSession(string uri, string authorizationKey)
        {
            this.uri = new Uri(uri);
            this.authorizationKey = authorizationKey;
            dc = new DocumentClient(this.uri, this.authorizationKey);
        }

        public IEnumerable<dynamic> QueryItem(string databaseid, string collectionid, string sql, FeedOptions fo)
        {
            IQueryable<dynamic> query = dc.CreateDocumentQuery<dynamic>(UriFactory.CreateDocumentCollectionUri(databaseid, collectionid), sql, fo);
            return query.AsEnumerable<dynamic>();
        }
    }
}
