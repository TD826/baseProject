using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasFade : MonoBehaviour
{
    [Tooltip("フェードイン用マテリアル"), SerializeField] private Material _fadeIn;
    [Tooltip("フェードアウト用マテリアル"), SerializeField] private Material _fadeOut;
    [NonSerialized] public float _alphaRange;
    private Image image;
    [Tooltip("マテリアル用のテクスチャ(Sprite形式)"), SerializeField] private Texture _matTex;
    [SerializeField] private float _fadeTime;

    // タイムスケールの有無
    private bool _timeScale;
    //フェード終了後のコールバック
    private event Action _fadeFinish = null;

    [SerializeField] private GameObject _imageObject;

    // フェードの状態
    public enum FadeState
    {
        None,
        FadeOut,
        FadeIn,
    }
    private FadeState _fadeState = FadeState.None;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(_fadeState == FadeState.None)
        {
            return;
        }

        float fadeSpeed = 1f / _fadeTime;
        switch (_fadeState)
        {
            case FadeState.FadeOut:
                Debug.Log(_alphaRange);
                image.material　=　_fadeOut;
                image.material.SetTexture("_MainTex", _matTex);
                if(_timeScale)
                {
                    fadeSpeed *= Time.deltaTime;
                }
                else
                {
                    fadeSpeed *= Time.unscaledDeltaTime;
                }
                _alphaRange += fadeSpeed;
                image.material.SetFloat("_Alpha",_alphaRange);

                // 終了判定
                if(_alphaRange >= 1)
                {
                    _fadeState = FadeState.FadeIn;
                    image.color = new Color(1, 1, 1, 1);    // 透明度１に
                    if(_fadeFinish != null)
                    {
                        _fadeFinish();
                    }
                    return;
                }
                break;
            case FadeState.FadeIn:
                Debug.Log(_alphaRange);
                image.material = _fadeIn;
                image.material.SetTexture("_MainTex", _matTex);
                if(_timeScale)
                {
                    fadeSpeed *= Time.deltaTime;
                }
                else
                {
                    fadeSpeed *= Time.unscaledDeltaTime;
                }
                _alphaRange -= fadeSpeed;
                image.material.SetFloat("_Alpha",_alphaRange);
                if(_alphaRange <= 0)
                {   // 終了
                    _fadeState = FadeState.None;
                    if(_fadeFinish != null)
                    {
                        _fadeFinish();
                    }
                    this.enabled = false;
                    return;
                }
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// フェードスタート関数
    /// </summary>
    /// <param name="isFadeOut">フェードアウトかフェードインか</param>
    /// <param name="fadeTime">フェードの秒数</param>
    /// <param name="timeScale">タイムスケールを使うか</param>
    /// <param name="fadeFinished">フェード後のアクション</param>
    public void FadeStart(bool isFadeOut,float fadeTime,bool timeScale = true,Action fadeFinished = null)
    {
        this.enabled = true;

        _timeScale = timeScale;
        _fadeFinish = fadeFinished;

        _alphaRange = isFadeOut ? 0 : 1;
        _fadeState = isFadeOut ? FadeState.FadeOut : FadeState.FadeIn;
        image = GetComponentInChildren<Image>();
        _fadeTime = fadeTime;
    }
}
