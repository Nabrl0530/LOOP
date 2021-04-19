using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public void ChangeSceneBtn()
    {
        switch(this.gameObject.name)
        {
            case "StartBtn":
                SceneManager.LoadScene("ChoiceStage");
                break;

            case "OptionBtn":
                //OptionWindow
                break;

            case "EndBtn":
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
                break;

        }
    }
 
}
