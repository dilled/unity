using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
    
{
    RedEye redEye;
    public override void Die()
    {
        base.Die();
        redEye = GetComponent<RedEye>();
        redEye.NotAttackEye();
        // Destroy(gameObject);
        GetComponent<BabyController>().SetSleeping();
    }
    public override void Born()
    {
        base.Born();
    }

}
