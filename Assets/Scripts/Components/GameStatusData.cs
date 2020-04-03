using Unity.Entities;

namespace Assets.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct GameStatusData : IComponentData
    {
        public bool Active;
        public bool PlayerWin;
    }
}