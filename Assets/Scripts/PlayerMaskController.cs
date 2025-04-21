using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaskController : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float maskSize = 1.0f;
    private float maskRatio;
    private float maskWidth;
    private Vector3 startPosition;

    void Awake()
    {
        startPosition = gameObject.transform.localPosition;
        maskRatio = gameObject.transform.localScale.x;
        maskWidth = gameObject.GetComponent<SpriteRenderer>().size.x - 1.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ResizeMask(maskSize);
    }

    // 协程，开启玩家遮罩
    public IEnumerator EnableMask(float duration)
    {
        gameObject.SetActive(true);

        float time = 0.0f;

        while (time < duration)
        {
            float t = time / duration;
            gameObject.transform.localPosition = Vector3.Lerp(startPosition * gameObject.transform.parent.localScale.x, Vector3.zero, t);
            time += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.localPosition = Vector3.zero;
    }

    // 协程，关闭玩家遮罩
    public IEnumerator DisableMask(float duration)
    {
        float time = 0.0f;

        while (time < duration)
        {
            float t = time / duration;
            gameObject.transform.localPosition = Vector3.Lerp(Vector3.zero, startPosition * gameObject.transform.parent.localScale.x, t);
            time += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.localPosition = startPosition;
        gameObject.SetActive(false);
    }

    // 设置遮罩大小，输入0-1之间的值
    public void ResizeMask(float rate)
    {
        gameObject.transform.localScale = new Vector3(rate * maskRatio, rate * maskRatio, 1);
        // 为保持遮罩的描边宽度一致，需要调整贴图的大小
        rate += 0.000001f; // 避免除以0
        gameObject.GetComponent<SpriteRenderer>().size = new Vector2(1 + maskWidth / rate, 1 + maskWidth / rate);
    }
}
