using Assets.Scripts.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    [UpdateAfter(typeof(AlienMovementSystem))]
    public class AlienImprovementSystem : ComponentSystem
    {
        private EntityQuery improveAlienQuery;

        protected override void OnCreate()
        {
            this.improveAlienQuery = this.GetEntityQuery(ComponentType.ReadOnly<AlienImproveRequest>());
            this.RequireForUpdate(this.improveAlienQuery);
        }

        protected override void OnUpdate()
        {
            NativeArray<Entity> entities = this.improveAlienQuery.ToEntityArray(Allocator.TempJob);
            Entity requestEntity = entities[0];

            AlienImproveRequest request = this.EntityManager.GetComponentData<AlienImproveRequest>(requestEntity);
            this.EntityManager.DestroyEntity(entities);
            entities.Dispose();

            AlienSpawnData spawnData = this.GetSingleton<AlienSpawnData>();

            this.Entities
                .WithAll<AlienTag>()
                .ForEach((ref SpeedData speed, ref Translation position) =>
                {
                    speed.Value = request.NewSpeed;
                    position.Value.x -= request.Drift;
                    position.Value.y -= spawnData.AlienHeight;
                });

        }
    }
}