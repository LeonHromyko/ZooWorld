using UnityEngine;

namespace Animals.Fight
{
    public class AnimalsFightStrategy : IAnimalsFightStrategy
    {
        public void Fight(IAnimal animal1, IAnimal animal2)
        {
            if (!animal1.IsAlive || !animal2.IsAlive)
            {
                return;
            }
            
            if (animal1.AnimalType == AnimalType.Prey && animal2.AnimalType == AnimalType.Predator)
            {
                animal2.Eat(animal1);
            }
            else if (animal1.AnimalType == AnimalType.Predator && animal2.AnimalType == AnimalType.Prey)
            {
                animal1.Eat(animal2);
            }
            else if (animal1.AnimalType == AnimalType.Predator && animal2.AnimalType == AnimalType.Predator)
            {
                var randomValue = Random.Range(0f, animal1.EatenAnimalsAmount + animal2.EatenAnimalsAmount + 2);

                if (randomValue > animal1.EatenAnimalsAmount + 1)
                {
                    animal2.Eat(animal1);
                }
                else
                {
                    animal1.Eat(animal2);
                }
            }
        }
    }
}