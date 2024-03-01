using Animals;

namespace GameSettings
{
    public interface IAnimalSettings
    {
        IAnimal Prefab { get; }
        float SpawnProbabilityWeight { get; }
    }
}