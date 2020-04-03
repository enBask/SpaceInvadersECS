using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Components;
using Unity.Entities;
using UnityEngine;

public class PlayerAuthoringBehavior : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddBuffer<InputData>(entity);
    }
}
