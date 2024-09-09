using ProjectPlanningSystem.Controllers;
using ProjectPlanningSystem.Model;

namespace ProjectPlanningSystem.Managers
{
    internal class ValidationManager
    {
        private UserController _userController;
        private PPSContext _context;

        private string _userLogin;
        private string _userPassword;

        public ValidationManager()
        {
            _context = new PPSContext();
            _userController = new UserController(_context);
        }

        public void GetUser()
        {
            User user = ValidateUser(GetUserLogin(), GetUserPassword());

            if (user != null)
            {
                Console.WriteLine("Login successful!");

                switch (user.Role)
                {
                    case UsersRole.Admin:
                        AdminManager adminManager = new AdminManager();
                        adminManager.CreateMenu();
                        break;
                    case UsersRole.General:
                        GeneralUserManager generalUserManager = new GeneralUserManager(user);
                        generalUserManager.CreateGeneralUserMenu();
                        break;

                    default:
                        Console.WriteLine("User don't have a role. You can't continue work with the system");
                        Console.WriteLine("Press any key to exit program");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine("User not found. Please try again");
                GetUser();
            }
        }

        private string GetUserLogin()
        {
            Console.WriteLine("Type in your login: ");
            string? userLogin = Console.ReadLine();

            if (!ErrorManager.CheckEmptyString(userLogin, "Login"))
                return GetUserLogin();
            

            return userLogin;
        }

        private string GetUserPassword()
        {
            Console.WriteLine("Type in your password: ");
            string userPassword = MenuManager.TypePassword();

            if (!ErrorManager.CheckEmptyString(userPassword, "Password"))
                return GetUserLogin();

            return userPassword;
        }

        public User ValidateUser(string login, string password)
        {
            User currentUser = _userController.GetUserByLogin(login);

            if (currentUser != null)
            {
                if (currentUser.Password == password)
                    return currentUser;
            }

            return null;
        }
    }
}
