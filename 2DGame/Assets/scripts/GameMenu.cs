using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void OnButtonStart()
    {
        SceneManager.LoadScene(1);
    }
    //新增---by lee
    public void OnButtonNewer()
    {
        Moves.moveLeft = true;
       (Moves.count)++;
    }
    //新增 --by lee
    public void OnButtonNewerReturn()
    {
        //SceneManager.LoadScene(0);
        Moves.moveLeft = false;
        Moves.count = 0;
    }
    public void OnButtonLeft()
    {
        if(Moves.count == 1);
        else{
            (Moves.count)--;
            Moves.moveLeft = true;
        }
    }
    public void OnButtonRight()
    {
        if(Moves.count == 8);
        else{
            (Moves.count)++;
            Moves.moveLeft = true;
        }
    }

    public void OnButtonSelect()
    {
        ButtonMoves.moveUp = true;
    }

    public void OnButtonReturn()
    {
        ButtonMoves.moveUp = false;
    }

    public void OnButtonLevel1()
    {
        SceneManager.LoadScene(4);
    }

    public void OnButtonLevel2()
    {
        SceneManager.LoadScene(5);
    }
    public void OnButtonLevel3()
    {
        SceneManager.LoadScene(6);
    }
    public void OnButtonLevel4()
    {
        SceneManager.LoadScene(7);
    }
    public void OnButtonQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
