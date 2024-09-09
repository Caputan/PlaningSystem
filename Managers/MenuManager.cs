using ConsoleTables;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectPlanningSystem.Controllers;
using ProjectPlanningSystem.Model;
using System.ComponentModel;
using System.Xml.Linq;

namespace ProjectPlanningSystem.Managers
{
    public static class MenuManager
    {
        public static int CreateOptionMenu<T>() where T : Enum
        {
            if (!typeof(T).IsEnum)
            {
                Console.WriteLine("Internal system error. Press any key to exit");
                Console.ReadKey();
            }

            Console.WriteLine("Please choose an action:\n");
            var menuItemNumber = 1;

            foreach (T choice in Enum.GetValues(typeof(T)))
            {
                var description = GetEnumDescription(choice); 
                Console.WriteLine($"{menuItemNumber}:    {description}");
                menuItemNumber++;
            }

            Console.Write("\nEnter your selection: ");

            int selectedOption = -1;

            try
            {
                selectedOption = int.Parse(Console.ReadLine());
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Selection can't be empty");
            }
            catch (FormatException)
            {
                Console.WriteLine("Selection must be a number");
            }

            return selectedOption;
        }

        public static void CreateObjectListMenu(List<Job> elements)
        {
            PPSContext context = new PPSContext();
            ProjectController projectController = new ProjectController(context);

            var menuItemNumber = 1;

            ConsoleTable table = new ConsoleTable("No", "Project Name", "Task Name", "Task Description", "Status");
            foreach (Job element in elements)
            {
                Project taskProject = projectController.GetElement(element.ProjectId);

                table.AddRow($"[{menuItemNumber}]", taskProject.Name, element.Name, element.Description, (JobStatus)element.Status);
                menuItemNumber++;
            }

            table.Write();
            Console.WriteLine();
        }

        public static void CreateObjectListMenu(List<Project> elements)
        {
            PPSContext context = new PPSContext();
            ProjectController projectController = new ProjectController(context);

            ErrorManager.CheckForNull(elements[0]);

            var menuItemNumber = 1;

            ConsoleTable table = new ConsoleTable("No", "Project Name", "Project Description");
            foreach (Project element in elements)
            {
                table.AddRow($"[{menuItemNumber}]", element.Name, element.Description);
                menuItemNumber++;
            }
            table.Write();
            Console.WriteLine();

            Console.WriteLine($"[{menuItemNumber}]: Add New Project");
        }

        public static void CreateObjectListMenu(List<User> elements)
        {
            PPSContext context = new PPSContext();
            UserController userController = new UserController(context);

            ErrorManager.CheckForNull(elements[0]);

            var menuItemNumber = 1;

            ConsoleTable table = new ConsoleTable("No", "User Name");
            foreach (User element in elements)
            {
                table.AddRow($"{menuItemNumber}", element.Login);
                menuItemNumber++;
            }

            table.Write();
            Console.WriteLine();
        }


        public static string TypePassword()
        {
            string text = "";
            ConsoleKeyInfo keyInfo;

            while (true)
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Write("\n");
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (text.Length == 0)
                        continue;
                    text = text.Remove(text.Length - 1);
                    Console.Write("\b \b");
                }
                else
                {
                    text += keyInfo.KeyChar;
                    Console.Write("*");
                }
            }

            return text;
        }

        private static string GetEnumDescription(Enum value)
        {
            var field = value.GetType()
                     .GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(
                field, typeof(DescriptionAttribute)
            );
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
