namespace Cosmetic_App.Common.Entity
{
    public class Customer:BaseEntity
    {
        public Guid CustomerID { get; set; }
        public string CustomerName { get; set; }
    }
}
