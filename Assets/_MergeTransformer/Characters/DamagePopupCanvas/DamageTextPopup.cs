using UnityEngine;
using UnityEngine.UI;
using VTLTools;

public class DamageTextPopup : MonoBehaviour
{
    [SerializeField] Text damageNumberText;

    public void SetDamageText(float _num, CharacterFaction _faction)
    {
        damageNumberText.text = _num.ToString();
        if (_faction == CharacterFaction.Enemy)
            damageNumberText.color = Color.red;
        else
            damageNumberText.color = Color.green;
    }


    //Attach this in animation clip event
    public void SelfRecycle()
    {
        ObjectPool.Recycle(this.gameObject);
    }
}
