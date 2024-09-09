using ProjectPlanningSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace ProjectPlanningSystem.Controllers
{
    public class UserController : Controller<User>
    {
        private readonly PPSContext _dbContext;


        public UserController(PPSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void AddElement(User user)
        {
            if (user == null)
            {
                Console.WriteLine("User can't be null");
                return;
            }

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            Console.Clear();
            Console.WriteLine("User added successfuly");
        }

        public override User GetElement(int? id)
        {
            var currentUser = _dbContext.Users.Where(u => u.UserId == id).FirstOrDefault();

            return currentUser;
        }

        public List<User> GetElements()
        {
            var currentUser = _dbContext.Users.ToList();

            return currentUser;
        }

        public User GetUserByLogin(string login)
        {
            var currentUser = _dbContext.Users.Where(u =>u.Login == login).FirstOrDefault();

            return currentUser;
        }

        public override void DeleteElement(User user)
        {
            if (user == null)
            {
                Console.WriteLine("User is not found");
                return;
            }

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }
    }
}
