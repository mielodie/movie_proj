namespace movie_project.Entities
{
    public class BillFood : BaseEntity
    {
        public int Quantity {  get; set; }
        public int BillID {  get; set; }
        public int FoodID {  get; set; }
        public Bill Bill { get; set; }
        public Food Food { get; set; }
    }
}
