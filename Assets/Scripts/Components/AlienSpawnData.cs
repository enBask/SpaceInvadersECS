using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.Components
{
    [GenerateAuthoringComponent]
    public struct AlienSpawnData : IComponentData
    {
        public Entity AlienPrefab;
        public Entity AlienRocketPrefab;

        public float AlienHeight;

        public float3 SpawnStart;

        public int Rows;
        public int Cols;
        public float3 Size;
    }
}