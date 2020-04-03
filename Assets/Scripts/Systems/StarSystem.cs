using Assets.Scripts.Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    public class StarSystem : JobComponentSystem
    {
        /*
        protected override void OnUpdate()
        {
            float dt = this.Time.DeltaTime;
            this.Entities
                .ForEach((Entity star, ref Translation position, ref StarData data) =>
              {
                  data.Accumulator += dt;
                  if (data.Accumulator >= data.Lifetime)
                  {
                      this.PostUpdateCommands.DestroyEntity(star);
                      return;
                  }

                  position.Value += new float3(data.Speed.x, data.Speed.y, 0f) * dt;
              });
        }
        */

        private EndSimulationEntityCommandBufferSystem bufferSystem;

        protected override void OnCreate()
        {
            this.bufferSystem = this.World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            float dt = this.Time.DeltaTime;
            EntityCommandBuffer.Concurrent buffer = this.bufferSystem.CreateCommandBuffer().ToConcurrent();


            JobHandle job =  this.Entities
                .ForEach((Entity star, int entityInQueryIndex, ref Translation position, ref StarData data) =>
                {
                    data.Accumulator += dt;
                    if (data.Accumulator >= data.Lifetime)
                    {
                        buffer.DestroyEntity(entityInQueryIndex,star);
                        return;
                    }

                    position.Value += new float3(data.Speed.x, data.Speed.y, 0f) * dt;
                })
                .Schedule(inputDeps);

            this.bufferSystem.AddJobHandleForProducer(job);

            return job;
        }
    }
}