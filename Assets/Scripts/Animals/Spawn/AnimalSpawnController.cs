using System;
using UnityEngine;
using Zenject;

namespace ZooWorld.Animals.Spawn
{
    public class AnimalSpawnController : IInitializable, IDisposable
    {
        private IAnimalSpawnService _animalSpawnService;

        public AnimalSpawnController(IAnimalSpawnService animalSpawnService)
        {
            _animalSpawnService = animalSpawnService;
        }

        public void Initialize()
        {
            _animalSpawnService.StartSpawn();
        }

        public void Dispose()
        {
            _animalSpawnService.StopSpawn();
        }
    }
}