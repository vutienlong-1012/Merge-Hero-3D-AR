using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI.NewHeroesPopup
{
    public class NewHeroesPopup : PopupBase
    {
        [SerializeField, BoxGroup("Popup Reference")] Text levelText;
        [SerializeField, BoxGroup("Popup Reference")] SkinnedMeshRenderer heroSkinnedMeshRenderer;
        [SerializeField, BoxGroup("Popup Reference")] Text healthText;
        [SerializeField, BoxGroup("Popup Reference")] Text damageText;
        [SerializeField, BoxGroup("Popup Reference")] Button collectButton;
        [SerializeField, BoxGroup("Popup Reference")] Button watchAdsButton;
        [SerializeField] AudioClip newHeroAudioClip;

        [SerializeField] Animator animator;
        [SerializeField] Transform leftSlotWeapon;
        [SerializeField] Transform rightSlotWeapon;

        public override void Show(object _data = null, float _delay = 0, Action _actionOnStartShow = null, Action _actionOnCompleteShow = null, Action _actionOnStartHide = null, Action _actionOnCompleteHide = null)
        {
            base.Show(_data, _delay, _actionOnStartShow, _actionOnCompleteShow, _actionOnStartHide, _actionOnCompleteHide);
            SetInteractablePopup(true);

            SoundSystem.Instance.PlaySoundOneShot(SoundSystem.Instance.uIAudioSource, newHeroAudioClip);

            CharacterData _char = (CharacterData)_data;

            heroSkinnedMeshRenderer.sharedMesh = _char.mesh;
            heroSkinnedMeshRenderer.material = _char.material;


            //Set new Idle animation for model
            AnimatorOverrideController animatorOverrideController;
            animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = animatorOverrideController;
            animatorOverrideController["MeleeIdle"] = _char.animatorOverrideController.animationClips[1]; //"MeleeIdle" mean default name of animation in Animation Controller, 1 mean order of Idle in override controller
            animatorOverrideController["MeleeAtk"] = _char.animatorOverrideController.animationClips[0];


            //Spawn Weapon
            Helpers.DestroyAllChilds(leftSlotWeapon);
            Helpers.DestroyAllChilds(rightSlotWeapon);

            if (_char.leftWeapon != null)
            {
                GameObject _weapon = Instantiate(_char.leftWeapon, leftSlotWeapon);
                foreach (Transform _child in _weapon.GetComponentsInChildren<Transform>(true))
                {
                    _child.gameObject.layer = LayerMask.NameToLayer("UI");
                }
            }
            if (_char.rightWeapon != null)
            {
                {
                    GameObject _weapon = Instantiate(_char.rightWeapon, rightSlotWeapon);
                    foreach (Transform _child in _weapon.GetComponentsInChildren<Transform>(true))
                    {
                        _child.gameObject.layer = LayerMask.NameToLayer("UI");
                    }
                }
            }       


            levelText.text = (I2.Loc.ScriptLocalization.LEVEL + " " + _char.scaleLevel).ToUpper();
            healthText.text = _char.startHealth.ToString();
            damageText.text = _char.damage.ToString();

            if (!StaticVariables.IsUnlockedAtLeastOneHero)
            {
                StaticVariables.IsUnlockedAtLeastOneHero = true;
                collectButton.gameObject.SetActive(true);
                watchAdsButton.gameObject.SetActive(false);
            }
            else
            {
                collectButton.gameObject.SetActive(false);
                watchAdsButton.gameObject.SetActive(true);
            }
        }

        protected override void ButtonAddListener()
        {
            base.ButtonAddListener();
            collectButton.onClick.AddListener(OnClickCollectButton);
            closeButton.onClick.AddListener(() =>
            {
                SetInteractablePopup(false);
            });
        }

        protected override void ButtonRemoveListener()
        {
            base.ButtonRemoveListener();
            collectButton.onClick.RemoveListener(OnClickCollectButton);
            closeButton.onClick.RemoveListener(() =>
            {
                SetInteractablePopup(false);
            });
        }

        void OnClickCollectButton()
        {
            SoundSystem.Instance.PlayUIClick();
            VibrationSystem.Instance.PlayVibration();
            SetInteractablePopup(false);
            this.Hide();
        }

        public void SetInteractablePopup(bool _value)
        {
            collectButton.interactable = _value;
            watchAdsButton.interactable = _value;
        }


        //protected Animator animator;
        protected AnimatorOverrideController animatorOverrideController;
        private void SetAnimation(AnimationClip newMotion)
        {
            animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = animatorOverrideController;

            animatorOverrideController.animationClips[0] = newMotion;
        }
    }
}