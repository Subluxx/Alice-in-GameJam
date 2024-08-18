using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[ExecuteInEditMode]

public class TextAnimator : MonoBehaviour
{

    [SerializeField]
    private string _message;

    [SerializeField]
    float _stringAnimationDuration;

    [SerializeField]
    private TextMeshProUGUI _animatedText;

    [SerializeField]
    private AnimationCurve _sizeCurve;

    [SerializeField]
    private float _sizeScale;


    [SerializeField]
    [Range(0.0001f, 1)]
    private float _charAnimationDuration;


    [SerializeField]
    [Range(0, 1)]
    private float _editorTValue;


    private float _timeElapsed;

    private void Start()
    {
        StartCoroutine(routine: RunAnimation(waitForSeconds: 3));
    }

    private void Update()
    {
        EvaluateRichText(_editorTValue);
    }


    IEnumerator RunAnimation(float waitForSeconds)
    {
        yield return new WaitForSeconds(waitForSeconds);
        float t = 0;
        while (t <= 1f)
        {
            EvaluateRichText(t);
            t = _timeElapsed / _stringAnimationDuration;
            _timeElapsed += Time.deltaTime;

            yield return null;
        }
    }

    void EvaluateRichText(float t)
    {
        _animatedText.text = "";
        for (int i = 0; i < _message.Length; i++)
        {
            _animatedText.text += EvaluateCharRichText(_message[i], _message.Length, cPosition: i, t);
        }
    }

    private string EvaluateCharRichText(char c, int sLength, int cPosition, float t)
    {
        float startPoint = ((1 - _charAnimationDuration) / (sLength - 1)) * cPosition;

        float endPoint = startPoint + _charAnimationDuration;

        float subT = t.Map(fromLow: startPoint, fromHigh: endPoint, toLow: 0, toHigh: 1);

        string sizeStart = $"<size={_sizeCurve.Evaluate(subT) * _sizeScale}%>";
        string sizeEnd = "</size>";

        return sizeStart + c + sizeEnd;
    }
}


public static class Extensions
{
    public static float Map(this float value, float fromLow, float fromHigh, float toLow, float toHigh)
    {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }
}