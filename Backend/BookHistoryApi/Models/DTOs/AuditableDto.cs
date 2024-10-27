namespace BookHistoryApi.Models.DTOs
{
    public class AuditableDto
    {
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}