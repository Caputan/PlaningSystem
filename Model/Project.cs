namespace ProjectPlanningSystem.Model
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Job> Tasks { get; set; }
    }
}
