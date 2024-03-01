using Animals;
using GameSettings;

namespace ObjectPool
{
    public interface IAnimalsObjectPool
    {
        IAnimal Get(IAnimalSettings animalSettings);
        void Release(IAnimal animal);
    }
}