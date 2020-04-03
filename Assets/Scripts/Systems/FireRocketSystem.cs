using Assets.Scripts.Components;
using Unity.Entities;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    [UpdateAfter(typeof(ApplyInputsSystem))]
    [UpdateAfter(typeof(FireAccumulatorSystem))]
    public class FireRocketSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            this.Entities.ForEach((Entity entity, ref FireRequest request) =>
            {
                Entity from = request.Requester;
                bool hasComponent = this.EntityManager.HasComponent<AmmoData>(from);
                if (hasComponent)
                {
                    AmmoData ammo = this.EntityManager.GetComponentData<AmmoData>(from);
                    if (ammo.Accumulator >= ammo.Firerate)
                    {
                        ammo.Accumulator -= ammo.Firerate;

                        Entity rocket = this.PostUpdateCommands.Instantiate(ammo.Prefab);
                        this.PostUpdateCommands.SetComponent(rocket, new Translation{ Value = request.Position});
                        this.EntityManager.SetComponentData(from, ammo);
                    }
                }

                this.PostUpdateCommands.DestroyEntity(entity);
            });
        }
    }
}