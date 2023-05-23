using UnityEngine;
using UnityEngine.UI;

public class Load_Screen : MonoBehaviour
{
    public int Load_level_index;
    public GameObject Loading_Screen;
    public Slider bar;
    public SceneLoader loader;

    public void Load()
    {
        Loading_Screen.SetActive(true);
        PlayerPrefs.SetInt("Try's", 1);
        //SceneManager.LoadScene(Load_level);

        loader.LoadScene(1, bar);
    }
}
