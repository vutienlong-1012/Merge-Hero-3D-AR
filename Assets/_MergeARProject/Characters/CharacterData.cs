using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData")]
    [InlineEditor]

    public class CharacterData : ScriptableObject
    {
        [BoxGroup("Info")]
        [LabelWidth(100)]
        public string characterName;

        [BoxGroup("Info")]
        [LabelWidth(100)]
        [TextArea]
        public string description;

        [BoxGroup("Info")]
        [LabelWidth(100)]
        [ShowInInspector]
        public CharacterType Type
        {
            get
            {
                if ((int)iD == 0)
                    return CharacterType.None;
                else
                if ((int)iD > 0 && (int)iD < 21)
                    return CharacterType.Melee;
                else
                    return CharacterType.Ranged;
            }
        }

        [BoxGroup("Info")]
        [LabelWidth(100)]
        public CharacterID iD;
         
        [BoxGroup("Data")]
        [HorizontalGroup("Data/Left", 100)]
        [PreviewField(100)]
        [HideLabel]
        public GameObject model;

        [BoxGroup("Data")]
        [VerticalGroup("Data/Left/Right")]
        [LabelWidth(100)]
        [ProgressBar(0, 100)]
        [GUIColor(0.5f, 1f, 0.5f)]
        public int health = 20;

        [BoxGroup("Data")]
        [VerticalGroup("Data/Left/Right")]
        [LabelWidth(100)]
        [Range(0.5f, 5f)]
        [GUIColor(0.3f, 0.5f, 1f)]
        public float speed = 2f;

        [BoxGroup("Data")]
        [VerticalGroup("Data/Left/Right")]
        [LabelWidth(100)]
        [Range(5, 30)]
        [GUIColor(1f, 1f, 0f)]
        public float attackRange = 10f;

        [BoxGroup("Data")]
        [VerticalGroup("Data/Left/Right")]
        [LabelWidth(100)]
        [Range(1, 10)]
        [GUIColor(0.8f, 0.4f, 0.4f)]
        public int damage = 1;

        [BoxGroup("Reference")]
        [LabelWidth(100)]
        [ShowIf("@Type == CharacterType.Ranged")]
        [PreviewField(100)]
        public GameObject bullet;

        [BoxGroup("Reference")]
        [LabelWidth(100)]
        [InlineEditor]
        public AnimatorOverrideController animatorOverrideController;
    }
}


