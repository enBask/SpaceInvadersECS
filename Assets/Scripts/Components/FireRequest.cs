using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Components
{
    public struct FireRequest : IComponentData
    {
        public Entity Requester;
        public float3 Position;
    }
}