using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Components;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class GameOverBehavior : MonoBehaviour, IConvertGameObjectToEntity
{
    public Text TopText;
    public Text BottomText;

    private Entity entity;
    private EntityManager entityManager;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        this.entity = entity;
        this.entityManager = dstManager;
    }

    private void LateUpdate()
    {
        if (this.entityManager == null) return;
        if (!this.entityManager.Exists(this.entity)) return;
        GameStatusData status = this.entityManager.GetComponentData<GameStatusData>(this.entity);

        if (!status.Active)
        {
            this.TopText.gameObject.SetActive(true);
            this.BottomText.gameObject.SetActive(true);

            this.BottomText.text = status.PlayerWin ? "Player Won" : "Player Lost";
            Time.timeScale = 0f;
        }
    }
}
