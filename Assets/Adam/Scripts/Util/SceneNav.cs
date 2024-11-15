using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

[Serializable]
public class SceneNav : MonoBehaviour
{
    public enum Scenes
    {
        MainMenu,
        GameScene
    }

    public enum UICanvas
    {
        MainMenuCanvas,
        InfoCanvas,
        SettingsCanvas
    }

    public float fadeTime = 1.0f;
    public GameObject MainMenuCanvas;
    public GameObject InfoCanvas;
    public GameObject SettingsCanvas;
    public Dictionary<UICanvas, GameObject> canvasDict;
    public Dictionary<GameObject, UICanvas> CanvasGameObjDict;
    public GameObject currentCanvas;

    #region UNITY METHODS

    public void Start()
    {
        canvasDict = new Dictionary<UICanvas, GameObject>
        {
            { UICanvas.MainMenuCanvas, MainMenuCanvas },
            { UICanvas.InfoCanvas, InfoCanvas },
            { UICanvas.SettingsCanvas, SettingsCanvas },
        };

        CanvasGameObjDict = new Dictionary<GameObject, UICanvas>
        {
            { MainMenuCanvas, UICanvas.MainMenuCanvas },
            { InfoCanvas, UICanvas.InfoCanvas },
            { SettingsCanvas, UICanvas.SettingsCanvas },
        };

        foreach (var item in canvasDict)
        {
            item.Value.gameObject.SetActive(false);
        }

        MainMenuCanvas.SetActive(true);
        MainMenuCanvas.GetComponent<CanvasFade>().setAlpha(1);
        currentCanvas = MainMenuCanvas;
    }

    public void loadMainMenuCanvas()
    {
        if (currentCanvas == MainMenuCanvas)
        {
            return;
        }
        if (currentCanvas != null)
        {
            //currentCanvas.GetComponent<CanvasFade>().FadeOutCanvas(fadeTime);

            StartCoroutine(FadeOutCanvas(currentCanvas));
        }

        currentCanvas = MainMenuCanvas;
        //currentCanvas.SetActive(true);
        //MainMenuCanvas.GetComponent<CanvasFade>().FadeInCanvas(fadeTime);
        StartCoroutine(FadeInCanvas(MainMenuCanvas));
    }

    public void loadInfoCanvas()
    {
        if (currentCanvas == InfoCanvas)
        {
            return;
        }
        if (currentCanvas != null)
        {
            //currentCanvas.GetComponent<CanvasFade>().FadeOutCanvas(fadeTime);
            StartCoroutine(FadeOutCanvas(currentCanvas));
        }

        currentCanvas = InfoCanvas;
        //currentCanvas.SetActive(true);
        //GameModeSelectCanvas.GetComponent<CanvasFade>().FadeInCanvas(fadeTime);
        StartCoroutine(FadeInCanvas(InfoCanvas));
    }

    public void loadSettingsCanvas()
    {
        if (currentCanvas == SettingsCanvas)
        {
            return;
        }
        if (currentCanvas != null)
        {
            currentCanvas.GetComponent<CanvasFade>().FadeOutCanvas(fadeTime);
            StartCoroutine(FadeOutCanvas(currentCanvas));
        }

        currentCanvas = SettingsCanvas;
        //currentCanvas.SetActive(true);
        //SinglePlayerCanvas.GetComponent<CanvasFade>().FadeInCanvas(fadeTime);
        StartCoroutine(FadeInCanvas(SettingsCanvas));
    }

    public void LoadCanvas(UICanvas canvas)
    {
        foreach (var item in canvasDict)
        {
            //item.Value.gameObject.SetActive(false);
            if (item.Value.gameObject.activeSelf & item.Key != canvas)
            {
                StartCoroutine(FadeOutCanvas(item.Value));
            }
        }
        StartCoroutine(FadeInCanvas(canvasDict[canvas]));
        //canvasDict[canvas].gameObject.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitApp()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.ExitPlaymode();
    }

    #endregion UNITY METHODS

    #region METHODS

    public IEnumerator FadeInCanvas(GameObject canvas)
    {
        //Debug.Log("Fading in canvas");
        yield return new WaitForSeconds(fadeTime);
        canvas.SetActive(true);
        //var img = canvas.GetComponent<UnityEngine.UI.Image>();
        canvas.GetComponent<CanvasFade>().FadeInCanvas(fadeTime);
        /*CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.alpha = Mathf.Lerp(0, 1, fadeTime);*/

        //yield return new WaitForSeconds(1);
    }

    public IEnumerator FadeOutCanvas(GameObject canvas)
    {
        //var img = canvas.GetComponent<UnityEngine.UI.Image>();
        //img.CrossFadeAlpha(1, fadeTime, true);
        canvas.GetComponent<CanvasFade>().FadeOutCanvas(fadeTime);
        yield return new WaitForSeconds(fadeTime);
        canvas.SetActive(false);
    }

    #endregion METHODS
}