using Assets.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    public class MissileMoveSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            GameWorldData gameData = this.GetSingleton<GameWorldData>();
            float dt = this.Time.DeltaTime;
            
            this.Entities.ForEach((Entity          entity, ref MissileData missile, ref SpeedData speed,
                                   ref Translation position) =>
            {
                float3 newPos = position.Value + speed.Value * dt;

                if (newPos.y >= gameData.Bounds.w || newPos.y <= gameData.Bounds.z)
                {
                    this.PostUpdateCommands.DestroyEntity(entity);
                    return;
                }

                position.Value = newPos;
            });
        }
    }
}