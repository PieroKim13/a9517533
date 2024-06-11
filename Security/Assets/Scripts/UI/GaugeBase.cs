using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBase: MonoBehaviour
{
    public Color color = Color.white;
    
    protected Slider slider;
    protected float maxValue;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        Transform temp;

        temp = transform.GetChild(0);
        Image backgroundImage = temp.GetComponent<Image>();
        Color backgroundColor = new Color(color.r, color.g, color.b, color.a * 0.75f);
        backgroundImage.color = backgroundColor;

        temp = transform.GetChild(1);
        Image fillImage = temp.GetComponentInChildren<Image>();
        fillImage.color = color;
    }

    /// <summary>
    /// 스테미나 값이 바뀌었을 때
    /// </summary>
    /// <param name="ratio">비율</param>
    protected void OnValueChange(float ratio)
    {
        ratio = Mathf.Clamp01(ratio);               // ratio를 0~1로 변경
        slider.value = ratio;                       // 슬라이더 조정
    }
}
