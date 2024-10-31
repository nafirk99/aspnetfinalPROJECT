using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Builder
{
    internal class ConnectionString
    {
        public StringBuilder ConnectionStringItem { get; set; }

        public ConnectionString() { ConnectionStringItem = new StringBuilder(); }
    }
}
