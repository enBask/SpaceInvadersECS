using Assets.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    public class MothershipSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            GameWorldData gameData = this.GetSingleton<GameWorldData>();
            float dt = this.Time.DeltaTime;

            this.Entities
                .WithAll<MotherShipTag>()
                .ForEach((Entity entity, ref SpeedData speed, ref Translation position) =>
                {
                    float3 newPos = position.Value + speed.Value * dt;
                    if (newPos.x >= gameData.Bounds.y || newPos.x <= gameData.Bounds.x)
                    {
                        this.PostUpdateCommands.DestroyEntity(entity);
                        return;
                    }
                    position.Value = newPos;
                });
        }
    }
}