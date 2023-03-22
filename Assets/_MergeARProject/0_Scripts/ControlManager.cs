using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR
{
    public class ControlManager : Singleton<ControlManager>
    {
        [SerializeField] LayerMask friendlyGridLayerMask;
        [SerializeField] Camera cam;
        [SerializeField, ReadOnly] Character chosenCharacter;

        [ShowInInspector]
        FriendlyGrid CenterFriendlyGrid
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

        [SerializeField, ReadOnly] FriendlyGrid originFriendlyGrid;

        private void Update()
        {
            UpdateCenterGrid();
        }


        Ray _ray;
        RaycastHit _hit;
        Transform _currentFriendlyGridTransform;

        void UpdateCenterGrid()
        {
            _ray = new Ray(cam.transform.position, cam.transform.forward);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, friendlyGridLayerMask))
            {
                _currentFriendlyGridTransform = _hit.transform;
            }
            else
            {
                _currentFriendlyGridTransform = null;
            }
        }

        public void PickUpCharacter()
        {
            if (CenterFriendlyGrid == null)
                return;
            if (CenterFriendlyGrid.currentCharacter == null)
                return;

            chosenCharacter = CenterFriendlyGrid.currentCharacter;
            CenterFriendlyGrid.currentCharacter.SetNewParentCharacter(CursorControl.instance.cursor.transform, new Vector3(0, 0.06f, 0));
            CenterFriendlyGrid.currentCharacter = null;
            originFriendlyGrid = CenterFriendlyGrid;
        }

        public void ReleaseCharacter()
        {
            if (chosenCharacter == null)
                return;

            if (CenterFriendlyGrid != null && CenterFriendlyGrid.currentCharacter == null)
            {
                chosenCharacter.SetNewParentCharacter(CenterFriendlyGrid.transform, Vector3.zero);
                CenterFriendlyGrid.currentCharacter = chosenCharacter;
                chosenCharacter = null;
                originFriendlyGrid = null;
            }
            else
            {
                chosenCharacter.SetNewParentCharacter(originFriendlyGrid.transform, Vector3.zero);
                originFriendlyGrid.currentCharacter = chosenCharacter;
                chosenCharacter = null;
                originFriendlyGrid = null;
            }

        }
    }
}