using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectPlanningSystem.Model;

namespace ProjectPlanningSystem.Controllers
{
    public class ProjectController: Controller<Project>
    {
        private readonly PPSContext _dbContext;

        public ProjectController(PPSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void AddElement(Project project)
        {
            if (project == null)
            {
                Console.WriteLine("Project is not found");
                return;
            }

            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
        }

        public override Project GetElement(int? id)
        {
            var currentProject = _dbContext.Projects.Where(u => u.ProjectId == id).FirstOrDefault();

            return currentProject;
        }

        public List<Project> GetElements()
        {
            var projects = _dbContext.Projects.ToList();

            return projects;
        }

        public void UpdateElement(Project project)
        {
            Project projectToUpdate = GetElement(project.ProjectId);

            _dbContext.Entry(projectToUpdate).CurrentValues.SetValues(project);
            _dbContext.SaveChanges();
        }

        public override void DeleteElement(Project project)
        {
            if (project == null)
            {
                Console.WriteLine("Project is not found");
                return;
            }

            _dbContext.Projects.Remove(project);
            _dbContext.SaveChanges();
        }
    }
}
