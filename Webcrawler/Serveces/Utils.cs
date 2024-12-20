namespace WebCrawler.Services
{
    public static class Utils
    {
        private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(3); // Limitar a 3 threads simultâneas

        public static async Task ExecuteWithLimitedThreads(Func<Task> action)
        {
            await Semaphore.WaitAsync();
            try
            {
                await action();
            }
            finally
            {
                Semaphore.Release();
            }
        }
    }
}
