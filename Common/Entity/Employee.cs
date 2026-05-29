namespace Cosmetic_App.Common.Entity
{
    public class Employee : BaseEntity
    {
        public Guid EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
    }
}
