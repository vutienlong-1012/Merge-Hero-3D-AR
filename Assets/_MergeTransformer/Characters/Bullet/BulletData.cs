using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace MergeAR
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "BulletData", menuName = "BulletData")]
    public class BulletData : ScriptableObject
    {
        public Effect spawnFx;

        public GameObject model;

        public Effect impactFx;

        public float speed;
    }
}
