using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class SceneLoader : MonoBehaviour
{
    protected Slider Bar;
    protected bool IsDoneLoading;
    
    private GameObject _loading_Screen;
    private Coroutine _loaderCoroutine;

    public virtual Value LevelID => throw new NotImplementedException();

    public void Init(GameObject loading_Screen, Slider bar)
    {
        _loading_Screen = loading_Screen;
        Bar = bar;
    }

    public void Load()
    {
        _loading_Screen.SetActive(true);

        LoadScene();
    }

    public void Load(LevelInfo levelInfo)
    {
        _loading_Screen.SetActive(true);

        LoadScene(levelInfo);
    }

    private void LoadScene()
    {
        if (!IsDoneLoading && _loaderCoroutine != null)
        {
            return;
        }

        if (_loaderCoroutine != null)
            StopCoroutine(_loaderCoroutine);

        _loaderCoroutine = StartCoroutine(Load_async_scene_menu());
    }

    private void LoadScene(LevelInfo levelInfo)
    {
        if (!IsDoneLoading && _loaderCoroutine != null)
        {
            return;
        }

        if (_loaderCoroutine != null)
            StopCoroutine(_loaderCoroutine);

        _loaderCoroutine = StartCoroutine(Load_async_scene_menu(levelInfo));
    }

    protected abstract IEnumerator Load_async_scene_menu();
    protected virtual IEnumerator Load_async_scene_menu(LevelInfo levelInfo)
    {
        yield return null;
        throw new NotImplementedException();
    }
}
