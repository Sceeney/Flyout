using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private Slider _bar;
    private Coroutine _loaderCoroutine;
    private bool _isDoneLoading;

    private void OnEnable()
    {
        if (_loaderCoroutine != null)
            StopCoroutine(_loaderCoroutine);
    }

    public void LoadScene(int sceneIndex, Slider bar)
    {
        _bar = bar;

        if (!_isDoneLoading && _loaderCoroutine != null)
        {
            return;
        }

        if (_loaderCoroutine != null)
            StopCoroutine(_loaderCoroutine);

        _loaderCoroutine = StartCoroutine(Load_async_scene_menu(sceneIndex));
    }

    private IEnumerator Load_async_scene_menu(int sceneIndex)
    {
        _isDoneLoading = false;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            _bar.value = asyncLoad.progress;
            yield return null;
        }

        _isDoneLoading = true;
    }
}
