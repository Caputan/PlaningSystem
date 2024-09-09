using ProjectPlanningSystem.Model;

namespace ProjectPlanningSystem.Controllers
{
    public abstract class Controller<T>
    {
        public abstract void AddElement(T Element);
        public abstract void DeleteElement(T Element);
        public abstract T GetElement(int? id);
    }
}
