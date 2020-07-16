using System.Collections;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private Rigidbody[] children;
   
    [ContextMenu("collapse")]
    public void CollapseTree()
    {
        children = this.transform.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody child in children)
        {
            LeanTween.rotateX(this.gameObject, -88, 1.5f);
            child.isKinematic = false;
        }
        StartCoroutine(TurnOnGravity());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            foreach (Rigidbody child in children)
            {
                child.useGravity = false;
                child.constraints = RigidbodyConstraints.FreezePositionY;
            }
            StartCoroutine(ChangeTreeState());
        }
        if (other.CompareTag("Axe"))
        {
            CollapseTree();
        }
    }
    public IEnumerator TurnOnGravity()
    {
        yield return new WaitForSeconds(1f);
        foreach (Rigidbody child in children)
        {
            child.useGravity = true;
        }
    }
    public IEnumerator ChangeTreeState()
    {
        yield return new WaitForSeconds(1f);
        foreach (Rigidbody child in children)
        {
            Destroy(child.gameObject.GetComponent<Tree>());
            child.transform.parent = null;
        }
    }
}
