using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5Script : MonoBehaviour
{
    public GameObject enemyBullet;
    public GameObject bulletSpawn;
    public float fireRate;
    private float nextFire;
    private PlayerScript playerScript;
    //public GameObject destroySoundPref;


    void FixedUpdate()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(enemyBullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        }

    }


            /*�� �ڵ�� enemyBullet ���� ������Ʈ�� bulletSpawn ��ġ���� �߻��ϴ� ��ũ��Ʈ�Դϴ�. fixedUpdate() �Լ��� �� �����Ӹ��� ������ �ð� �������� ȣ��Ǵ� �Լ��Դϴ�. �� �Լ��� ����Ͽ� ���� �����ϰ�, ���� �ð� �������� �Ѿ��� �߻��ϰ� �˴ϴ�.

        public GameObject enemyBullet: �߻�� �Ѿ� ������Ʈ�� ��Ÿ���� public �����Դϴ�.
        public GameObject bulletSpawn: �Ѿ��� �߻�� ��ġ�� ��Ÿ���� public �����Դϴ�.
        public float fireRate: �Ѿ��� �߻�Ǵ� �ֱ⸦ ��Ÿ���� public �����Դϴ�.
        private float nextFire: ���� �Ѿ��� �߻�� �ð��� �����ϴ� private �����Դϴ�.
        FixedUpdate() �Լ����� if (Time.time > nextFire) ������ ����Ͽ� fireRate �ֱ⿡ ���� �Ѿ��� �߻��մϴ�. 

        Instantiate() �Լ��� ����Ͽ� enemyBullet ������Ʈ�� bulletSpawn ��ġ�� ȸ�������� �����մϴ�. nextFire ������ ���� �ð��� fireRate�� ���� ���� �����Ͽ�, ���� �Ѿ��� �߻�� �ð��� ����մϴ�. 
            �̷��� �����ν� fireRate �ֱ⸶�� �Ѿ��� �߻��� �� �ֽ��ϴ�. */

            // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("player"); // ���� ������Ʈ�� �±�(��player��)�� �˻��Ͽ� ����
        if (playerObject != null)
            playerScript = playerObject.GetComponent<PlayerScript>(); // ���� ���������� PlayerScript �� �����ϱ� ���� ����
    }

    // Update is called once per frame

}
