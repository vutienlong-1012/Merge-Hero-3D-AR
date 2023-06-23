using UnityEngine;

namespace MergeAR
{
    public class MotionEventBridge : MonoBehaviour
    {
        [SerializeField] Character character;

        public void OnAttackEvent()
        {
            character.Attack();
        }
    }
}