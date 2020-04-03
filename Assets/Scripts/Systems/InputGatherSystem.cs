using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Components;
using Unity.Entities;
using UnityEngine;

public class InputGatherSystem : ComponentSystem
{
    private EntityQuery playerQuery;

    protected override void OnCreate()
    {
        this.playerQuery = this.GetEntityQuery(ComponentType.ReadOnly<PlayerTag>());
    }

    protected override void OnUpdate()
    {
        Entity playerEntity = this.playerQuery.GetSingletonEntity();
        DynamicBuffer<InputData> inputBuffer = this.GetBufferFromEntity<InputData>()[playerEntity];

        InputData input = default;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            input.Left = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            input.Right = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            input.Fire = true;
        }

        inputBuffer.Add(input);

    }
}
