using BreakInfinity;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VTLTools;

namespace MergeAR.UI
{
    public class CoinInforPopup : PopupBase
    {
        [SerializeField, BoxGroup("Popup Reference")] CoinInforText coinText;
        [SerializeField, BoxGroup("Popup Reference")] Image coinImage;
        [SerializeField, BoxGroup("Popup Reference")] GameObject coinPrefab;

        [SerializeField] AudioClip addCoinAudioClip;

        private void OnEnable()
        {
            EventDispatcher.Instance.AddListener(EventName.OnCoinValueChange, UpdateText);
            UpdateText();
        }

        private void OnDisable()
        {
            EventDispatcher.Instance.RemoveListener(EventName.OnCoinValueChange, UpdateText);
        }

        private void UpdateText(EventName key = EventName.NONE, object data = null)
        {
            coinText.SetText(BigDouble.ToText(StaticVariables.CurrentCoin));
        }


        [Button]
        public void SpawnAndPlusCoins(Transform _spawnArea, BigDouble _plusCoinValue, Action _OnCompleteAction)
        {
            SoundSystem.Instance.PlaySoundOneShot(SoundSystem.Instance.uIAudioSource, addCoinAudioClip);

            Vector3 _spawnPos = _spawnArea.position;
            float _radius = 0.3f;
            int _numCoins = 5;
            int _numCoinsCompleted = 0;

            for (int i = 0; i < _numCoins; i++)
            {
                float _angle = i * Mathf.PI * 2 / _numCoins;
                Vector3 _offset = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0) * _radius;
                Vector3 _targetPos = _spawnPos + _offset;

                GameObject _coin = Instantiate(coinPrefab, _spawnArea);

                float _delay = i * 0.1f;

                _coin.transform.DOMove(_targetPos, 0.3f).OnComplete(() =>
                {
                    _coin.transform.DOMove(coinImage.transform.position, 0.5f).SetDelay(_delay).OnComplete(() =>
                    {
                        StaticVariables.CurrentCoin += _plusCoinValue / _numCoins;
                        _numCoinsCompleted++;

                        if (_numCoinsCompleted == _numCoins)
                        {
                            StartCoroutine(_DelayInvokeOnCompleteAction());
                        }
                        Destroy(_coin);
                    });
                });
            }

            IEnumerator _DelayInvokeOnCompleteAction()
            {
                yield return new WaitForSeconds(0.5f);
                _OnCompleteAction?.Invoke();
            }
        }

    }
}