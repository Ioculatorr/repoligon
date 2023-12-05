using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] Image LoadingBarFill;
    [SerializeField] float LoadTime = 5f;

    public static event Action StartedLoading;
    public static event Action<float> ProgressUpdated;
    public static event Action SceneLoaded;

    public FloatVariable loadingProgress;

    public void LoadScene(int sceneId)
    {
        GlobalEvents.GameLoadedFromSave += OnGameLoaded;
        GlobalEvents.GameLoadingProgressed += OnGameProgressed;
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    public void OnGameLoaded()
    {

    }

    public void OnGameProgressed(CustomEventArguments customEventArguments)
    {
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        operation.allowSceneActivation = false;
        LoadingScreen.SetActive(true);
        float delay = LoadTime;
        StartedLoading?.Invoke();
        while (!operation.isDone && delay > 0)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            delay -= Time.deltaTime;
            LoadingBarFill.fillAmount = 1 - (delay / LoadTime);
            ProgressUpdated?.Invoke(1 - (delay / LoadTime));

            yield return null;
        }
        GlobalEvents.OnGameLoadedFromSave();
        GlobalEvents.OnGameLoadingProgressed(new CustomEventArguments { Progress = delay });
        SceneLoaded?.Invoke();
        operation.allowSceneActivation = delay <= 0;
    }
}