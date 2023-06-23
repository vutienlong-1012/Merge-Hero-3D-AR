using DG.Tweening;
using MergeAR.UI;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;
using VTLTools;

namespace MergeAR
{
    [Serializable]
    public class Character : MonoBehaviour, IDamageable
    {
        [TabGroup("Information")]
        public CharacterData data;

        [TabGroup("Information"), ReadOnly]
        public GameObject target;

        CharacterState state;
        [TabGroup("Information"), ShowInInspector, ReadOnly]
        public CharacterState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                switch (state)
                {
                    case CharacterState.Idle:
                        break;
                    case CharacterState.LookingForTarget:
                        HandleLookingForTargetState();
                        break;
                    case CharacterState.MoveToTarget:
                        HandleMoveToTargetState();
                        break;
                    case CharacterState.AttackTarget:
                        HandleAttackTargetState();
                        break;
                    case CharacterState.Dead:
                        HandleDeadState();
                        break;
                    case CharacterState.Dance:
                        HandleDanceState();
                        break;
                    case CharacterState.Fall:
                        HandleFallState();
                        break;
                }
            }
        }

        float currentHealth;
        [TabGroup("Information"), ShowInInspector, ReadOnly, ProgressBar(0f, "@(data == null) ? 0f : (float)data.startHealth", ColorGetter = "GetHealthBarColor")]
        public float CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            private set
            {
                currentHealth = Mathf.Clamp(value, 0, value);
                healthBar.UpdateHealthSlider(currentHealth);
                if (currentHealth <= 0)
                {
                    State = CharacterState.Dead;
                    healthBar.gameObject.SetActive(false);
                }
            }
        }

        [TabGroup("Information"), ShowInInspector]
        public bool IsDead
        {
            get => CurrentHealth <= 0;
        }

        public UnityEvent OnDeadEvent;

        [TabGroup("References"), SerializeField]
        GameObject model;

        [TabGroup("References"), SerializeField]
        SkinnedMeshRenderer skinnedMeshRenderer;

        [TabGroup("References")]
        public MotionController motionController;

        [TabGroup("References"), SerializeField]
        DamageTextPopup DamageTextPopupPrefab;

        [TabGroup("References"), SerializeField]
        CharacterCanvas healthBar;

        [TabGroup("References"), SerializeField]
        Transform leftWeaponSlot;

        [TabGroup("References"), SerializeField]
        Transform rightWeaponSlot;

        LookToTarget lookToTarget;
        [TabGroup("Components"), ShowInInspector]
        LookToTarget LookToTarget
        {
            get
            {
                if (lookToTarget == null)
                    lookToTarget = this.GetComponent<LookToTarget>();
                return lookToTarget;
            }
        }

        MoveToTarget moveToTarget;
        [TabGroup("Components"), ShowInInspector]
        MoveToTarget MoveToTarget
        {
            get
            {
                if (moveToTarget == null)
                    moveToTarget = this.GetComponent<MoveToTarget>();
                return moveToTarget;
            }
        }

        Rigidbody characterRigidbody;
        [TabGroup("Components"), ShowInInspector]
        Rigidbody Rigidbody
        {
            get
            {
                if (characterRigidbody == null)
                    characterRigidbody = this.GetComponent<Rigidbody>();
                return characterRigidbody;
            }
        }

        CapsuleCollider capsuleCollider;
        [TabGroup("Components"), ShowInInspector]
        CapsuleCollider CapsuleCollider
        {
            get
            {
                if (capsuleCollider == null)
                    capsuleCollider = this.GetComponent<CapsuleCollider>();
                return capsuleCollider;
            }
        }

        AudioSource audioSource;
        [TabGroup("Components"), ShowInInspector]
        protected AudioSource AudioSource
        {
            get
            {
                if (audioSource == null)
                    audioSource = this.GetComponent<AudioSource>();
                return audioSource;
            }
        }

        [SerializeField, TabGroup("FX")] ParticleSystem spawnParticleSystem;
        [SerializeField, TabGroup("FX")] ParticleSystem deadParticleSystem;
        [SerializeField, TabGroup("FX")] AudioClip attackAudioClip;
        [SerializeField, TabGroup("FX")] AudioClip deadAudioClip;

        protected virtual void OnEnable()
        {
            EventDispatcher.Instance.AddListener(EventName.OnGameStateChanged, OnListenToGameState);
        }

        protected virtual void OnDisable()
        {
#if UNITY_EDITOR
            if (CharacterDataManager.Instance == null)
                return;
#endif
            if (data.Faction == CharacterFaction.Enemy)
                CharacterDataManager.Instance.enemyCharacters.Remove(this);
            else
                CharacterDataManager.Instance.friendlyCharacters.Remove(this);

            EventDispatcher.Instance.RemoveListener(EventName.OnGameStateChanged, OnListenToGameState);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            other.GetComponent<IInteractable>()?.Interact(this);
        }

        protected virtual void Start()
        {
        }

        public void Init(CharacterData _data, Transform _parent = null, Vector3 _localPosition = default, Quaternion _localRotation = default)
        {
            this.data = _data;
            this.transform.SetParent(_parent);
            this.transform.localPosition = _localPosition;
            this.transform.localRotation = _localRotation;

            skinnedMeshRenderer.sharedMesh = data.mesh;
            skinnedMeshRenderer.material = data.material;

            Vector3 _scale = Vector3.one + new Vector3(0.04f, 0.04f, 0.04f) * data.scaleLevel;
            this.transform.localScale = Vector3.zero;
            this.transform.DOScale(_scale, 0.5f);


            motionController.animator.Rebind();
            motionController.animator.Update(0);


            model.transform.localPosition = Vector3.zero;

            motionController.animator.runtimeAnimatorController = data.animatorOverrideController;

            CurrentHealth = data.startHealth;

            spawnParticleSystem.Play();

            Helpers.DestroyAllChilds(leftWeaponSlot);
            Helpers.DestroyAllChilds(rightWeaponSlot);
            if (data.leftWeapon != null)
                Instantiate(data.leftWeapon, leftWeaponSlot);
            if (data.rightWeapon != null)
                Instantiate(data.rightWeapon, rightWeaponSlot);
        }
        public void OnListenToGameState(EventName _key, object _data)
        {
            if (IsDead)
                return;
            switch ((GameState)_data)
            {
                case GameState.None:
                    break;
                case GameState.ResetRound:
                    break;
                case GameState.Starting:
                    break;
                case GameState.Idle:
                    break;
                case GameState.Fight:
                    if (data.Faction == CharacterFaction.Enemy)
                    {
                        if (!CharacterDataManager.Instance.enemyCharacters.Contains(this))
                            return;
                    }
                    else
                    {
                        if (!CharacterDataManager.Instance.friendlyCharacters.Contains(this))
                            return;
                    }

                    State = CharacterState.LookingForTarget;
                    CapsuleCollider.isTrigger = false;
                    CapsuleCollider.radius = 0.2f;
                    healthBar.gameObject.SetActive(true);
                    break;
                case GameState.DefeatBattle:
                    if (data.Faction == CharacterFaction.Enemy)
                        State = CharacterState.Dance;
                    healthBar.gameObject.SetActive(false);
                    break;
                case GameState.VictoryBattle:
                    if (data.Faction == CharacterFaction.Friendly)
                        State = CharacterState.Dance;
                    healthBar.gameObject.SetActive(false);
                    break;
            }
        }

        void HandleLookingForTargetState()
        {
            MoveToTarget.allowToMove = false;
            LookToTarget.LookingForClosestTarget();
        }

        void HandleMoveToTargetState()
        {
            MoveToTarget.allowToMove = true;
            motionController.SetBoolRun(true);
            motionController.SetBoolAtk(false);
        }

        void HandleAttackTargetState()
        {
            //MoveToTarget.allowToMove = false;
            motionController.SetBoolAtk(true);
            motionController.SetBoolRun(false);
        }

        void HandleDeadState()
        {
            SoundSystem.Instance.PlaySoundOneShot(AudioSource, deadAudioClip);
            motionController.SetTriggerDead();
            if (GameManager.Instance.State == GameState.Fight)
            {
                CharacterDataManager.Instance.CharacterDeadCheck(this);

                OnDeadEvent.Invoke();
                OnDeadEvent.RemoveAllListeners();
            }
            healthBar.gameObject.SetActive(false);
            CapsuleCollider.isTrigger = true;
            MoveToTarget.allowToMove = false;
            target = null;

            deadParticleSystem.Play();
        }

        void HandleDanceState()
        {
            motionController.SetTriggerDance();
            MoveToTarget.allowToMove = false;
        }

        void HandleFallState(float _downTime = 1, float _downHeight = -5, float _downFar = 5)
        {
            this.transform.DOMoveY(_downHeight, _downTime);
            this.transform.DOMoveZ(this.transform.position.z + _downFar, _downTime);
        }

        public void RunToBattlefield()
        {
            motionController.SetBoolRun(true);
        }

        public void OnTargetDead()
        {
            if (IsDead)
                return;
            State = CharacterState.LookingForTarget;
        }

        public virtual void TakeDamage(float _damage)
        {
            if (IsDead)
                return;
            if (GameManager.Instance.State != GameState.Fight)
                return;

            CurrentHealth -= _damage;

            DamageTextPopup _pop = ObjectPool.Spawn(DamageTextPopupPrefab);
            _pop.transform.position = this.transform.position + new Vector3(0, 2, 0);
            _pop.SetDamageText(_damage, data.Faction);
            UIManager.Instance.fightPopup.UpdateSliderHealBar(data.Faction);
        }


        //Attach this to event of attack animation clip
        public virtual void Attack()
        {
            SoundSystem.Instance.PlaySoundOneShot(AudioSource, attackAudioClip);
        }


        public void SetHelperStat(CharacterData _charData)
        {
            data.startHealth = _charData.startHealth;
            CurrentHealth = data.startHealth;
            data.damage = _charData.damage;
        }
        #region Editor
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            if (data != null)
                Gizmos.DrawWireSphere(transform.position, data.attackRange);
            if (target != null)
                Gizmos.DrawLine(this.transform.position, target.transform.position);
        }

        private Color GetHealthBarColor(float value)
        {
            return Color.Lerp(Color.red, Color.green, Mathf.Pow(value / ((data == null) ? 0f : (float)data.startHealth), 2));
        }
        #endregion
    }
}