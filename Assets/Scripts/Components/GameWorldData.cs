using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct GameWorldData : IComponentData
    {
        public float4 Bounds;

        public float AlienMaxSpeed;
        public float EndGameLine;
    }
}