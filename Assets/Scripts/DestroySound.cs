using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioo;
    void Start()
    {
        audioo = GetComponent<AudioSource>();  //audio에 audiosource를 가져옴
        audioo.Play();  //사운드 재생
        Destroy(gameObject, audioo.clip.length);  //audio.clip.length 만큼 시간이 지난 다음, gameObject 파괴
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
