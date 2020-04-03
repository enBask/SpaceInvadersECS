using Assets.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    public class StarGenerationSystem : ComponentSystem
    {
        private Random random;
        private EntityQuery starQuery;

        protected override void OnCreate()
        {
            this.random = new Random((uint)UnityEngine.Random.Range(int.MinValue, int.MaxValue));
            this.starQuery = this.GetEntityQuery(ComponentType.ReadOnly<StarData>());

            this.RequireSingletonForUpdate<StarGeneratordata>();
        }

        protected override void OnUpdate()
        {
            StarGeneratordata generator = this.GetSingleton<StarGeneratordata>();
            int starCount = this.starQuery.CalculateEntityCount();

            int diff = generator.Count - starCount;
            for (int i = 0; i < diff; ++i)
            {
                Entity star = this.EntityManager.Instantiate(generator.Prefab);
                this.EntityManager.SetComponentData(star, new StarData
                {
                    Lifetime = this.random.NextFloat(generator.Lifetime.x, generator.Lifetime.y),
                    Speed = this.random.NextFloat2(new float2(generator.Speed.x, generator.Speed.x),
                                                   new float2(generator.Speed.y, generator.Speed.y))
                });

                float scale = this.random.NextFloat(generator.Scale.x, generator.Scale.y);
                this.EntityManager.AddComponent<NonUniformScale>(star);
                this.EntityManager.SetComponentData(star, new NonUniformScale
                {
                    Value = new float3(scale,scale,scale)
                });

                this.EntityManager.SetComponentData(star, new Translation
                {
                    Value = this.random.NextFloat3(
                                                   new float3(generator.Bounds.x, generator.Bounds.z, 5),
                                                   new float3(generator.Bounds.y, generator.Bounds.w, 5)
                                                   )
                });

            }
        }
    }
}