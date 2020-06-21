using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    //继续游戏
    public void OnButtonContinue()
    {
        SceneManager.LoadScene(0);
    }

    //退出游戏
    public void OnButtonQuit()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
