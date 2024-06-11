using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScene : MonoBehaviour
{
    CanvasGroup cg;
    [SerializeField]
    [Range(1.0f, 5.0f)]
    float fadeOutTime = 30.0f;
    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();

    }

    private void Start()
    {
        Invoke("AppendInputAnykey", 5.5f); // 게임 시작 후 5초 뒤에 실행
        
    }

    public void AppendInputAnykey()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float timeElaspad = 0.0f;
        while (timeElaspad < fadeOutTime)
        {
            timeElaspad += Time.deltaTime;
            cg.alpha = timeElaspad / fadeOutTime;
            yield return null;
        }
        cg.alpha = 1.0f;
    }
}
