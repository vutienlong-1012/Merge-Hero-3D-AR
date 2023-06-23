using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MergeAR.Loading
{
    public class LoadMainScene : MonoBehaviour
    {
        [SerializeField] Slider loadingSlider;
        [SerializeField] Text loadingText;
        [SerializeField] float fakeLoadTime;
        //[SerializeField] bool isShowedFirstAOA = false;
        private void Start()
        {
            //LogManager.LogFirstOpen();
#if UNITY_EDITOR
            fakeLoadTime = 1;
#endif
            StartCoroutine(LoadAsynchronously());
        }
        public IEnumerator LoadAsynchronously()
        {
            AsyncOperation _operation = SceneManager.LoadSceneAsync(1);
            _operation.allowSceneActivation = false;
            float _currentFakeLoadTime = fakeLoadTime;
            while (!_operation.isDone && loadingSlider.value < 1)
            {
                _currentFakeLoadTime -= Time.deltaTime;
                float _progress = 1 - (_currentFakeLoadTime / fakeLoadTime);
                loadingSlider.value = _progress;
                if (loadingText != null)
                    loadingText.text = Mathf.Round(_progress * 100) + "%";
                if (loadingSlider.value >= 0.6)
                {
                    //if (CC_Interface.current.isJustLaunch)
                    //    CC_Interface.current.ShowAppOpenAd();
                }
                yield return null;
            }
            _operation.allowSceneActivation = true;
        }


    }
}