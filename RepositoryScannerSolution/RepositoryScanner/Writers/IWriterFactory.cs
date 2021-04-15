using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryScanner.Writers
{
    public static class IWriterFactory
    {
        public static IWriter GetIWriter(IDataReader reader)
        {
            return new Writer(reader);
        }
    }
}
