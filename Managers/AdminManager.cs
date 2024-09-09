using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;
using ProjectPlanningSystem.Model;
using System.ComponentModel;
using ProjectPlanningSystem.Controllers;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProjectPlanningSystem.Managers
{
    internal class AdminManager
    {
        private PPSContext _context;
        private UserController _userController;
        private JobController _jobController;
        private ProjectController _projectController;

        public AdminManager() 
        { 
            _context = new PPSContext();
            _userController = new UserController(_context);
            _jobController = new JobController(_context);
            _projectController = new ProjectController(_context);
        }

        public void CreateMenu()
        {
            int selectedOption = MenuManager.CreateOptionMenu<AdminMenuChoices>();

            ManageAdminMenu(selectedOption);
        }

        private void ManageAdminMenu(int selectedOption)
        {
            switch(selectedOption)
            {
                case 1:
                    AddNewUser();
                    break;
                case 2:
                    ChooseUser();
                    break;
                case 3:
                    AddNewTaskToUser(null);
                    break;
                case 4:
                    Console.WriteLine("Are you sure you want to exit the application? (Type 'Y' to exit 'N' to stay)");
                    var userInput = Console.ReadKey();

                    if (userInput.Key == ConsoleKey.Y)
                    {
                        Console.Write("\n");
                        Console.WriteLine("Have a nice day!");
                    } else
                    {
                        Console.Clear();
                        CreateMenu();
                    }
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please type in correct number");
                    CreateMenu();
                    break;
            }
        }

        private void ChooseUser()
        {
            List<User> users = _userController.GetElements();
            MenuManager.CreateObjectListMenu(users);

            Console.Write("Enter your selection: ");
            int selectedOption = -1;

            try
            {
                selectedOption = int.Parse(Console.ReadLine());
                ChangeUser(users[selectedOption - 1]);
            }
            catch (NullReferenceException)
            {
                ErrorManager.ThrowEmptySelectionError();
                CreateMenu();
            }
            catch (FormatException)
            {
                ErrorManager.ThrowWrongFormatError();
                CreateMenu();
            }            
        }

        private void AddNewUser()
        {
            Console.WriteLine("Type Login for new user:");
            string userName = Console.ReadLine();

            if (!ErrorManager.CheckEmptyString(userName, "Login"))
                CreateMenu();

            Console.WriteLine("Type Password for new user: ");
            string userPassword = MenuManager.TypePassword();

            if (!ErrorManager.CheckEmptyString(userPassword, "Password"))
                CreateMenu();

            Console.WriteLine("Choose role for new user: ");

            int userRole = MenuManager.CreateOptionMenu<UsersRole>();

            CreateNewUser(userName, userPassword,  userRole);

            CreateMenu();
        }

        private void ChangeUser(User user)
        {
            if (user != null)
            {
                var userJobs = _jobController.GetElements(user.UserId);

                if (userJobs.Count != 0)
                {
                    Console.WriteLine($"List of {user.Login} tasks:");

                    MenuManager.CreateObjectListMenu(userJobs);
                    ReadUserChoices(user);
                }
                else
                {
                    Console.WriteLine($"{user.Login} don't have any tasks");
                    ReadUserChoices(user);
                }
            }
        }

        private void AddNewTaskToUser(User? user)
        {
            List<Project> projects = _projectController.GetElements();

            Console.WriteLine("Choose Project to add new Task: ");
            MenuManager.CreateObjectListMenu(projects);

            Console.Write("Enter your selection: ");
            int selectedOption = -1;

            try
            {
                selectedOption = int.Parse(Console.ReadLine());

                if (selectedOption == projects.Count + 1)
                {
                    AddNewProject(user);
                }
                else
                {
                    Project project = projects[selectedOption - 1];
                    AddNewTaskToProject(user, project);
                }
            }
            catch (NullReferenceException)
            {
                ErrorManager.ThrowEmptySelectionError();
                CreateMenu();
            }
            catch (FormatException)
            {
                ErrorManager.ThrowWrongFormatError();
                CreateMenu();
            }
        }

        private void AddNewProject(User? user)
        {
            Console.WriteLine("Type Name for a new Project:");
            string newProjectName = Console.ReadLine();

            if (!ErrorManager.CheckEmptyString(newProjectName, "Project Name"))
                CreateMenu();

            Console.WriteLine("Type Description for a new Project:");
            string newProjectDescription = Console.ReadLine();

            if (!ErrorManager.CheckEmptyString(newProjectDescription, "Project Description"))
                CreateMenu();

            Project newProject = new Project()
            {
                Name = newProjectName,
                Description = newProjectDescription
            };

            _projectController.AddElement(newProject);

            Console.WriteLine("New Task added successfully");
            AddNewTaskToUser(user);
        }

        private void AddNewTaskToProject(User? user,Project project)
        {         
            Console.WriteLine("Type Name for a new task: ");
            var newJobName = Console.ReadLine();

            if (!ErrorManager.CheckEmptyString(newJobName, "Task Name"))
                CreateMenu();

            Console.WriteLine("Type Description for a new task: ");
            var newJobDescription = Console.ReadLine();

            if (!ErrorManager.CheckEmptyString(newJobDescription, "Task Description"))
                CreateMenu();

            int userId = 0;

            if (user != null)
                userId = user.UserId;

            Job newJob = new Job()
            {
                Name = newJobName,
                Description = newJobDescription,
                Status = JobStatus.ToDo,
                ProjectId = project.ProjectId,
                UserId = userId == 0 ? null : userId
            };

            _jobController.AddElement(newJob);

            Console.WriteLine("New Task added successfully");
            CreateMenu();
        }

        private void CreateNewUser(string login, string password, int role)
        {
            if (_userController.GetUserByLogin(login) == null)
            {

                User newUser = new User()
                {
                    Login = login,
                    Password = password,
                    Role = (UsersRole)role - 1
                };

                _userController.AddElement(newUser);
            }
            else
            {
                Console.WriteLine($"User with login {login} is already exist.");
                CreateMenu();
            }
        }

        private void ReadUserChoices(User user)
        {
            int selectedOption = MenuManager.CreateOptionMenu<ChangeUserChoices>();

            switch (selectedOption)
            {
                case 1: // Add Task
                    AddNewTaskToUser(user);
                    break;
                case 2: // Back
                    CreateMenu();
                    break;
            }
        }
    }

    public enum AdminMenuChoices
    {
        [Description("Add New User")]
        AddNewUser,
        [Description("Change Existing User")]
        ChangeExistingUser,
        [Description("Create Job")]
        CreateJob,
        [Description("Exit")]
        Exit
    }

    public enum ChangeUserChoices
    {
        [Description("Add Task")]
        AddTask,
        [Description("Back")]
        Back
    }
}
