using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimator : MonoBehaviour
{
    public static UIAnimator INSTANCE = null;
    void Awake()
    {
        // singleton
        if (!INSTANCE)
            INSTANCE = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ScaleUI(RectTransform _rectTransform, Vector3 _startScale, Vector3 _endScale, float _speed)
    {
        _rectTransform.localScale = _startScale;
        while (_rectTransform.localScale != _endScale)
        {
            float x = Mathf.MoveTowards(_rectTransform.localScale.x, _endScale.x, _speed * Time.deltaTime);
            float y= Mathf.MoveTowards(_rectTransform.localScale.y, _endScale.y, _speed * Time.deltaTime);
            _rectTransform.localScale = new Vector3(x, y, 1);
            yield return null;
        }
    }

    public IEnumerator FadeUI(CanvasGroup canvasGroup, float _alpha, float _speed)
    {
        while (canvasGroup.alpha != _alpha)
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, _alpha, _speed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator FadeLoopUI(CanvasGroup _canvasGrp, float _speed)
    {
        while (true)
        {
            yield return FadeUI(_canvasGrp, 0, _speed);
            yield return FadeUI(_canvasGrp, 1, _speed);
            yield return new WaitForSeconds(1);
        }
    }
}
