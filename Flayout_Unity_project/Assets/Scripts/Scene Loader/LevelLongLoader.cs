using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLongLoader : SceneLoader
{
    public override Value LevelID => Value.Distance;

    protected override IEnumerator Load_async_scene_menu()
    {
        IsDoneLoading = false;

        AsyncOperation asyncLoad = Level_Jump_Long.LoadAsync(new LevelInfo());

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
