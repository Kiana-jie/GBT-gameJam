using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaskController : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator EnableMask(float duration)
    {
        gameObject.SetActive(true);

        float time = 0.0f;

        while (time < duration)
        {
            float t = time / duration;
            gameObject.transform.localPosition = Vector3.Lerp(startPosition * gameObject.transform.parent.localScale.x, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.localPosition = targetPosition;
    }
    public IEnumerator DisableMask(float duration)
    {
        float time = 0.0f;

        while (time < duration)
        {
            float t = time / duration;
            gameObject.transform.localPosition = Vector3.Lerp(targetPosition, startPosition * gameObject.transform.parent.localScale.x, t);
            time += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.localPosition = startPosition;
        gameObject.SetActive(false);
    }
}
