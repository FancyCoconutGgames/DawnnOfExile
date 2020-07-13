using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTrunk : MonoBehaviour
{
    private Rigidbody[] children;
    [SerializeField] bool pom;
    private Rigidbody rb;
    [SerializeField] private bool isFallen = false;
    // Start is called before the first frame update
    void Start()
    {
        children = this.transform.GetComponentsInChildren<Rigidbody>();
        //rb = this.GetComponent<Rigidbody>();
    }
    /*
    // Update is called once per frame
    void Update()
    {
        if(pom == true)
        {
            foreach(Rigidbody child in children)
            {
                child.useGravity = true;
            }
        }
    }*/
    [ContextMenu("collapse")]
    public void CollapseTree()
    {
        foreach (Rigidbody child in children)
        {
            child.isKinematic = false;
            LeanTween.rotateX(child.gameObject, -10, 1);
        }
        StartCoroutine(collapsePartOfTree());
        children = this.transform.GetComponentsInChildren<Rigidbody>();
    }

    private IEnumerator collapsePartOfTree()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (Rigidbody child in children)
        {
            child.useGravity = true;
            child.transform.parent = null;
   
        }
       
    }
}
