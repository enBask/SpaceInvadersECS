using System.ComponentModel;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct StarData : IComponentData
    {
        public float2 Speed;
        public float Lifetime;
        public float Accumulator;
    }
}