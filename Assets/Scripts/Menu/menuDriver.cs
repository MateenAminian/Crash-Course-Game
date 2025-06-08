using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuDriver : MonoBehaviour
{

    private string asynchLoadedScene;
    private static AsyncOperation _asyncLoad;
    //Use this before change scene where possible - this will load a scene in the background - perfect for preloading large scenes
    //The load function used in change scene will load the target scene as fast as possible - freezing everything else if need be
    //Also Yes I know this is super messy but its the only way I found to make this work
    public void load(string sceneName)
    {
        asynchLoadedScene = sceneName;
        loadHelper1(sceneName);
    }
    private static void loadHelper1(string sceneName)
    {
        GameObject obj = new GameObject();
        obj.AddComponent<menuDriver>().StartCoroutine(LoadHelper2(obj, sceneName));
    }

    private static IEnumerator LoadHelper2(GameObject obj, string sceneName)
    {
        _asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        _asyncLoad.allowSceneActivation = false;
        // Wait until the asynchronous scene fully loads
        while (!_asyncLoad.isDone)
        {
            if (_asyncLoad.progress < 0.9f)
            {
                Debug.Log(_asyncLoad.progress);
            }
            yield return null;

        }
        GameObject.Destroy(obj);
    }

    //Use to reduce memory use
    //likely only useful if we have multiple levels
    public static void unload(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void changeScene(string sceneName)
    {
        if(sceneName == asynchLoadedScene)
        {
            _asyncLoad.allowSceneActivation = true;
        }
        else if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }
    }

    public static void exit()
    {
        Application.Quit();
    }

}

