using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingModel
{
    public enum Scene
    {
        SPLASH_SCENE,
        GAME_SCENE
    }
}

public class LoadingController : MonoBehaviour
{
    [Header("Loading UI gameobjects")]
    [SerializeField] Slider progressBar;
    [SerializeField] TMP_Text loadingPercentage;
    [Header("Start UI gameobjects")]
    [SerializeField] CanvasGroup startImage;

    LoadingModel loadingModel;

    void Awake()
    {
        loadingModel = new LoadingModel();
        StartCoroutine(AsyncLoading((int)LoadingModel.Scene.GAME_SCENE));

    }

    void Start()
    {
        StartCoroutine(UIAnimator.INSTANCE.FadeLoopUI(startImage, 0.5f));
    }

    IEnumerator AsyncLoading(int _scene)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_scene);
        asyncOperation.allowSceneActivation = false;

        if (_scene == (int)LoadingModel.Scene.GAME_SCENE)
        {
            while (!asyncOperation.isDone)
            {
                progressBar.value = asyncOperation.progress;
                loadingPercentage.text = (Mathf.Clamp01(asyncOperation.progress / 0.9f)) * 100 + "%";

                // scene is loaded, wait for user input
                if (asyncOperation.progress >= 0.9f)
                {
                    progressBar.gameObject.GetParentObj().SetActive(false);
                    if (Input.anyKeyDown)
                        asyncOperation.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}
