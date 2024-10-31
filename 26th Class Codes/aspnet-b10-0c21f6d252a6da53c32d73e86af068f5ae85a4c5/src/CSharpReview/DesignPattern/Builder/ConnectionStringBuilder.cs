using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Builder
{
    internal class ConnectionStringBuilder
    {
        private ConnectionString _connectionString;

        public ConnectionStringBuilder(string server)
        {
            _connectionString = new ConnectionString();
            _connectionString.ConnectionStringItem.Append($"Server={server};");
        }

        public void AddUsernameAndPassword(string username, string password)
        {
            _connectionString.ConnectionStringItem.Append($"User={username}, Pass={password};");
        }

        public void AddTrustedConnection()
        {
            _connectionString.ConnectionStringItem.Append("TrustedConnection=True;");
        }

        public void AddTrustedCertificate()
        {

        }

        public ConnectionString GetConnectionString()
        {
            return _connectionString;
        }
    }
}
