using ProjectPlanningSystem.Managers;
using ProjectPlanningSystem.Model;

namespace ProjectPlanningSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            ValidationManager validationManager = new ValidationManager();

            validationManager.GetUser();
        }
    }
}