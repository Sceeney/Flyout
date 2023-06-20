using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;

public class JumpHeightLoader : SceneLoader
{
    [SerializeField] private ADInfo _adInfo;

    public override Value LevelID => Value.Height;

    protected override IEnumerator Load_async_scene_menu()
    {
        IsDoneLoading = false;

        AsyncOperation asyncLoad = Level_Jump_Hight.LoadAsync(new LevelInfo(new ADViewer(_adInfo)));

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

        AsyncOperation asyncLoad = Level_Jump_Hight.LoadAsync(levelInfo);

        while (!asyncLoad.isDone)
        {
            Bar.value = asyncLoad.progress;
            yield return null;
        }

        IsDoneLoading = true;
    }
}
