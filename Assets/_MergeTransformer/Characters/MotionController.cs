using System.Collections;
using UnityEngine;

namespace MergeAR
{
    public class MotionController : MonoBehaviour
    {
        public Animator animator;

        public AnimationClip[] victoryDanceAnimationClips;

        public void SetBoolAtk(bool value)
        {
            animator.SetBool("isAttacking", value);
        }

        public void SetBoolRun(bool value)
        {
            animator.SetBool("IsRun", value);
        }

        public void SetTriggerDead()
        {
            animator.SetTrigger("Die");
        }

        public void SetTriggerDropDown()
        {
            animator.SetTrigger("DropDown");
        }

        public void SetBoolPickedUp(bool value)
        {
            animator.SetBool("IsPickedUp", value);
        }

        public void SetTriggerJump(float _timeJump)
        {
            animator.speed /= (_timeJump / 1);  //this number is length of jump animation clip
            StartCoroutine(BackToNormalSpeed(_timeJump));
            animator.SetTrigger("Jump");
        }

        IEnumerator BackToNormalSpeed(float time)
        {
            yield return new WaitForSeconds(time);
            animator.speed = 1;
        }

        public void SetTriggerDance()
        {

            AnimatorOverrideController animatorOverrideController = new(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = animatorOverrideController;

            //Animation clip name in AnimatorOverrideController is "Victory01"
            animatorOverrideController["Victory01"] = victoryDanceAnimationClips[Random.Range(0, victoryDanceAnimationClips.Length)];

            animator.SetTrigger("Dance");
        }
    }
}