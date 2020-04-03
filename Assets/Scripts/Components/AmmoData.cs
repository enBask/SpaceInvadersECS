using Unity.Entities;

namespace Assets.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct AmmoData : IComponentData
    {
        public Entity Prefab;
        public float Firerate;
        public float Accumulator;
    }
}