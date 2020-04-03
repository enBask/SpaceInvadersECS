using Assets.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    [UpdateAfter(typeof(InputGatherSystem))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public class ApplyInputsSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            float dt = this.Time.DeltaTime;

            GameWorldData gameData = this.GetSingleton<GameWorldData>();


            this.Entities
                .WithAll<PlayerTag>()
                .ForEach((Entity          entity, DynamicBuffer<InputData> inputBuffer,
                                   ref Translation position, ref SpeedData speed) =>
            {

                for (int i = 0; i < inputBuffer.Length; ++i)
                {
                    InputData input = inputBuffer[i];

                    if (input.Left)
                    {
                        position.Value -= speed.Value * dt;
                    }

                    if (input.Right)
                    {
                        position.Value += speed.Value * dt;
                    }

                    position.Value.x = math.clamp(position.Value.x, gameData.Bounds.x, gameData.Bounds.y);

                    if (input.Fire)
                    {
                        Entity request = this.PostUpdateCommands.CreateEntity();
                        this.PostUpdateCommands.AddComponent(request, new FireRequest
                        {
                            Requester = entity,
                            Position = position.Value + new float3(0, .5f, 0)
                        });
                    }
                }
                inputBuffer.Clear();

            });
        }
    }
}