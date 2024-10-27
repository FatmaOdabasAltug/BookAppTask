namespace BookHistoryApi.Models.Entities
{
    public abstract class Auditable
    {
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}

