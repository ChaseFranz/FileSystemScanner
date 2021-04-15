using RepositoryScanner;
using RepositoryScanner.Writers;

namespace DriverProject
{
    public static class Driver
    {
        static void Main(string[] args)
        {
            DriveTest();
        }

      
        private static void DriveTest()
        {
            var repositoryReader = IRepositoryReaderFactory.GetIRepositoryReader();
            var writer = IWriterFactory.GetIWriter(repositoryReader);
            writer.Execute();
        }
    }
}