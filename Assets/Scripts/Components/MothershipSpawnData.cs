using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct MothershipSpawnData : IComponentData
    {
        public Entity Prefrab;

        public float3 SpawnPos;
        public float MinDelay;
        public float MaxDelay;

        public float Delay;
        public float Accumulator;

    }
}