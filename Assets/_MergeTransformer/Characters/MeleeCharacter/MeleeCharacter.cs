using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    public class MeleeCharacter : Character
    {
        //[SerializeField, TabGroup("FX")] protected Effect meleeImpactEffect;
        //[SerializeField, TabGroup("FX")] protected Effect meleeAttackEffect;
        public override void Attack()
        {
            base.Attack();
            if (GameManager.Instance.State == VTLTools.GameState.Fight)
            {
                if (target == null || Vector3.Distance(this.transform.position, target.transform.position) > data.attackRange)
                {

                    return;
                }
                else
                {
                    target.GetComponent<IDamageable>()?.TakeDamage(data.damage);

                    //Effect _tempFX = ObjectPool.Spawn(meleeImpactEffect);
                    //Vector3 _fxPosition = this.transform.forward + this.transform.position + new Vector3(0, 1.5f, 0);
                    //Quaternion _spawnQuaternion = Helpers.GetQuaternionLookAt(this.transform.position, target.transform.position);
                    //_tempFX.Init(_fxPosition, _spawnQuaternion);

                    _SpawnMeleeEffect(data.meleeImpactEffect);

                    if (data.meleeAttackEffect != null)
                        _SpawnMeleeEffect(data.meleeAttackEffect);

                    void _SpawnMeleeEffect(Effect _effect)
                    {
                        Effect _tempFX = ObjectPool.Spawn(_effect);
                        Vector3 _fxPosition = this.transform.forward + this.transform.position + new Vector3(0, 1.5f, 0);
                        Quaternion _spawnQuaternion = Helpers.GetQuaternionLookAt(this.transform.position, target.transform.position);
                        _tempFX.Init(_fxPosition, _spawnQuaternion);
                    }
                }
            }
            else
            {
                Effect _tempFX = ObjectPool.Spawn(data.meleeImpactEffect);
                _tempFX.transform.position = this.transform.forward + this.transform.position + new Vector3(0, 1.5f, 0);
            }
        }
    }
}