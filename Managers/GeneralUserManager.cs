using ProjectPlanningSystem.Controllers;
using ProjectPlanningSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPlanningSystem.Managers
{
    internal class GeneralUserManager
    {
        private User _currentUser;
        private PPSContext _context;
        private JobController _jobController;

        public GeneralUserManager(User user) 
        { 
            _currentUser = user;
            _context = new PPSContext(); 
            _jobController = new JobController(_context);
        }

        public void CreateGeneralUserMenu()
        {
            int selectedOption = MenuManager.CreateOptionMenu<GeneralUserMenuChoices>();

            ManageGeneralUserMenu(selectedOption);
        }

        private void ManageGeneralUserMenu(int selectedOption)
        {
            switch (selectedOption)
            {
                case 1:
                    CheckUserTasks();
                    break;
                case 2:
                    Console.WriteLine("Are you sure you want to exit the application? (Type 'Y' to exit 'N' to stay)");
                    var userInput = Console.ReadKey();

                    if (userInput.Key == ConsoleKey.Y)
                    {
                        Console.Write("\n");
                        Console.WriteLine("Have a nice day!");
                    }
                    else
                    {
                        Console.Clear();
                        CreateGeneralUserMenu();
                    }
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please type in correct number");
                    CreateGeneralUserMenu();
                    break;
            }
        }

        private void CheckUserTasks()
        {
            var userJobs = _jobController.GetElements(_currentUser.UserId);

            if (userJobs.Count != 0)
            {
                Console.WriteLine($"List of {_currentUser.Login} tasks:");

                MenuManager.CreateObjectListMenu(userJobs);
                ChangeUserTaskStatus(userJobs);
            }
            else
            {
                Console.WriteLine($"{_currentUser.Login} don't have any tasks");
                ReadUserChoices(_currentUser);
            }
        }

        private void ReadUserChoices(User user)
        {
            int selectedOption = MenuManager.CreateOptionMenu<ChangeUserChoices>();

            switch (selectedOption)
            {
                case 1: // Add Task
                    CheckUserTasks();
                    break;
                case 2: // Back
                    CreateGeneralUserMenu();
                    break;
            }
        }

        private void ChangeUserTaskStatus(List<Job> jobs)
        {
            Console.Write("Choose Task to change: ");
            int selectedOption = -1;

            try
            {
                selectedOption = int.Parse(Console.ReadLine());

                Job job = jobs[selectedOption - 1];
                ChangeJobStatus(job);
                
            }
            catch (NullReferenceException)
            {
                ErrorManager.ThrowEmptySelectionError();
                CreateGeneralUserMenu();
            }
            catch (FormatException)
            {
                ErrorManager.ThrowWrongFormatError();
                CreateGeneralUserMenu();
            }
        }

        private void ChangeJobStatus(Job job)
        {
            int newJobStatus = MenuManager.CreateOptionMenu<JobStatus>();

            Job newJob = new Job()
            {
                Name = job.Name,
                Description = job.Description,
                JobId = job.JobId,
                UserId = job.UserId,
                ProjectId = job.ProjectId,
                Status = (JobStatus)newJobStatus - 1
            };

            _jobController.UpdateElement(newJob);
            Console.WriteLine("Task changed successfuly");
            CreateGeneralUserMenu();
        }
    }

    public enum GeneralUserMenuChoices
    {
        [Description("Check fot your Tasks")]
        CheckTasks,
        [Description("Exit")]
        Exit
    }
}
