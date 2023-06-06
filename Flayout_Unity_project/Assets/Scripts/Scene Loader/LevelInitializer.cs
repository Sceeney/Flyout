using UnityEngine;
using UnityEngine.UI;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _bar;

    public void SetNewLevelLoader(SceneLoader load_screen)
    {
        load_screen.Init(_loadingScreen, _bar);
    }
}
