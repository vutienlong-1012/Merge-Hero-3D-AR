using Sirenix.OdinInspector;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    public class RangedCharacter : Character
    {
        [TabGroup("References")]
        public Transform firePos;

        public override void Attack()
        {
            base.Attack();

            Bullet _bu = ObjectPool.Spawn(data.bullet);
            _bu.transform.position = firePos.position;
            //if (GameManager.Instance.State == VTLTools.GameState.Run)
            //{
            //    if (PlayerManager.Instance.currentRoadEnemy == null)
            //    {
            //        Destroy(_bu.gameObject);
            //        return;
            //    }
            //    _bu.Init(firePos, PlayerManager.Instance.currentRoadEnemy.TargetForRanged(), 0);
            //}
            //else
            //{
            if (target == null)
            {
                Destroy(_bu.gameObject);
                return;
            }
            else
            {
                _bu.Init(firePos, target.transform, data.damage);
            }
            // }
        }
    }
}