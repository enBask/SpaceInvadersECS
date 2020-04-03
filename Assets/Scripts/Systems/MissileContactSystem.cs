using Assets.Scripts.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    public class MissileContactSystem : ComponentSystem
    {
        private EntityQuery teamQuery;

        protected override void OnCreate()
        {
            this.teamQuery = this.GetEntityQuery(ComponentType.ReadOnly<TeamData>(),
                                                 ComponentType.ReadOnly<LocalToWorld>());
        }

        protected override void OnUpdate()
        {
            NativeArray<Entity> entities = this.teamQuery.ToEntityArray(Allocator.TempJob);
            NativeArray<TeamData> teams = this.teamQuery.ToComponentDataArray<TeamData>(Allocator.TempJob);
            NativeArray<LocalToWorld> positions = this.teamQuery.ToComponentDataArray<LocalToWorld>(Allocator.TempJob);

            this.Entities.ForEach((Entity entity, ref MissileData missile, ref LocalToWorld position) =>
            {
                for(int i=0; i < teams.Length; ++i)
                {
                    Entity teamEntity = entities[i];
                    if (entity == teamEntity) continue;

                    TeamData team = teams[i];
                    float3 teamPos = positions[i].Position;

                    float distance = math.distance(position.Position, teamPos);
                    if (distance < .5f)
                    {
                        this.PostUpdateCommands.DestroyEntity(entity);
                        if (missile.Team != team.Value)
                        {
                            this.PostUpdateCommands.DestroyEntity(teamEntity);
                        }
                    }
                }

            });

            entities.Dispose();
            teams.Dispose();
            positions.Dispose();
        }
    }
}