using MergeAR;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;
using Sirenix.OdinInspector;

public class LookToTarget : MonoBehaviour
{
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

    [ShowInInspector, ReadOnly]
    Transform Target
    {
        get
        {
            if (Character.target != null)
                return Character.target.transform;
            else
                return null;
        }
        set
        {
            Character.target = value.gameObject;
        }
    }

    [SerializeField] float rotateSpeed = 1;

    private void Update()
    {
        if (Target != null)
            LookAtTarget();
    }

    public void LookingForClosestTarget()
    {
        float _closestDistance = Mathf.Infinity;

        if (Character.data.Faction == CharacterFaction.Enemy)
        {
            _LoopThroughList(CharacterDataManager.Instance.friendlyCharacters);
            Debug.DrawLine(this.transform.position, Target.transform.position, Color.red, 5);
        }
        else
        {
            _LoopThroughList(CharacterDataManager.Instance.enemyCharacters);
            Debug.DrawLine(this.transform.position + new Vector3(0, 2, 0), Target.transform.position + new Vector3(0, 2, 0), Color.green, 5);
        }

        if (Target != null)
            if (GameManager.Instance.State == GameState.Fight)
                if (_closestDistance < character.data.attackRange)
                    character.State = CharacterState.AttackTarget;
                else
                    character.State = CharacterState.MoveToTarget;

        void _LoopThroughList(List<Character> _list)
        {
            foreach (var _opponent in _list)
            {
                float distance = Vector3.Distance(this.transform.position, _opponent.transform.position);

                if (distance < _closestDistance)
                {
                    _closestDistance = distance;
                    _opponent.OnDeadEvent.AddListener(Character.OnTargetDead);
                    Target = _opponent.transform;
                }
            }
        }


    }

    Vector3 _direction;
    Quaternion _lookRotation;
    public void LookAtTarget()
    {
        _direction = (Target.position - transform.position).normalized;
        _lookRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotateSpeed);
    }
}
