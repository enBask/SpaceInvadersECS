using Assets.Scripts.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    public class AlienAttackSystem : ComponentSystem
    {
        private Random random;
        private EntityQuery alienQuery;

        protected override void OnCreate()
        {
            this.random = new Random((uint)UnityEngine.Random.Range(int.MinValue, int.MaxValue));
            this.alienQuery =
                this.GetEntityQuery(ComponentType.ReadOnly<AlienTag>(), ComponentType.ReadOnly<Translation>());
        }

        protected override void OnUpdate()
        {
            AlienSpawnData spawnData = this.GetSingleton<AlienSpawnData>();
            AlienAttackData attackData = this.GetSingleton<AlienAttackData>();
            Entity attackDataEntity = this.GetSingletonEntity<AlienAttackData>();
            float dt = this.Time.DeltaTime;

            if (attackData.Delay == 0f)
            {
                attackData.Delay = this.random.NextFloat(attackData.MinDelay, attackData.MaxDelay);
            }

            attackData.Accumulator += dt;
            if (attackData.Accumulator >= attackData.Delay)
            {
                attackData.Accumulator -= attackData.Delay;
                attackData.Delay = 0f;

                NativeArray<Translation> aliens = this.alienQuery.ToComponentDataArray<Translation>(Allocator.TempJob);

                //group aliens by x line so that we build cols
                NativeMultiHashMap<float, Translation> groupedAliens = new NativeMultiHashMap<float, Translation>(aliens.Length, Allocator.TempJob);
                for(int i=0; i<aliens.Length; ++i)
                {
                    Translation pos = aliens[i];
                    groupedAliens.Add(pos.Value.x, pos);
                }

                //pick a random col
                NativeArray<float> keys = groupedAliens.GetKeyArray(Allocator.TempJob);
                int randomIdx = this.random.NextInt(0, keys.Length);
                float randomKey = keys[randomIdx];


                Translation closestAlien = default;
                closestAlien.Value.y = float.MaxValue;
                groupedAliens.TryGetFirstValue(randomKey, out Translation tPos, out var it);
                do
                {
                    if (tPos.Value.y < closestAlien.Value.y)
                    {
                        closestAlien = tPos;
                    }
                } while (groupedAliens.TryGetNextValue(out tPos, ref it));


                closestAlien.Value += new float3(0, -1f, 0f);

                Entity rocket = this.PostUpdateCommands.Instantiate(spawnData.AlienRocketPrefab);
                this.PostUpdateCommands.SetComponent(rocket, closestAlien);

                keys.Dispose();
                groupedAliens.Dispose();
                aliens.Dispose();
            }

            this.EntityManager.SetComponentData(attackDataEntity, attackData);
        }
    }
}