using Unity.Entities;

namespace Assets.Scripts.Components
{

    [InternalBufferCapacity(64)]
    public struct InputData : IBufferElementData
    {
        public bool Left;
        public bool Right;
        public bool Fire;
    }
}