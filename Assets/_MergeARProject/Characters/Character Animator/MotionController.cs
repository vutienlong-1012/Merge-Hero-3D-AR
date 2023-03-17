using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MergeAR
{
    public class MotionController : MonoBehaviour
    {
        public Animator animator;

        public AnimatorOverrideController overrideController;
        public string Music_Name_Attack;

        public ParticleSystem spawnEffect;
        public ParticleSystem dieEffect;

        private void OnEnable()
        {
            if (spawnEffect != null)
                spawnEffect.Play();
        }
        private void Start()
        {
            animator.runtimeAnimatorController = overrideController;
        }

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
            dieEffect.Play();
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

        public void SetTriggerJump(float timeJump)
        {
            Debug.Log(timeJump);
            Debug.Log((timeJump / 0.792f));
            animator.speed /= (timeJump / 0.792f);
            Debug.Log(animator.speed);
            StartCoroutine(BackToNomalSpeed(timeJump));
            animator.SetTrigger("Jump");
        }

        IEnumerator BackToNomalSpeed(float time)
        {
            yield return new WaitForSeconds(time);
            animator.speed = 1;
        }

        public void SetTriggerDance()
        {
            animator.SetTrigger("Dance");
        }
    }
}