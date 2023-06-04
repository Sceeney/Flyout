using UnityEngine;
using UnityEngine.UI;

public class LevelSelecter : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _bar;

    public void SetNewLevelLoader(Load_Screen load_screen)
    {
        load_screen.Init(_loadingScreen, _bar);
    }
}
