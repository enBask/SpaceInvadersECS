using Unity.Entities;

namespace Assets.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct AlienAttackData : IComponentData
    {
        public float MinDelay;
        public float MaxDelay;
        public float Delay;
        public float Accumulator;
    }
}