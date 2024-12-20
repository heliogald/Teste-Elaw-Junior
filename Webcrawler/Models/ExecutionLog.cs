namespace WebCrawler.Models
{
    public class ExecutionLog
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalPages { get; set; }
        public int TotalRows { get; set; }
        public string JsonFilePath { get; set; }
    }
}
