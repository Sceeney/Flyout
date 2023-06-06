using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;

public class JumpLongLoaderOLD : SceneLoader
{
    public override Value LevelID => Value.Distance;

    protected override IEnumerator Load_async_scene_menu()
    {
        IsDoneLoading = false;

        AsyncOperation asyncLoad = Level_Long_Jump_OLD.LoadAsync(new LevelInfo());

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

        AsyncOperation asyncLoad = Level_Long_Jump_OLD.LoadAsync(levelInfo);

        while (!asyncLoad.isDone)
        {
            Bar.value = asyncLoad.progress;
            yield return null;
        }

        IsDoneLoading = true;
    }
}
