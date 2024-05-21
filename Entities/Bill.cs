namespace movie_project.Entities
{
    public class Bill : BaseEntity
    {
        public double TotalMoney {  get; set; }
        public string TradingCode {  get; set; }
        public DateTime CreateTime {  get; set; }
        public int CustomerID {  get; set; }
        public string Name {  get; set; }
        public DateTime UpdateTime {  get; set; }
        public int PromotionID {  get; set; }
        public int BillStatusID {  get; set; }
        public bool IsActive {  get; set; }
        public User User { get; set; }
        public Promotion Promotion { get; set; }
        public BillStatus BillStatus { get; set; }
        public IQueryable<BillFood> BillFoods { get; set; }
        public IQueryable<BillTicket> BillTickets { get; set; }
    }
}
