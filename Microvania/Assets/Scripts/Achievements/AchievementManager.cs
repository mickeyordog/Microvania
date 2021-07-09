using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField]
    List<PickupAchievement> pickupAchievements;
    [SerializeField]
    List<EnemyAchievement> enemyAchievements;

    public static AchievementManager Instance;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        if (Instance != this)
        {
            Debug.Log("More than one instance of " + name);
            Destroy(this);
        }

        foreach (var achievement in pickupAchievements)
        {
            achievement.Init();
        }
        foreach (var achievement in enemyAchievements)
        {
            achievement.Init();
        }
    }

    public void AchievementCompleted(Achievement achievement)
    {
        StartCoroutine(AchievementPanel.Instance.ShowAchievement(achievement));
    }

   

}

public class Achievement
{
    public string title = "Achievement";
    public string description = "Description";
    public int requiredNumber;
    public int currentNumber = 0;
    [HideInInspector]
    public bool isCompleted = false;

    public virtual void Init()
    {

    }

    protected void ConditionMet()
    {
        if (isCompleted)
            return;
        currentNumber++;
        CheckIfCompleted();
    }

    void CheckIfCompleted()
    {
        if (currentNumber >= requiredNumber)
        {
            isCompleted = true;
            AchievementManager.Instance.AchievementCompleted(this);

        }
    }

}


[System.Serializable]
public class EnemyAchievement : Achievement
{
    public bool hasRequiredEnemyType;
    public EnemyType requiredEnemyType;
    public override void Init()
    {
        base.Init();
        Enemy.OnAnyEnemyKilled += EnemyKilled;
    }

    private void EnemyKilled(EnemyType enemyType)
    {
        if (!hasRequiredEnemyType || enemyType == requiredEnemyType)
           ConditionMet();
    }
}

[System.Serializable]
public class PickupAchievement : Achievement
{
    public Item requiredItem;

    public override void Init()
    {
        base.Init();
        PlayerInventory.OnAnyItemPickedUp += ItemPickedUp;
    }

    private void ItemPickedUp(Item item)
    {
        if (item == requiredItem)
        {
            ConditionMet();
        }
    }
}