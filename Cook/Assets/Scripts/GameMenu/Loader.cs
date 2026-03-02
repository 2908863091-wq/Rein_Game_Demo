using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        start,
        load,
        game
    }
    private static Scene targetScene;

    public static void Load(Scene scene)
    {
        Time.timeScale = 1;
        targetScene = scene;
        SceneManager.LoadScene((int)Scene.load);
    }
    public static void LoadBack() 
    {
        SceneManager.LoadScene((int)targetScene);
    }
}
