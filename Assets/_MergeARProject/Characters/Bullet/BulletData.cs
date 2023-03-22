using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeAR
{
    [Serializable]
    [CreateAssetMenu(fileName = "BulletData", menuName = "BulletData")]
    public class BulletData : ScriptableObject
    {
        [BoxGroup("Basic Info")]
        [LabelWidth(100)]
        public string ID;

        [BoxGroup("Game Data")]
        [HorizontalGroup("Game Data/Left", 100)]
        [PreviewField(100)]
        [HideLabel]
        public GameObject model;

        [BoxGroup("Game Data")]
        [VerticalGroup("Game Data/Left/Right")]
        [LabelWidth(100)]
        [Range(20, 100)]
        [GUIColor(0.5f, 1f, 0.5f)]
        public int health = 20;

        [BoxGroup("Game Data")]
        [VerticalGroup("Game Data/Left/Right")]
        [LabelWidth(100)]
        [Range(0.5f, 5f)]
        [GUIColor(0.3f, 0.5f, 1f)]
        public float speed = 2f;

        [BoxGroup("Game Data")]
        [VerticalGroup("Game Data/Left/Right")]
        [LabelWidth(100)]
        [Range(5, 30)]
        [GUIColor(1f, 1f, 0f)]
        public float attackRange = 10f;

        [BoxGroup("Game Data")]
        [VerticalGroup("Game Data/Left/Right")]
        [LabelWidth(100)]
        [Range(5, 30)]
        [GUIColor(1f, 1f, 0f)]
        public float attackSpeed = 10f;

        [BoxGroup("Game Data")]
        [VerticalGroup("Game Data/Left/Right")]
        [LabelWidth(100)]
        [Range(1, 10)]
        [GUIColor(0.8f, 0.4f, 0.4f)]
        public int damage = 1;


    }
}
