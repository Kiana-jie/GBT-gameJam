using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneController : MonoBehaviour
{
    public GameObject wall;
    public GameObject player1Mask;
    public GameObject player2Mask;
    public Vector3 resetPosition;
    public Vector3 world1Position;
    public Vector3 world2Position;
    // Start is called before the first frame update
    void Start()
    {
        player1Mask.SetActive(false);
        player2Mask.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ShiftToWorld1();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ShiftToCentre();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ShiftToWorld2();

    }

    private IEnumerator MoveWall(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = wall.transform.position;
        float time = 0.0f;

        while (time < duration)
        {
            float t = time / duration;
            wall.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }

        wall.transform.position = targetPosition;
        wall.GetComponent<BoxCollider2D>().enabled = true;
    }

    private IEnumerator ResetWall(float duration)
    {
        Vector3 startPosition = wall.transform.position;
        float time = 0.0f;

        while (time < duration)
        {
            float t = time / duration;
            wall.transform.position = Vector3.Lerp(startPosition, resetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }

        wall.transform.position = resetPosition;
        wall.GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(player1Mask.GetComponent<PlayerMaskController>().DisableMask(0.1f));
        StartCoroutine(player2Mask.GetComponent<PlayerMaskController>().DisableMask(0.1f));
    }

    public IEnumerator MoveWall(Vector3 targetPosition, Quaternion targetRotation, float duration)
    {
        Vector3 startPosition = wall.transform.position;
        Quaternion startRotation = wall.transform.rotation;
        float time = 0.0f;

        while (time < duration)
        {
            float t = time / duration;
            wall.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            wall.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            time += Time.deltaTime;
            yield return null;
        }

        wall.transform.position = targetPosition;
        wall.transform.rotation = targetRotation;
        wall.GetComponent<BoxCollider2D>().enabled = true;
    }


    public void ShiftToWorld1()
    {
        wall.GetComponent<BoxCollider2D>().enabled = false;
        player1Mask.SetActive(true);
        player2Mask.SetActive(false);
        StartCoroutine(player1Mask.GetComponent<PlayerMaskController>().EnableMask(0.1f));
        StartCoroutine(MoveWall(world1Position, 0.5f));
    }

    public void ShiftToWorld2()
    {
        wall.GetComponent<BoxCollider2D>().enabled = false;
        player1Mask.SetActive(false);
        player2Mask.SetActive(true);
        StartCoroutine(player2Mask.GetComponent<PlayerMaskController>().EnableMask(0.1f));
        StartCoroutine(MoveWall(world2Position, 0.5f));
    }

    public void ShiftToCentre()
    {
        StartCoroutine(ResetWall(0.5f));

    }

}
