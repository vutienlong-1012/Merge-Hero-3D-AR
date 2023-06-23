using DG.Tweening;
using MergeAR.UI;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using VTLTools;

namespace MergeAR
{
    public class ControlManager : Singleton<ControlManager>
    {
        [SerializeField] LayerMask friendlyGridLayerMask;
        [SerializeField] Camera cam;
        [SerializeField, ReadOnly] Character chosenCharacter;
        [SerializeField, ReadOnly] FriendlyGrid originFriendlyGrid;
        [ShowInInspector]
        public FriendlyGrid CurrentFriendlyGrid
        {
            get
            {
                if (_currentFriendlyGridTransform != null)
                {
                    if (_currentFriendlyGridTransform.GetComponent<FriendlyGrid>() != null)
                        return _currentFriendlyGridTransform.GetComponent<FriendlyGrid>();
                    else return null;
                }
                else
                    return null;
            }
        }

        [SerializeField] AudioClip mergeAudioClip;

        public bool isAllowToMerge = false;


        private void Update()
        {
            if (!isAllowToMerge)
                return;
            UpdateCenterGrid();
        }

        Ray _ray;
        RaycastHit _hit;
        Transform _currentFriendlyGridTransform;
        void UpdateCenterGrid()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (MouseHoverUI.IsPointerOverUIElement())
                    return;

                _ray = cam.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(_ray.origin, _ray.direction * 10, Color.yellow);
                if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, friendlyGridLayerMask))
                {
                    _currentFriendlyGridTransform = _hit.transform;
                    PickUpCharacter();
                    EventDispatcher.Instance.Dispatch(EventName.OnHoldCharacter, chosenCharacter);
                }
                else
                {
                    _currentFriendlyGridTransform = null;
                }
            }
            if (Input.GetMouseButton(0))
            {
                if (MouseHoverUI.IsPointerOverUIElement())
                    return;

                _ray = cam.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(_ray.origin, _ray.direction * 10, Color.yellow);
                if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, friendlyGridLayerMask))
                {
                    if (_hit.transform != _currentFriendlyGridTransform)
                    {
                        _currentFriendlyGridTransform = _hit.transform;
                        EventDispatcher.Instance.Dispatch(EventName.OnHoldCharacter, chosenCharacter);
                    }
                }
                else
                {
                    _currentFriendlyGridTransform = null;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                ReleaseCharacter();
                _currentFriendlyGridTransform = null;
            }
        }

        public void PickUpCharacter()
        {
            if (CurrentFriendlyGrid == null)
                return;
            if (!CurrentFriendlyGrid.IsHaveCharacter)
                return;

            chosenCharacter = CurrentFriendlyGrid.GetCurrentCharacter();
            chosenCharacter.motionController.SetBoolPickedUp(true);

            //CurrentFriendlyGrid.currentCharacter.SetNewParentCharacter(CursorControl.instance.cursor.transform, new Vector3(0, 0.2f, 0));
            SetNewParentCharacter(chosenCharacter.transform, CursorControl.Instance.cursor.transform, new Vector3(0, 0.2f, 0));
            CurrentFriendlyGrid.SetCurrentCharacter(null);
            originFriendlyGrid = CurrentFriendlyGrid;

            TutorialManager.instance.HideGuildHandTutorial();
            VibrationSystem.Instance.PlayVibration();
        }

        public void ReleaseCharacter()
        {
            if (chosenCharacter == null)
                return;

            chosenCharacter.motionController.SetBoolPickedUp(false);

            //If current friendly grid empty
            if (CurrentFriendlyGrid == null)
            {
                _BackToOriginGrid();
            }
            else
            {
                //if current friendly grid don't have character inside
                if (!CurrentFriendlyGrid.IsHaveCharacter)
                {
                    SetNewParentCharacter(chosenCharacter.transform, CurrentFriendlyGrid.transform, Vector3.zero);
                    CurrentFriendlyGrid.SetCurrentCharacter(chosenCharacter);
                    if (StaticVariables.CurrentTutorialPhase == TutorialPhase.FirstMoveCharacter && CurrentFriendlyGrid != originFriendlyGrid)
                        StaticVariables.CurrentTutorialPhase++;
                }
                else
                {
                    //if current friendly grid's character have different id
                    if (CurrentFriendlyGrid.GetCurrentCharacter().data.iD != chosenCharacter.data.iD)
                    {
                        _BackToOriginGrid();
                    }
                    //Current friendly grid have same ID
                    else
                    {
                        //Max level character, can not merge anymore
                        if (CurrentFriendlyGrid.GetCurrentCharacter().data.iD == CharacterID.FM10
                            || CurrentFriendlyGrid.GetCurrentCharacter().data.iD == CharacterID.FR10
                            || CurrentFriendlyGrid.GetCurrentCharacter().data.iD == CharacterID.HM
                            || CurrentFriendlyGrid.GetCurrentCharacter().data.iD == CharacterID.HR)
                        {
                            _BackToOriginGrid();
                        }
                        else // good to merge
                        {
                            SoundSystem.instance.PlaySoundOneShot(SoundSystem.instance.sharedAudioSource, mergeAudioClip);
                            VibrationSystem.Instance.PlayVibration();
                            CharacterData _charData = CharacterDataManager.Instance.GetCharacterDataByID(chosenCharacter.data.iD + 1);
                            if (!_charData.IsUnlocked)
                            {
                                _charData.IsUnlocked = true;
                                UIManager.instance.newHeroesPopup.Show(_charData);
                            }
                            CurrentFriendlyGrid.GetCurrentCharacter().Init(_charData, CurrentFriendlyGrid.transform);


                            Destroy(chosenCharacter.gameObject);
                            chosenCharacter = null;
                            GridManager.instance.CheckFriendlyGridFull();

                            if (StaticVariables.CurrentTutorialPhase == TutorialPhase.FirstMergeCharacter)
                                StaticVariables.CurrentTutorialPhase++;
                        }
                    }
                }
            }
            _ResetAndSaveControlData();
            EventDispatcher.Instance.Dispatch(EventName.OnReleaseCharacter, null);




            void _BackToOriginGrid()
            {
                SetNewParentCharacter(chosenCharacter.transform, originFriendlyGrid.transform, Vector3.zero);
                originFriendlyGrid.SetCurrentCharacter(chosenCharacter);
            }
            void _ResetAndSaveControlData()
            {
                chosenCharacter = null;
                originFriendlyGrid = null;
                CharacterDataManager.Instance.SaveFriendlyGrids();
                return;
            }
        }

        public void SetNewParentCharacter(Transform _obj, Transform _parent, Vector3 _offset, Action _action = null)
        {
            _obj.parent = _parent;
            _obj.DOKill();
            _obj.DOLocalMove(_offset, 0.3f).OnComplete(() => _action?.Invoke());
            _obj.DOLocalRotate(Vector3.zero, 0.3f);
        }
    }
}