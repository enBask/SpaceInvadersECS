using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct MissileData : IComponentData
    {
        public Team Team;
        public float3 Offset;
    }
}