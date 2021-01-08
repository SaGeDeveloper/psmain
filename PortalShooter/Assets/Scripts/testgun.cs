using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testgun : MonoBehaviour
{
    public GameObject prefab;
    public GameObject player;
    public float delay;
    public float fireRate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(delay);
        while(true){
            if(Input.GetKey(KeyCode.Mouse0)){
                Instantiate(prefab, transform.position, transform.rotation).GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity;
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

}
