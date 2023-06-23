//using Sirenix.OdinInspector;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARSubsystems;
//using VTLTools;

//namespace MergeAR
//{
//    public class CursorControl : Singleton<CursorControl>
//    {
//        public Cursor cursor;
//        [SerializeField] Camera cam;

//        Pose placementPose;
//        [ShowInInspector, ReadOnly]
//        public Pose PlacementPose
//        {
//            get => placementPose;
//            private set => placementPose = value;
//        }

//        bool isCursorPlacementValid;
//        [ShowInInspector, ReadOnly]
//        public bool IsCursorPlacementValid
//        {
//            get => isCursorPlacementValid;
//            private set => isCursorPlacementValid = value;
//        }


//        private void Update()
//        {
//            UpdatePlacementPoseWithPhysicRaycast();
//            UpdatePlacementCursor();
//        }

//        private void UpdatePlacementCursor()
//        {
//            cursor.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
//        }

//        #region Physic Raycast
//        Ray _ray;
//        RaycastHit _hit;
//        Vector3 _hitPosition;
//        Quaternion _hitRotation;
//        public LayerMask layerMask;
//        void UpdatePlacementPoseWithPhysicRaycast()
//        {
//            _ray = cam.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, layerMask))
//            {
//                IsCursorPlacementValid = true;
//                _hitPosition = _hit.point;
//                //_hitRotation = Quaternion.FromToRotation(Vector3.up, _hit.normal);
//                _hitRotation = _hit.collider.gameObject.transform.rotation;
//                PlacementPose = new Pose(_hitPosition, _hitRotation);
//            }
//        }
//        #endregion
//    }
//}

using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using VTLTools;

namespace MergeAR
{
    public class CursorControl : Singleton<CursorControl>
    {
        public Cursor cursor;
        [SerializeField] ARRaycastManager aRRaycastManager;
        [SerializeField] Camera cam;
        [ReadOnly] public bool isARRaycast;

        Pose placementPose;
        [ShowInInspector, ReadOnly]
        public Pose PlacementPose
        {
            get => placementPose;
            private set => placementPose = value;
        }

        bool isCursorPlacementValid;
        [ShowInInspector, ReadOnly]
        public bool IsCursorPlacementValid
        {
            get => isCursorPlacementValid;
            private set => isCursorPlacementValid = value;
        }


        private void Update()
        {
            if (isARRaycast)
                UpdatePlacementPoseWithARRaycast();
            else
                UpdatePlacementPoseWithPhysicRaycast();
            UpdatePlacementCursor();
        }

        private void UpdatePlacementCursor()
        {
            if (IsCursorPlacementValid)
                cursor.gameObject.SetActive(true);
            else
                cursor.gameObject.SetActive(false);
            cursor.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }

        #region AR Raycast
        Vector3 _screenCenterValue = new(0.5f, 0.5f);
        List<ARRaycastHit> _hits = new();
        Vector3 _screenCenter;
        void UpdatePlacementPoseWithARRaycast()
        {
            _screenCenter = cam.ViewportToScreenPoint(_screenCenterValue);
            aRRaycastManager.Raycast(_screenCenter, _hits, TrackableType.Planes);

            IsCursorPlacementValid = _hits.Count > 0;
            if (IsCursorPlacementValid)
            {
                PlacementPose = _hits[0].pose;
            }
        }
        #endregion

        #region Physic Raycast
        Ray _ray;
        RaycastHit _hit;
        Vector3 _hitPosition;
        Quaternion _hitRotation;
        public LayerMask layerMask;
        void UpdatePlacementPoseWithPhysicRaycast()
        {
            _ray = new Ray(cam.transform.position, cam.transform.forward);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, layerMask))
            {
                IsCursorPlacementValid = true;
                _hitPosition = _hit.point;
                //_hitRotation = Quaternion.FromToRotation(Vector3.up, _hit.normal);
                _hitRotation = _hit.collider.gameObject.transform.rotation;
                PlacementPose = new Pose(_hitPosition, _hitRotation);
            }
            else
            {
                IsCursorPlacementValid = false;
            }
        }
        #endregion
    }
}