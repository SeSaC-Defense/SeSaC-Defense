using Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : Singleton<SceneSwitcher>
{
    public void SwitchScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
