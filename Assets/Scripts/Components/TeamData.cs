using Unity.Entities;

namespace Assets.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct TeamData : IComponentData
    {
        public Team Value;
    }
}