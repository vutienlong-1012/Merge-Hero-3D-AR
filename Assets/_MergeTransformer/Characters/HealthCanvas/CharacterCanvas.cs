using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MergeAR
{
    public class CharacterCanvas : MonoBehaviour
    {
        Transform camPos;

        [SerializeField] Character character;
        [SerializeField] HealthBar healthBar;
        private void Start()
        {
            camPos = Camera.main.transform;
            healthBar.Init(character.data.startHealth);
        }

        void LateUpdate()
        {
            LookAtCamera();
        }

        Vector3 _temp;
        void LookAtCamera()
        {
            _temp.x = this.transform.position.x;
            _temp.y = camPos.position.y;
            _temp.z = camPos.position.z;
            transform.LookAt(_temp);
        }

        public void UpdateHealthSlider(float _currentHealth)
        {
            healthBar.UpdateSlider(_currentHealth);
        }
    }
}