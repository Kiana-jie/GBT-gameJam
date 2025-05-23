using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject wall;
    public GameObject player1Mask;
    public GameObject player2Mask;
    public Vector3 centrePosition;
    public Vector3 world1Position;
    public Vector3 world2Position;
    private int worldNow = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 测试场景切换，用123左右切
        if (Input.GetKeyDown(KeyCode.Alpha1))
            StartCoroutine(ShiftToWorld1());
        if (Input.GetKeyDown(KeyCode.Alpha2))
            StartCoroutine(ShiftToCentre());
        if (Input.GetKeyDown(KeyCode.Alpha3))
            StartCoroutine(ShiftToWorld2());
        if (Input.GetKeyDown(KeyCode.Alpha4))
            StartCoroutine(GameManager.Instance.WorldAttack());
    }

    // 协程，将折叠线移动到任意位置
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
    }

    // 协程，将折叠线移动到任意位置，并旋转到任意角度，虽然暂时不用但是以后可以拿来玩
    private IEnumerator MoveWall(Vector3 targetPosition, Quaternion targetRotation, float duration)
    {
        Vector3 startPosition = wall.transform.position;
        Quaternion startRotation = wall.transform.rotation;
        float time = 0.0f;

        while (time < duration)
        {
            float t = time / duration;
            wall.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            wall.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            time += Time.deltaTime;
            yield return null;
        }

        wall.transform.position = targetPosition;
        wall.transform.rotation = targetRotation;


    }

    // 没救了，世界1和世界2之间不能直接切，会穿帮
    // 我直接暴力解决

    // 协程队列，切换到世界1
    public IEnumerator ShiftToWorld1()
    {
        wall.GetComponent<BoxCollider2D>().enabled = false;
        if (worldNow == 0)
        {
            StartCoroutine(player1Mask.GetComponent<PlayerMaskController>().EnableMask(0.1f));
            yield return StartCoroutine(MoveWall(world1Position, 0.5f));
            yield return StartCoroutine(player2Mask.GetComponent<PlayerMaskController>().DisableMask(0.1f));
        }
        else if (worldNow == 2)
        {
            yield return StartCoroutine(MoveWall(centrePosition, 0.25f));
            StartCoroutine(player2Mask.GetComponent<PlayerMaskController>().DisableMask(0.05f));
            StartCoroutine(player1Mask.GetComponent<PlayerMaskController>().EnableMask(0.05f));
            yield return StartCoroutine(MoveWall(world1Position, 0.25f));
            yield return StartCoroutine(player2Mask.GetComponent<PlayerMaskController>().DisableMask(0.1f));
        }
        wall.GetComponent<BoxCollider2D>().enabled = true;
        worldNow = 1;
        Debug.Log(2);

        //World1音量调大,Wprld2音量调小
        /*
            查找对应audioclip的gameobject
            设置音量
         */
        AudioSource source = FindClip(GameManager.Instance.currentWave + "_left");
        source.volume = 0.3f;
    }

    // 协程队列，切换到世界2
    public IEnumerator ShiftToWorld2()
    {
        wall.GetComponent<BoxCollider2D>().enabled = false;
        if (worldNow == 0)
        {
            StartCoroutine(player2Mask.GetComponent<PlayerMaskController>().EnableMask(0.1f));
            yield return StartCoroutine(MoveWall(world2Position, 0.5f));
            yield return StartCoroutine(player1Mask.GetComponent<PlayerMaskController>().DisableMask(0.1f));
        }
        else if (worldNow == 1)
        {
            yield return StartCoroutine(MoveWall(centrePosition, 0.25f));
            StartCoroutine(player1Mask.GetComponent<PlayerMaskController>().DisableMask(0.05f));
            StartCoroutine(player2Mask.GetComponent<PlayerMaskController>().EnableMask(0.05f));
            yield return StartCoroutine(MoveWall(world2Position, 0.25f));
            yield return StartCoroutine(player1Mask.GetComponent<PlayerMaskController>().DisableMask(0.1f));
        }
        wall.GetComponent<BoxCollider2D>().enabled = true;
        Debug.Log(2);
        worldNow = 2;

        //World2音量调大
        AudioSource source = FindClip(GameManager.Instance.currentWave + "_right");
        source.volume = 0.3f;
    }

    // 协程队列，切换到中间
    public IEnumerator ShiftToCentre()
    {
        wall.GetComponent<BoxCollider2D>().enabled = false;
        yield return StartCoroutine(MoveWall(centrePosition, 0.5f));
        StartCoroutine(player1Mask.GetComponent<PlayerMaskController>().DisableMask(0.1f));
        StartCoroutine(player2Mask.GetComponent<PlayerMaskController>().DisableMask(0.1f));
        wall.GetComponent<BoxCollider2D>().enabled = true;
        Debug.Log(2);
        worldNow = 0;

        //两边bgm音量持平
        AudioSource source1 = FindClip(GameManager.Instance.currentWave + "_left");
        AudioSource source2 = FindClip(GameManager.Instance.currentWave + "_right");
        source1.volume = 1f;
        source2.volume = 1f;
    }


    public AudioSource FindClip(string clipName)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("bgm");
        foreach (GameObject gameObject in gameObjects)
        {
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null && audioSource.clip.name == clipName)
            {
                return audioSource;
            }
        }
        Debug.Log("Fail to find!");
        return null;
    }
}

