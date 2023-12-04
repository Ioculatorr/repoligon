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

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        operation.allowSceneActivation = false;
        LoadingScreen.SetActive(true);
        float delay = LoadTime;

        while (!operation.isDone && delay > 0)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            delay -= Time.deltaTime;
            LoadingBarFill.fillAmount = 1 - (delay / LoadTime);

            yield return null;
        }
        operation.allowSceneActivation = delay <= 0;
    }
}