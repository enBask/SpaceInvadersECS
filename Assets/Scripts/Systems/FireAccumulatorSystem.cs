using Assets.Scripts.Components;
using Unity.Entities;

namespace Assets.Scripts.Systems
{
    [UpdateAfter(typeof(ApplyInputsSystem))]
    public class FireAccumulatorSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            float dt = this.Time.DeltaTime;

            this.Entities.ForEach((ref AmmoData ammo) =>
            {
                if (ammo.Accumulator < ammo.Firerate)
                {
                    ammo.Accumulator += dt;
                }
            });
        }
    }
}