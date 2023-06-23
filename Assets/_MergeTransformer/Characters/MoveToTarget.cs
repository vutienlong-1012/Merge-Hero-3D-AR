using Sirenix.OdinInspector;
using UnityEngine;

namespace MergeAR
{
    public class MoveToTarget : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        Transform target
        {
            get
            {
                if (Character.target != null)
                    return Character.target.transform;
                else
                    return null;
            }
        }

        [ShowInInspector, ReadOnly]
        float speed
        {
            get
            {
                if (Character.data != null)
                    return Character.data.speed;
                else
                    return 0;
            }
        }

        [ShowInInspector, ReadOnly]
        float attackRange
        {
            get
            {
                if (Character.data != null)
                    return Character.data.attackRange;
                else
                    return 0;
            }
        }

        [ReadOnly]
        public bool allowToMove = false;

        Rigidbody rigibody;

        [ShowInInspector, ReadOnly]
        Vector3 Velocity => Rigibody.velocity;

        [ShowInInspector, ReadOnly]
        Rigidbody Rigibody
        {
            get
            {
                if (rigibody != null)
                {
                    return rigibody;
                }
                else
                {
                    rigibody = this.GetComponent<Rigidbody>();
                    return rigibody;
                }
            }
        }

        Character character;

        [ShowInInspector, ReadOnly]
        Character Character
        {
            get
            {
                if (character != null)
                {
                    return character;
                }
                else
                {
                    character = this.GetComponent<Character>();
                    return character;
                }
            }
        }

        void FixedUpdate()
        {
            MovingToTarget();
        }


        float _distance;
        Vector3 _direction;
        void MovingToTarget()
        {
            if (!allowToMove)
                return;

            if (target != null)
            {
                _distance = Vector3.Distance(transform.position, target.position);
                if (_distance > attackRange)
                {
                    if (Character.State == VTLTools.CharacterState.AttackTarget)
                    {
                        Character.State = VTLTools.CharacterState.MoveToTarget;
                    }
                    else
                    {
                        _direction = (target.position - transform.position).normalized;
                        Rigibody.velocity = _direction * speed;
                    }
                }
                else
                {
                    if (Character.State == VTLTools.CharacterState.MoveToTarget)
                    {
                        Rigibody.velocity = Vector3.zero;
                        Character.State = VTLTools.CharacterState.AttackTarget;
                    }
                }
            }
            else
            {
                Character.State = VTLTools.CharacterState.LookingForTarget;
            }
        }
    }
}