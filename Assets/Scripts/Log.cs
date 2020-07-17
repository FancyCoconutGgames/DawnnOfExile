using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    private GameObject logPrefab;
    private int hp = 3;
    public void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Axe"))
        {
            Debug.Log("plank");
            logPrefab = Resources.Load("Plank") as GameObject;
            if(hp!= 0)
            {
                GameObject log = Instantiate(logPrefab, transform.position + Vector3.up, Quaternion.identity);
                hp--;
            }
            if(hp == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
