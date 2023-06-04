using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Load_Screen : MonoBehaviour
{
    protected Slider Bar;
    protected bool IsDoneLoading;
    
    private GameObject _loading_Screen;
    private Coroutine _loaderCoroutine;

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

    protected abstract IEnumerator Load_async_scene_menu();
}
