using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;

public class MainSceneLoader : Load_Screen
{
    protected override IEnumerator Load_async_scene_menu()
    {
        IsDoneLoading = false;

        AsyncOperation asyncLoad = Main_Menu.LoadAsync();

        while (!asyncLoad.isDone)
        {
            Bar.value = asyncLoad.progress;
            yield return null;
        }

        IsDoneLoading = true;
    }
}
