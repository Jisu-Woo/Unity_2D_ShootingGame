using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioo;
    void Start()
    {
        audioo = GetComponent<AudioSource>();  //audio�� audiosource�� ������
        audioo.Play();  //���� ���
        Destroy(gameObject, audioo.clip.length);  //audio.clip.length ��ŭ �ð��� ���� ����, gameObject �ı�
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
