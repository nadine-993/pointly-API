namespace Pointly.Models
{
    public class Charger
    {
        public int Id {get; set;}
        public int UserId {get; set;}
        public User User {get; set;} = null!;
        public decimal AvailablePoints {get; set;}
        public int? AssignedByAdminId {get; set;}

        public Admin? AssignedByAdmin {get; set;}
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<PointAssignment> PointAssignments { get; set; } = new List<PointAssignment>();



    }
}