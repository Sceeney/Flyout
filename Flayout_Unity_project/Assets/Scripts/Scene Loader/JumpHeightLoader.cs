using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;

public class JumpHeightLoader : Load_Screen
{
    protected override IEnumerator Load_async_scene_menu()
    {
        IsDoneLoading = false;

        AsyncOperation asyncLoad = Level_Jump_Hight.LoadAsync(new LevelInfo());

        while (!asyncLoad.isDone)
        {
            Bar.value = asyncLoad.progress;
            yield return null;
        }

        IsDoneLoading = true;
    }
}