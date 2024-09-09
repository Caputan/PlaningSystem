using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPlanningSystem.Managers
{
    public static class ErrorManager
    {
        public static bool CheckEmptyString(string stringToCheck, string fieldName)
        {
            if (string.IsNullOrEmpty(stringToCheck))
            {
                Console.WriteLine($"{fieldName} can't be empty.");
                return false;
            }

            return true;
        }

        public static bool CheckForNull<T>(T objectToCheck)
        {
            if (objectToCheck == null)
            {
                Console.WriteLine($"{objectToCheck} is empty.");
                return false;
            }

            return true;
        }

        public static void ThrowEmptySelectionError()
        {
            Console.Clear();
            Console.WriteLine("Selection can't be empty");
        }

        public static void ThrowWrongFormatError()
        {
            Console.Clear();
            Console.WriteLine("Selection must be a number");
        }
    }
}
