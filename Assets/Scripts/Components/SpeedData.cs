using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct SpeedData : IComponentData
    {
        public float3 Value;
    }
}