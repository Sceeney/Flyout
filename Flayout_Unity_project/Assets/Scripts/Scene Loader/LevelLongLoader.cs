using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelLongLoader : SceneLoader
{
    [SerializeField] private ADInfo _adInfo;

    public override Value LevelID => Value.Distance;

    protected override IEnumerator Load_async_scene_menu()
    {
        IsDoneLoading = false;

        AsyncOperation asyncLoad = Level_Jump_Long.LoadAsync(new LevelInfo(new ADViewer(_adInfo)));

        while (!asyncLoad.isDone)
        {
            Bar.value = asyncLoad.progress;
            yield return null;
        }

        IsDoneLoading = true;
    }

    protected override IEnumerator Load_async_scene_menu(LevelInfo levelInfo)
    {
        IsDoneLoading = false;

        AsyncOperation asyncLoad = Level_Jump_Long.LoadAsync(levelInfo);

        while (!asyncLoad.isDone)
        {
            Bar.value = asyncLoad.progress;
            yield return null;
        }

        IsDoneLoading = true;
    }
}
