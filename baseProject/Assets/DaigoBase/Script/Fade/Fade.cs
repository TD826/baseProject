using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 現在は選択したシーンに移行しないバグがでている
/// </summary>
public class Fade : MonoBehaviour
{
    [SerializeField] private CanvasFade canvasFade;
    private SceneObject nextSceneObject;   // Changeでセットされる時用の変数

    [NonSerialized] public bool isPlay = false;    // プレイ状態に切り替わったとき用のフラグ(参照用)

    // フェードアウト、イン後のコールバック関数
    public event Action FadeOutFinished  = delegate{};

    public event Action FadeInFinished  = delegate{};

    public const float FADE_TIME = 0.5f;    // フェードの時間
    private float fadeTime = FADE_TIME;



    private static Fade fadeInstance = null;
    public static Fade instance
    {
        get
        {
            if(fadeInstance == null)
            {
                fadeInstance = FindObjectOfType<Fade>();
                if(fadeInstance == null)
                {
                    var go = new GameObject("Fade");
                    fadeInstance = go.AddComponent<Fade>();
                    DontDestroyOnLoad(fadeInstance);
                }
                
            }
            return fadeInstance;
        }
    }
    private void Reset()
    {
        if(canvasFade == null)
        {
            CreateFadeCanvas();
        }
    }

    private void Awake()
    {
        if(fadeInstance == null)
        {
            fadeInstance = this as Fade;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    private void CreateFadeCanvas()
    {
        gameObject.name = "Fade";
        //フェード用のキャンバス作成(new だとInstanceが生成されない)
        Transform transformFadeCanvas = Instantiate(transform,transform.position,Quaternion.identity,transform);
        GameObject fadeCanvas = transformFadeCanvas.gameObject;
        Debug.Log(fadeCanvas);
        fadeCanvas.SetActive(false);
    
        Canvas canvas = fadeCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.sortingOrder = 999;
    
        fadeCanvas.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        //fadeCanvas.AddComponent<GraphicRaycaster>();
        canvasFade = fadeCanvas.AddComponent<CanvasFade>();
        canvasFade._alphaRange = 0; 
    
        //フェード用の画像作成
        GameObject imageObject = new GameObject("FadeImage");
        imageObject.transform.SetParent(fadeCanvas.transform, false);
        imageObject.AddComponent<Image>().color = Color.white;
        imageObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2000, 2000);
    }
    
    public void FadeChange(SceneObject nextScene,float time = FADE_TIME)
    {
        Debug.Log("Fade開始");
        if(isPlay)
        {   
            Debug.Log("フェード中");
            return;
        }
        nextSceneObject = nextScene;
        fadeTime = time;

        canvasFade.gameObject.SetActive(true);
        canvasFade.FadeStart(isFadeOut:true, fadeTime:fadeTime, fadeFinished:OnFadeOutFinished);
    }

    private void OnFadeOutFinished()
    {
        FadeOutFinished();

        // シーン読み込み
        SceneManager.LoadScene(nextSceneObject);
        
        canvasFade.gameObject.SetActive(true);
        //Debug.Log(canvasFade.gameObject.active);
        canvasFade._alphaRange = 1;
        canvasFade.FadeStart(isFadeOut:false,fadeTime:fadeTime,fadeFinished:OnFadeInFinished);

    }

    private void OnFadeInFinished()
    {
        canvasFade.gameObject.SetActive(false);
        FadeInFinished();
    }

}
