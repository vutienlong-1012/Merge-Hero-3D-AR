using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using VTLTools;

namespace MergeAR.Editor
{
    public class ControlManagerEditor : Singleton<ControlManagerEditor>
    {
        [SerializeField] LayerMask enemyGridLayerMask;
        [SerializeField] Camera cam;
        [SerializeField, ReadOnly] Character chosenCharacter;
        [SerializeField, ReadOnly] EnemyGrid originEnemyGrid;
        [ShowInInspector]
        public EnemyGrid CurrentEnemyGrid
        {
            get
            {
                if (_currentEnemyGridTransform != null)
                {
                    if (_currentEnemyGridTransform.GetComponent<EnemyGrid>() != null)
                        return _currentEnemyGridTransform.GetComponent<EnemyGrid>();
                    else return null;
                }
                else
                    return null;
            }
        }

        [SerializeField] Material enemyGridMat;

        private void Update()
        {
            UpdateCenterGrid();
        }

        Ray _ray;
        RaycastHit _hit;
        Transform _currentEnemyGridTransform;
        void UpdateCenterGrid()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, enemyGridLayerMask))
                {
                    if (_currentEnemyGridTransform != null)
                    {
                        _currentEnemyGridTransform.GetComponent<Renderer>().material.DOKill();
                        _currentEnemyGridTransform.GetComponent<Renderer>().material.DOFade(1, 0);

                    }
                    _currentEnemyGridTransform = _hit.transform;
                    _currentEnemyGridTransform.GetComponent<Renderer>().material.DOFade(0.2f, 0.5f).SetLoops(-1);

                    PickUpCharacter();
                    // EventDispatcher.Instance.Dispatch(EventName.OnHoldCharacter, chosenCharacter);
                }

            }
            if (Input.GetMouseButton(0))
            {
                _ray = cam.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(_ray.origin, _ray.direction * 10, Color.yellow);
                if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, enemyGridLayerMask))
                {
                    if (_hit.transform != _currentEnemyGridTransform)
                    {
                        if (_currentEnemyGridTransform != null)
                        {
                            _currentEnemyGridTransform.GetComponent<Renderer>().material.DOKill();
                            _currentEnemyGridTransform.GetComponent<Renderer>().material.DOFade(1, 0);

                        }
                        _currentEnemyGridTransform = _hit.transform;
                        _currentEnemyGridTransform.GetComponent<Renderer>().material.DOFade(0.2f, 0.5f).SetLoops(-1);
                        //   EventDispatcher.Instance.Dispatch(EventName.OnHoldCharacter, chosenCharacter);
                    }
                }
                else
                {
                    //_currentEnemyGridTransform = null;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                ReleaseCharacter();
                // _currentEnemyGridTransform = null;
            }
        }

        public void PickUpCharacter()
        {
            if (CurrentEnemyGrid == null)
                return;
            if (!CurrentEnemyGrid.IsHaveCharacter)
                return;

            chosenCharacter = CurrentEnemyGrid.GetCurrentCharacter(); ;
            chosenCharacter.motionController.SetBoolPickedUp(true);

            //CurrentFriendlyGrid.currentCharacter.SetNewParentCharacter(CursorControl.instance.cursor.transform, new Vector3(0, 0.2f, 0));
            SetNewParentCharacter(CurrentEnemyGrid.GetCurrentCharacter().transform, CursorControl.Instance.cursor.transform, new Vector3(0, 0.2f, 0));
            CurrentEnemyGrid.SetCurrentCharacter(null);
            originEnemyGrid = CurrentEnemyGrid;
        }

        public void ReleaseCharacter()
        {
            if (chosenCharacter == null)
                return;

            chosenCharacter.motionController.SetBoolPickedUp(false);

            //If current friendly grid empty
            if (CurrentEnemyGrid == null)
            {
                _BackToOriginGrid();
            }
            else
            {
                //if current friendly grid don't have character inside
                if (!CurrentEnemyGrid.IsHaveCharacter)
                {
                    //chosenCharacter.SetNewParentCharacter(CurrentFriendlyGrid.transform, Vector3.zero);
                    SetNewParentCharacter(chosenCharacter.transform, CurrentEnemyGrid.transform, Vector3.zero);
                    CurrentEnemyGrid.SetCurrentCharacter(chosenCharacter);
                }
                else
                {
                    //if current friendly grid's character have different id
                    if (CurrentEnemyGrid.GetCurrentCharacter().data.iD != chosenCharacter.data.iD)
                    {
                        _BackToOriginGrid();
                    }
                    //Current friendly grid have same ID
                    else
                    {
                        //Max level character, can not merge anymore
                        if (CurrentEnemyGrid.GetCurrentCharacter().data.iD == CharacterID.FM10 || CurrentEnemyGrid.GetCurrentCharacter().data.iD == CharacterID.FR10)
                        {
                            _BackToOriginGrid();
                        }
                        else // good to merge
                        {
                            _BackToOriginGrid();
                        }
                    }
                }
            }
            _ResetAndSaveControlData();
            EventDispatcher.Instance.Dispatch(EventName.OnReleaseCharacter, null);




            void _BackToOriginGrid()
            {
                SetNewParentCharacter(chosenCharacter.transform, originEnemyGrid.transform, Vector3.zero);
                originEnemyGrid.SetCurrentCharacter(chosenCharacter);
            }
            void _ResetAndSaveControlData()
            {
                chosenCharacter = null;
                originEnemyGrid = null;
                CharacterDataManager.Instance.SaveFriendlyGrids();
                return;
            }
        }

        public void SetNewParentCharacter(Transform _obj, Transform _parent, Vector3 _offset)
        {
            _obj.parent = _parent;
            _obj.DOKill();
            _obj.DOLocalMove(_offset, 0.3f);
            _obj.DOLocalRotate(new Vector3(0, 180, 0), 0.3f);
        }
    }
}