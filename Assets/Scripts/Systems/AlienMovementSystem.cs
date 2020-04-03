using Assets.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    [UpdateAfter(typeof(GameSetupSystem))]
    public class AlienMovementSystem : ComponentSystem
    {
        private EntityQuery alienQuery;

        protected override void OnCreate()
        {
            this.alienQuery = this.GetEntityQuery(ComponentType.ReadOnly<AlienTag>(),
                                                  ComponentType.ReadWrite<SpeedData>());
        }

        protected override void OnUpdate()
        {

            GameWorldData gameData = this.GetSingleton<GameWorldData>();
            float dt = this.Time.DeltaTime;

            if (this.alienQuery.CalculateEntityCount() == 1)
            {
                Entity alienEntity = this.alienQuery.GetSingletonEntity();
                SpeedData speed = this.alienQuery.GetSingleton<SpeedData>();
                speed.Value.x = gameData.AlienMaxSpeed * speed.Value.x;
                if (speed.Value.x > gameData.AlienMaxSpeed)
                    speed.Value.x = gameData.AlienMaxSpeed;
                if (speed.Value.x < -gameData.AlienMaxSpeed)
                    speed.Value.x = -gameData.AlienMaxSpeed;
                this.EntityManager.SetComponentData(alienEntity, speed);
            }



            this.Entities
                .WithAll<AlienTag>()
                .ForEach((ref SpeedData speed, ref Translation position) =>
                {
                    position.Value += speed.Value * dt;

                    if (position.Value.x <= gameData.Bounds.x || position.Value.x >= gameData.Bounds.y)
                    {
                        float3 newSpeed = -speed.Value;
                        newSpeed *= 1.2f;

                        if (newSpeed.x > gameData.AlienMaxSpeed)
                            newSpeed.x = gameData.AlienMaxSpeed;
                        if (newSpeed.x < -gameData.AlienMaxSpeed)
                            newSpeed.x = -gameData.AlienMaxSpeed;


                        float diff = 0;
                        if (newSpeed.x < 0)
                        {
                            diff = position.Value.x - gameData.Bounds.y;
                        }
                        else
                        {
                            diff = position.Value.x - gameData.Bounds.x;
                        }

                        Entity request = this.PostUpdateCommands.CreateEntity();
                        this.PostUpdateCommands.AddComponent(request, new AlienImproveRequest
                        {
                            NewSpeed = newSpeed,
                            Drift = diff
                        });

                    }

                });
        }
    }
}