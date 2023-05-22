using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load_Screen : MonoBehaviour
{
    public string Load_level;
    public GameObject Loading_Screen;
    public Slider bar;
    bool isDone_UI;
    
    void OnEnable()
    {
        isDone_UI = false;
    }
    void Update()
    {
        if(!isDone_UI){    
            if(Сhoice_Level.Start_Level_1 == true){
                    Load();
                    isDone_UI = true;}}
    }
    public void Load()
    {
        Loading_Screen.SetActive(true);
        PlayerPrefs.SetInt("Try's", 1);
        //SceneManager.LoadScene(Load_level);

        StartCoroutine(Load_async_scene());
    }

    IEnumerator Load_async_scene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Load_level);

        while(!asyncLoad.isDone)
        {            
            bar.value = asyncLoad.progress; 
            yield return null;        
        }
    }
}
