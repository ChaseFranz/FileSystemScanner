namespace RepositoryScanner
{
    public static class IRepositoryReaderFactory
    {

        public static IRepositoryReader GetIRepositoryReader()
        {
            return new NecRepositoryReader();
        }
    }
}
