using Sirenix.OdinInspector;
using UnityEngine;
using VTLTools;

namespace MergeAR
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData")]
    [InlineEditor]

    public class CharacterData : ScriptableObject
    {
        [BoxGroup("Info")]
        [VerticalGroup("Info/1/2", 100)]
        [LabelWidth(100)]
        public string characterName;

        [BoxGroup("Info")]
        [HorizontalGroup("Info/1", 100)]
        [PreviewField(100)]
        [HideLabel]
        public Sprite avatar;

        [BoxGroup("Info")]
        [VerticalGroup("Info/1/2", 100)]
        [LabelWidth(100)]
        [TextArea]
        public string description;

        [BoxGroup("Info")]
        [VerticalGroup("Info/1/2", 100)]
        [LabelWidth(100)]
        [ShowInInspector]
        public CharacterType Type
        {
            get
            {
                return Helpers.DecideCharacterType(iD);
            }
        }

        [BoxGroup("Info")]
        [VerticalGroup("Info/1/2", 100)]
        [LabelWidth(100)]
        [ShowInInspector]
        public CharacterFaction Faction
        {
            get
            {
                if (iD == 0)
                    return CharacterFaction.None;
                else
                if ((iD >= CharacterID.EM1 && iD <= CharacterID.EM10) || iD >= CharacterID.ER1 && iD <= CharacterID.ER10)
                    return CharacterFaction.Enemy;
                else
                    return CharacterFaction.Friendly;
            }
        }

        [BoxGroup("Info")]
        [VerticalGroup("Info/1/2", 100)]
        [LabelWidth(100)]
        public CharacterID iD;

        [BoxGroup("Data")]
        [HorizontalGroup("Data/0", 100)]
        [PreviewField(100)]
        [HideLabel]
        public Mesh mesh;

        [BoxGroup("Data")]
        [HorizontalGroup("Data/1", 100)]
        [PreviewField(100)]
        [HideLabel]
        public Material material;

        [BoxGroup("Data")]
        [VerticalGroup("Data/0/0")]
        [LabelWidth(100)]
        [GUIColor(0.5f, 1f, 0.5f)]
        public float startHealth;

        [BoxGroup("Data")]
        [VerticalGroup("Data/0/0")]
        [LabelWidth(100)]
        [GUIColor(0.3f, 0.5f, 1f)]
        public float speed = 2f;

        [BoxGroup("Data")]
        [VerticalGroup("Data/0/0")]
        [LabelWidth(100)]
        [GUIColor(1f, 1f, 0f)]
        public float attackRange;

        [BoxGroup("Data")]
        [VerticalGroup("Data/0/0")]
        [LabelWidth(100)]
        [GUIColor(0.8f, 0.4f, 0.4f)]
        public int damage;

        [BoxGroup("Data")]
        [VerticalGroup("Data/0/0")]
        [LabelWidth(100)]
        [GUIColor(0.2f, 0.5f, 0.7f)]
        public int scaleLevel;

        [BoxGroup("Data")]
        [VerticalGroup("Data/0/0")]
        [LabelWidth(100)]
        public AnimatorOverrideController animatorOverrideController;

        [BoxGroup("Data")]
        [VerticalGroup("Data/0/0")]
        [LabelWidth(100)]
        [ShowInInspector]
        [GUIColor(0.8f, 0.4f, 0.1f)]
        public bool IsUnlocked
        {
            get
            {
                if (iD == CharacterID.FM0 || iD == CharacterID.FM1 || iD == CharacterID.FR1)
                    return VTLPlayerPrefs.GetBool(StringsSafeAccess.PREF_KEY_ISUNLOCKED_CHAR_ + iD, true);
                else
                    return VTLPlayerPrefs.GetBool(StringsSafeAccess.PREF_KEY_ISUNLOCKED_CHAR_ + iD, false);
            }
            set => VTLPlayerPrefs.SetBool(StringsSafeAccess.PREF_KEY_ISUNLOCKED_CHAR_ + iD, value);
        }

        [BoxGroup("Reference")]
        [LabelWidth(100)]
        [ShowIf("@Type == CharacterType.EnemyRanged || Type == CharacterType.FriendlyRanged")]
        public Bullet bullet;

        [BoxGroup("Reference")]
        [LabelWidth(100)]
        [ShowIf("@Type == CharacterType.EnemyMelee || Type == CharacterType.FriendlyMelee")]
        public Effect meleeAttackEffect;

        [BoxGroup("Reference")]
        [LabelWidth(100)]
        [ShowIf("@Type == CharacterType.EnemyMelee || Type == CharacterType.FriendlyMelee")]
        public Effect meleeImpactEffect;




        [BoxGroup("Weapon")]
        [HorizontalGroup("Weapon/1", 100)]
        [PreviewField(100)]
        [HideLabel]
        public GameObject leftWeapon;

        [BoxGroup("Weapon")]
        [VerticalGroup("Weapon/1/1", 100)]
        [PreviewField(100)]
        [HideLabel]
        public GameObject rightWeapon;
    }
}


