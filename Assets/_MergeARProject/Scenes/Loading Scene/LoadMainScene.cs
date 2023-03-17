using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMainScene : MonoBehaviour
{
    [SerializeField] Slider loadingSlider;
    [SerializeField] Text loadingText;
    [SerializeField] float fakeLoadTime;
    //[SerializeField] bool isShowedFirstAOA = false;
    private void Start()
    {
        //#if UNITY_EDITOR
        //        fakeLoadTime = 3;
        //#endif
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
            float _progess = 1 - (_currentFakeLoadTime / fakeLoadTime);
            loadingSlider.value = _progess;
            if (loadingText != null)
                loadingText.text = Mathf.Round(_progess * 100) + "%";
            if (loadingSlider.value >= 0.6)
            {
                //if (!VariableSystem.isShowAds && !CC_Interface.current.isShowedFirstAOA)
                //{
                //    CC_Interface.current.ShowAppOpenAd();
                //    CC_Interface.current.isShowedFirstAOA = true;
                //}
            }
            yield return null;
        }
        _operation.allowSceneActivation = true;
    }


}
