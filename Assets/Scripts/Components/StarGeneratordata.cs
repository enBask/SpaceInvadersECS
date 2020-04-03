using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct StarGeneratordata : IComponentData
    {
        public Entity Prefab;
        public float4 Bounds;
        public int Count;
        public float2 Scale;
        public float2 Speed;
        public float2 Lifetime;

    }
}