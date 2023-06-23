using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using VTLTools;
using static Unity.VisualScripting.Member;

namespace MergeAR
{
    [InlineEditor]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] BulletData data;
        [SerializeField, ReadOnly] Transform target;
        [SerializeField, ReadOnly] float damage;

        [SerializeField] Transform modelPlacement;

        [SerializeField, ReadOnly] Vector3 offsetYTarget;

        Vector3 targetPosition;
        Vector3 TargetPosition
        {
            get
            {
                targetPosition = target.position;
                targetPosition.y = offsetYTarget.y;
                return targetPosition;
            }
        }

        private void Update()
        {
            MovingToTarget();
            this.transform.LookAt(TargetPosition);
        }

        public void Init(Transform _startPos, Transform _target, float _damage)
        {
            this.transform.position = _startPos.position;
            target = _target;
            damage = _damage;
            offsetYTarget = new Vector3(0, this.transform.position.y, 0);

            if (data.model != null)
                Instantiate(data.model, modelPlacement);

            if (data.spawnFx != null)
            {

                Effect _spawnEffect = ObjectPool.Spawn(data.spawnFx);

                Quaternion _spawnQuaternion = Helpers.GetQuaternionLookAt(this.transform.position, TargetPosition);
                _spawnEffect.Init(modelPlacement.position, _spawnQuaternion);
            }
        }
        void MovingToTarget()
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, data.speed * Time.deltaTime);

            // Check if the position of the bullet and target are approximately equal.
            if (Vector3.Distance(transform.position, TargetPosition) < 0.001f)
            {
                target.GetComponent<IDamageable>()?.TakeDamage(damage);
                Effect _tempFX = ObjectPool.Spawn(data.impactFx);
                _tempFX.transform.position = TargetPosition;
                Destroy(this.gameObject);
            }
        }
    }
}