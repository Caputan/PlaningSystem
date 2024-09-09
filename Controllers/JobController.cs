using ProjectPlanningSystem.Model;

namespace ProjectPlanningSystem.Controllers
{
    public class JobController : Controller<Job>
    {
        private readonly PPSContext _dbContext;

        public JobController(PPSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void AddElement(Job job)
        {
            if (job == null)
            {
                Console.WriteLine("Project is not found");
                return;
            }

            _dbContext.Jobs.Add(job);
            _dbContext.SaveChanges();
        }

        public override Job GetElement(int? id)
        {
            var currentJob = _dbContext.Jobs.Where(u => u.JobId == id).FirstOrDefault();

            return currentJob;
        }

        public List<Job> GetElements(int? userId)
        {
            var userJobs = _dbContext.Jobs.Where(j => j.UserId == userId).ToList();

            return userJobs;
        }

        public void UpdateElement(Job job)
        {
            Job jobToUpdate = GetElement(job.JobId);

            _dbContext.Entry(jobToUpdate).CurrentValues.SetValues(job);
            _dbContext.SaveChanges();
        }

        public override void DeleteElement(Job job)
        {
            if (job == null)
            {
                Console.WriteLine("User is not found");
                return;
            }

            _dbContext.Jobs.Remove(job);
            _dbContext.SaveChanges();
        }
    }
}
