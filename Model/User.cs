using System.ComponentModel;

namespace ProjectPlanningSystem.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UsersRole Role { get; set; }

        public List<Job> Tasks { get; set; }
    }

    public enum UsersRole
    {
        [Description("Admin User")]
        Admin,
        [Description("General User")]
        General
    }
}
