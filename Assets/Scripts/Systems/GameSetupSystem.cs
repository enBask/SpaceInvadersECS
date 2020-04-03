using Assets.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    public class GameSetupSystem : ComponentSystem
    {
        protected override void OnCreate()
        {
            this.RequireSingletonForUpdate<GameSetupTag>();
            Entity entity = this.EntityManager.CreateEntity();
            this.EntityManager.AddComponent<GameSetupTag>(entity);
        }

        protected override void OnUpdate()
        {
            Entity gameSetup = this.GetSingletonEntity<GameSetupTag>();
            this.EntityManager.DestroyEntity(gameSetup);

            AlienSpawnData spawnData = this.GetSingleton<AlienSpawnData>();
            GameWorldData gameWorld = this.GetSingleton<GameWorldData>();

            for (int x = 0; x < spawnData.Cols; ++x)
            {
                for (int y = 0; y < spawnData.Rows; ++y)
                {
                    float3 pos = spawnData.SpawnStart +
                                 new float3(x * spawnData.Size.x, y * -spawnData.Size.y, 0f);

                    Entity alien = this.EntityManager.Instantiate(spawnData.AlienPrefab);
                    this.EntityManager.SetComponentData(alien, new Translation{Value = pos});
                }
            }
        }
    }
}