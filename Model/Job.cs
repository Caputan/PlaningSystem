namespace ProjectPlanningSystem.Model
{
    public class Job
    {
        public int JobId { get; set; }

        public int? UserId { get; set; }
        public virtual User? User { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public JobStatus Status { get; set; }
    }

    public enum JobStatus
    {
        ToDo,
        InProgress,
        Done,
        Removed
    }
}
