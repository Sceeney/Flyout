using UnityEngine;
using UnityEngine.UI;

public class Load_Screen : MonoBehaviour
{
    public int Load_level_Index;
    public GameObject Loading_Screen;
    public Slider bar;
    public SceneLoader loader;

    public void Load()
    {
        Loading_Screen.SetActive(true);
        PlayerPrefs.SetInt("Try's", 1);

        loader.LoadScene(Load_level_Index, bar);
    }
}
