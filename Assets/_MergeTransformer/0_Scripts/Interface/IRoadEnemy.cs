using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoadEnemy
{
    public bool IsFinishFight();

    public void EnemyVictory();

    public Transform TargetForRanged();
}

