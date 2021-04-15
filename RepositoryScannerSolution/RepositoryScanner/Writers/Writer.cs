using System;
using System.Data;
using System.Data.SqlClient;

namespace RepositoryScanner.Writers
{
    public class Writer : IWriter
    {
        private IDataReader Reader { get; set; }

        public Writer(IDataReader reader)
        {
            Reader = reader;
        }

        public void Execute()
        {
            string connectionString = GetConnectionString();

            using (SqlConnection sourceConnection =
                       new SqlConnection(connectionString))
            {
                sourceConnection.Open();

                using (var bulkCopy = new SqlBulkCopy(connectionString))
                {
                    bulkCopy.DestinationTableName = "dbo.[file_stage]";

                    var table = new DataTable();
                    table.Columns.Add("file");
                    bulkCopy.ColumnMappings.Add("file", "file");

                    try
                    {
                        while(Reader.Read())
                        {
                            var values = new object[1];
                            values[0] = Reader.GetValue(0);
                            table.Rows.Add(values);
                        }

                        bulkCopy.WriteToServer(table);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    finally
                    {
                        Reader.Close();
                    }
                }
            }
        }

        private static string GetConnectionString()
        // To avoid storing the sourceConnection string in your code,
        // you can retrieve it from a configuration file.
        {
            return @"Server=localhost\SQLEXPRESS;Database=NEC;Trusted_Connection=True;";
        }
    }
}
