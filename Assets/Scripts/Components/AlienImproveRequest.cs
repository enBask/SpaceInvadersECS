using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Components
{
    public struct AlienImproveRequest : IComponentData
    {
        public float3 NewSpeed;
        public float Drift;
    }
}