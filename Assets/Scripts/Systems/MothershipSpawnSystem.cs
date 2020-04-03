using Assets.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    public class MothershipSpawnSystem : ComponentSystem
    {
        private Random random;

        protected override void OnCreate()
        {
            this.random = new Random((uint)UnityEngine.Random.Range(int.MinValue, int.MaxValue));
        }

        protected override void OnUpdate()
        {
            Random rng = this.random;
            float dt = this.Time.DeltaTime;

            this.Entities.ForEach((ref MothershipSpawnData spawnData) =>
            {
                if (spawnData.Delay == 0f)
                {
                    spawnData.Delay = rng.NextFloat(spawnData.MinDelay, spawnData.MaxDelay);
                }

                spawnData.Accumulator += dt;

                if (spawnData.Accumulator >= spawnData.Delay)
                {
                    spawnData.Accumulator -= spawnData.Delay;
                    spawnData.Delay = 0f;

                    //spawn a mothership

                    Entity ship = this.PostUpdateCommands.Instantiate(spawnData.Prefrab);
                    Translation position = new Translation {Value = spawnData.SpawnPos};

                    if (rng.NextBool())
                    {
                        position.Value.x = -position.Value.x;
                        SpeedData speed = this.EntityManager.GetComponentData<SpeedData>(spawnData.Prefrab);
                        speed.Value.x = -speed.Value.x;
                        this.PostUpdateCommands.SetComponent(ship, speed);
                    }

                    this.PostUpdateCommands.SetComponent(ship, position);

                }
            });
            this.random = rng;
        }
    }
}