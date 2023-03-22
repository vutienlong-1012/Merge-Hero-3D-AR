using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MergeAR
{
    public class MotionController : MonoBehaviour
    {
        public Animator animator;

        public void SetBoolAtk(bool value)
        {
            animator.SetBool("isAttacking", value);
        }

        public void SetBoolRun(bool value)
        {
            animator.SetBool("IsRun", value);
        }

        public void SetTriggerDie()
        {
            animator.SetTrigger("Die");
        }

        public void SetTriggerPickedUp()
        {
            animator.SetTrigger("PickedUp");
        }

        public void SetTriggerDropDown()
        {
            animator.SetTrigger("DropDown");
        }

        public void SetBoolPickedUp(bool value)
        {
            animator.SetBool("IsPickedUp", value);
        }

        public void SetTriggerJump()
        {
            animator.SetTrigger("Jump");
        }

        public void SetTriggerDance()
        {
            animator.SetTrigger("Dance");
        }
    }
}