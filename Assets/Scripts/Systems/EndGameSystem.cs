using Assets.Scripts.Components;
using Unity.Entities;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    [UpdateAfter(typeof(AlienMovementSystem))]
    public class EndGameSystem : ComponentSystem
    {
        private EntityQuery alienQuery;
        private EntityQuery playerQuery;

        protected override void OnCreate()
        {
            this.alienQuery = this.GetEntityQuery(ComponentType.ReadOnly<AlienTag>(),
                                                  ComponentType.ReadOnly<Translation>());
            this.playerQuery = this.GetEntityQuery(ComponentType.ReadOnly<PlayerTag>());
        }

        protected override void OnUpdate()
        {
            GameWorldData gameData = this.GetSingleton<GameWorldData>();

            GameStatusData status = this.GetSingleton<GameStatusData>();

            if(this.playerQuery.CalculateEntityCount() == 0)
            {
                //player lost
                status.Active = false;
                status.PlayerWin = false;
            }

            if (this.alienQuery.CalculateEntityCount() == 0)
            {
                //player won
                status.Active = false;
                status.PlayerWin = true;
            }

            this.Entities
                .WithAll<AlienTag>()
                .ForEach((ref Translation position) =>
                {
                    if (position.Value.y <= gameData.EndGameLine)
                    {
                        //player lost
                        status.Active = false;
                        status.PlayerWin = false;

                    }
                });
            this.SetSingleton(status);
        }
    }
}