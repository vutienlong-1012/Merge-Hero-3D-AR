using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    public class Road : MonoBehaviour
    {
        MeshFilter meshFilter;

        [ShowInInspector]
        MeshFilter ThisMeshFilter
        {
            get
            {
                if (meshFilter == null)
                    meshFilter = GetComponent<MeshFilter>();
                return meshFilter;
            }
        }

        MeshRenderer meshRenderer;

        [ShowInInspector]
        MeshRenderer ThisMeshRenderer
        {
            get
            {
                if (meshRenderer == null)
                    meshRenderer = GetComponent<MeshRenderer>();
                return meshRenderer;
            }
        }


        [SerializeField, BoxGroup("Space")] Mesh spaceRoadNormal;
        [SerializeField, BoxGroup("Space")] Mesh spaceRoadPitLeft;
        [SerializeField, BoxGroup("Space")] Mesh spaceRoadPitRight;
        [SerializeField, BoxGroup("Space")] Mesh spaceRoadPit6m;
        [SerializeField, BoxGroup("Space")] Mesh spaceRoadPit10m;
        [SerializeField, BoxGroup("Space")] Material spaceMaterial;

        [SerializeField, BoxGroup("City")] Mesh cityRoadNormal;
        [SerializeField, BoxGroup("City")] Mesh cityRoadPitLeft;
        [SerializeField, BoxGroup("City")] Mesh cityRoadPitRight;
        [SerializeField, BoxGroup("City")] Mesh cityRoadPit6m;
        [SerializeField, BoxGroup("City")] Mesh cityRoadPit10m;
        [SerializeField, BoxGroup("City")] Material cityMaterial;


        [SerializeField] RoadType roadType;

        private void OnEnable()
        {
            EventDispatcher.Instance.AddListener(EventName.OnLoadBackground, SetRoad);
            //if (EnvironmentManager.EnvironmentManager.Instance != null)
            //    SetRoad(EventName.NONE, EnvironmentManager.EnvironmentManager.Instance.CurrentEnvironmentBackground);
        }

        private void OnDisable()
        {
            EventDispatcher.Instance.RemoveListener(EventName.OnLoadBackground, SetRoad);
        }

        private void SetRoad(EventName _key = EventName.NONE, object _data = null)
        {
            switch (roadType)
            {
                case RoadType.Normal:
                    switch ((EnvironmentBackground)_data)
                    {
                        case EnvironmentBackground.Space:
                            _SetMeshMat(spaceRoadNormal, spaceMaterial);
                            break;
                        case EnvironmentBackground.City:
                            _SetMeshMat(cityRoadNormal, cityMaterial);
                            break;
                    }
                    break;
                case RoadType.PitLeft:
                    switch ((EnvironmentBackground)_data)
                    {
                        case EnvironmentBackground.Space:
                            _SetMeshMat(spaceRoadPitLeft, spaceMaterial);
                            break;
                        case EnvironmentBackground.City:
                            _SetMeshMat(cityRoadPitLeft, cityMaterial);
                            break;
                    }
                    break;
                case RoadType.PitRight:
                    switch ((EnvironmentBackground)_data)
                    {
                        case EnvironmentBackground.Space:
                            _SetMeshMat(spaceRoadPitRight, spaceMaterial);
                            break;
                        case EnvironmentBackground.City:
                            _SetMeshMat(cityRoadPitRight, cityMaterial);
                            break;
                    }
                    break;
                case RoadType.Pit6m:
                    switch ((EnvironmentBackground)_data)
                    {
                        case EnvironmentBackground.Space:
                            _SetMeshMat(spaceRoadPit6m, spaceMaterial);
                            break;
                        case EnvironmentBackground.City:
                            _SetMeshMat(cityRoadPit6m, cityMaterial);
                            break;
                    }
                    break;

                case RoadType.Pit10m:
                    switch ((EnvironmentBackground)_data)
                    {
                        case EnvironmentBackground.Space:
                            _SetMeshMat(spaceRoadPit10m, spaceMaterial);
                            break;
                        case EnvironmentBackground.City:
                            _SetMeshMat(cityRoadPit10m, cityMaterial);
                            break;
                    }
                    break;
            }

            void _SetMeshMat(Mesh _mesh, Material _mat)
            {
                ThisMeshFilter.mesh = _mesh;
                ThisMeshRenderer.material = _mat;
            }
        }
    }
}