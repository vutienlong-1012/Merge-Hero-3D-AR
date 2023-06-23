using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    [InlineEditor]
    public class FriendlyGrid : Grid
    {
        MeshRenderer meshRenderer;
        [ShowInInspector, ReadOnly]
        MeshRenderer ThisMeshRenderer
        {
            get
            {
                if (meshRenderer != null)
                    return meshRenderer;
                else
                    return this.GetComponent<MeshRenderer>();
            }
        }

        bool isMergeable;

        [SerializeField] MergeableFX MergeableFX;


        [SerializeField] Material spaceMaterial;
        [SerializeField] Material cityMaterial;
        [SerializeField, ReadOnly] Color defaultColor;

        EventDispatcher eventDispatcher;
        protected override void OnEnable()
        {
            base.OnEnable();
            eventDispatcher = EventDispatcher.Instance;
            eventDispatcher.AddListener(EventName.OnHoldCharacter, OnCheckGridColor);
            eventDispatcher.AddListener(EventName.OnHoldCharacter, OnCheckGridHoldFx);
            eventDispatcher.AddListener(EventName.OnReleaseCharacter, OnResetGrid);
            eventDispatcher.AddListener(EventName.OnReleaseCharacter, OnCheckGridIdleFx);
            eventDispatcher.AddListener(EventName.OnBuyCharacter, OnCheckGridIdleFx);

            eventDispatcher.AddListener(EventName.OnGameStateChanged, OnListenGameStateChange);
            eventDispatcher.AddListener(EventName.OnLoadBackground, OnListenLoadBackground);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            eventDispatcher.RemoveListener(EventName.OnHoldCharacter, OnCheckGridColor);
            eventDispatcher.RemoveListener(EventName.OnHoldCharacter, OnCheckGridHoldFx);
            eventDispatcher.RemoveListener(EventName.OnReleaseCharacter, OnResetGrid);
            eventDispatcher.RemoveListener(EventName.OnReleaseCharacter, OnCheckGridIdleFx);
            eventDispatcher.RemoveListener(EventName.OnBuyCharacter, OnCheckGridIdleFx);

            eventDispatcher.RemoveListener(EventName.OnGameStateChanged, OnListenGameStateChange);
            eventDispatcher.RemoveListener(EventName.OnLoadBackground, OnListenLoadBackground);
        }

        private void OnResetGrid(EventName _key, object _data)
        {
            ThisMeshRenderer.material.color = defaultColor;
            if (CurrentCharacter == null)
            {
                isMergeable = false;
                MergeableFX.SetActive(false);
            }
        }
        private void OnCheckGridColor(EventName _key, object _data)
        {
            if (_data == null)
                return;

            Character _chosenCharacter = (Character)_data;

            if (this != ControlManager.Instance.CurrentFriendlyGrid)
            {
                ThisMeshRenderer.material.color = defaultColor;
                return;
            }

            //if current friendly grid don't have character inside
            if (CurrentCharacter == null)
            {
                if (_data != null)
                    ThisMeshRenderer.material.color = Color.green;
            }
            else
            {
                //if current friendly grid's character have different id or max level unit
                if (CurrentCharacter.data.iD != _chosenCharacter.data.iD
                    || CurrentCharacter.data.iD == CharacterID.FM10
                    || CurrentCharacter.data.iD == CharacterID.FR10)
                {
                    ThisMeshRenderer.material.color = Color.red;
                }
                //Current friendly grid have same ID, good to merge
                else
                {
                    ThisMeshRenderer.material.color = Color.green;
                }
            }

        }
        private void OnCheckGridHoldFx(EventName _key, object _data)
        {
            if (CurrentCharacter == null)
            {
                isMergeable = false;
                MergeableFX.SetActive(false);
                return;
            }
            if (_data == null)
                return;

            if (((Character)_data).data.iD != CurrentCharacter.data.iD
                || CurrentCharacter.data.iD == CharacterID.FM10
                || CurrentCharacter.data.iD == CharacterID.FR10)
            {
                isMergeable = false;
                MergeableFX.SetActive(false);
            }
            else
            {
                isMergeable = true;
                MergeableFX.SetActive(true, CurrentCharacter.data.Type, true);
            }
        }
        private void OnCheckGridIdleFx(EventName _key, object _data)
        {
            CheckIfHaveAnySameChar();
        }
        private void OnListenGameStateChange(EventName _key, object _data)
        {
            switch ((GameState)_data)
            {
                case GameState.Idle:
                    ThisMeshRenderer.material.DOFade(0, 0f);
                    break;
                case GameState.Merging:
                    CheckIfHaveAnySameChar();
                    ThisMeshRenderer.material.DOFade(1, 0.5f);
                    break;
                case GameState.Fight:
                    isMergeable = false;
                    MergeableFX.SetActive(false);
                    ThisMeshRenderer.material.DOFade(0, 0.5f);
                    break;

            }
        }

        private void OnListenLoadBackground(EventName _key, object _data)
        {
            switch ((EnvironmentBackground)_data)
            {
                case EnvironmentBackground.None:
                    break;
                case EnvironmentBackground.Space:
                    ThisMeshRenderer.material = spaceMaterial;
                    break;
                case EnvironmentBackground.City:
                    ThisMeshRenderer.material = cityMaterial;
                    break;
            }
            defaultColor = ThisMeshRenderer.material.color;
        }
        void CheckIfHaveAnySameChar()
        {
            if (CurrentCharacter == null)
                return;

            foreach (var _item in GridManager.Instance.friendlyGrids)
            {
                if (_item == this)
                {
                    continue;
                }
                else
                {
                    if (_item.CurrentCharacter == null)
                        continue;
                    if (CurrentCharacter.data.iD == _item.CurrentCharacter.data.iD
                       && CurrentCharacter.data.iD != CharacterID.FM10
                       && CurrentCharacter.data.iD != CharacterID.FR10)
                    {
                        isMergeable = true;
                        MergeableFX.SetActive(true, CurrentCharacter.data.Type, false);

                        TutorialManager.Instance.CheckTutorialFirstMerge(this.transform, _item.transform);
                        return;
                    }
                    TutorialManager.Instance.CheckTutorialFirstMove(this.transform);

                    isMergeable = false;
                    MergeableFX.SetActive(false);
                }
            }
        }
    }
}