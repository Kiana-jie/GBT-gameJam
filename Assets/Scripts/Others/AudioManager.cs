using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get 
        {
            return instance;
        }
    }
    [Serializable]
    public class ClipInfo
    {
        public string Name;
        public  AudioClip Clip;
        public bool Loop;
    }
    [SerializeField] private List<ClipInfo> clipInfoList;
    private Dictionary<string, ClipInfo> clipInfoDic;

    public class SourceInfo
    {
        public string SourceName;
        public GameObject Target;
        public AudioSource Source;
    }
    [SerializeField] private List<SourceInfo> sourceInfoList;

    private void Awake()
    {
        instance = this;
        transform.parent = null;
        DontDestroyOnLoad(gameObject);

        clipInfoDic = new Dictionary<string, ClipInfo>();
        foreach (var clipInfo in clipInfoList)
        {
            clipInfoDic.Add(clipInfo.Name, clipInfo);
        }
        sourceInfoList = new List<SourceInfo>();
    }
    
    public void Play(string name, GameObject player)
    {
        SourceInfo curInfo = null;
        if (clipInfoDic[name].Loop)
        {
            foreach (var info in sourceInfoList)
            {
                if(info.SourceName == name)
                {
                    curInfo = info;
                    break;
                }
            }
        }
        if(curInfo == null || !clipInfoDic[name].Loop)
        {
            curInfo = new SourceInfo();
            GameObject GO = new GameObject();
            GO.tag = "bgm";
            curInfo.Source = GO.AddComponent<AudioSource>();
            curInfo.Source.transform.parent = transform;
            sourceInfoList.Add(curInfo);
            curInfo.SourceName = name;
            curInfo.Target = player;
            curInfo.Source.clip = clipInfoDic[name].Clip;
            curInfo.Source.loop = clipInfoDic[name].Loop;
            curInfo.Source.Play();
        }
        else
        {
            ////查找当前有无空闲source
            if (curInfo.Source.isPlaying && curInfo.Source.loop)
            {
                return;
            }
            else
            {
                curInfo.Source.Play();
            }
        }
    }

    public void Stop(string name,GameObject player)
    {
        foreach(var info in sourceInfoList)
        {
            if(info.Target == player && info.SourceName == name)
            { info.Source.Stop(); }
        }
    }

    private void FixedUpdate()
    {
        foreach(var info in sourceInfoList)
        {
            if(info.Target != null && info.Source.isPlaying)
            {
                info.Source.transform.position = info.Target.transform.position;
            }
        }
    }
}
