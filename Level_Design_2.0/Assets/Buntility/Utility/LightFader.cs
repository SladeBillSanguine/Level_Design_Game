using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightFader : MonoBehaviour
{
    [SerializeField] float _fadeTime = 3f;

    float _lightOn;
    Light _source;

    string _tweenID;


    private void Awake()
    {
        _source = GetComponent<Light>();
        _lightOn = _source.intensity;
        _tweenID = gameObject.GetInstanceID().ToString();
    }

    private void Start()
    {
        turnLightOff();
    }

    private void OnDestroy()
    {
        DOTween.Kill(_tweenID);
    }

    void turnLightOn()
    {
        //Debug.Log("TunrON");
        DOTween.To(setLight, 0, _lightOn, _fadeTime).OnComplete(turnLightOff).SetId(_tweenID);
    }

    void turnLightOff()
    {
        //Debug.Log("TurnOff");
        DOTween.To(setLight, _lightOn, 0, _fadeTime).OnComplete(turnLightOn).SetId(_tweenID);
    }

    void setLight(float inValue)
    {
        //Debug.Log("SET");
        _source.intensity = inValue;
    }


}
