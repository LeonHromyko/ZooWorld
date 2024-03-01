namespace GameSettings
{
    public interface IGameSettings
    {
        int AnimalSpawnDelayMinMilliSec { get; }
        int AnimalSpawnDelayMaxMilliSec { get; }
        IAnimalSettings[] AnimalSettings { get; }
        ISpawnSettings SpawnSettings { get; }
        int UpdateStateDelayMilliSec { get; }
    }
}