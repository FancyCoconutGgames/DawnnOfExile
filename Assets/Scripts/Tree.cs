using System;
using System.Collections;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private Rigidbody[] treeParts;
    private Vector3 axeHitPoint;
    private float collapseAngle;

    [ContextMenu("collapseTree")]
    public void CollapseTree()
    {
       treeParts = this.transform.GetComponentsInChildren<Rigidbody>();
       CollapseInDirection();
       StartCoroutine(TurnOnGravity());
    }

    public void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Ground"))
        {
            foreach (Rigidbody child in treeParts)
            {
                child.useGravity = false;
                child.constraints = RigidbodyConstraints.FreezePositionY;
            }
            StartCoroutine(ChangeTreeState());
        }
        if (coll.CompareTag("Axe"))
        {
            axeHitPoint = coll.ClosestPointOnBounds(coll.gameObject.transform.position);
            CollapseTree();
        }
    }

    private void CollapseInDirection()
    {
        if (axeHitPoint.z < 0.5 && axeHitPoint.z > -0.5)
        {
            if (axeHitPoint.z > 0)
            {
                collapseAngle = -88;
            }
            else
            {
                collapseAngle = 88;
            }
            foreach (Rigidbody child in treeParts)
            {
                LeanTween.rotateX(this.gameObject, collapseAngle, 1.5f);
                child.isKinematic = false;
            }
            return;
        }
        else if (axeHitPoint.x > -0.5 && axeHitPoint.x < 0.5)
        {
            if (axeHitPoint.x > 0)
            {
                collapseAngle = 88;
            }
            else
            {
                collapseAngle = -88;
            }
            foreach (Rigidbody child in treeParts)
            {
                LeanTween.rotateZ(this.gameObject, collapseAngle, 1.5f);
                child.isKinematic = false;
            }
            return;
        }
    }
    private IEnumerator TurnOnGravity()
    {
        yield return new WaitForSeconds(1f);
        foreach (Rigidbody child in treeParts)
        {
            child.useGravity = true;
        }
    }
    private IEnumerator ChangeTreeState()
    {
        yield return new WaitForSeconds(1f);
        foreach (Rigidbody child in treeParts)
        {
            Destroy(child.gameObject.GetComponent<Tree>());
            child.gameObject.AddComponent<Log>();
            child.transform.parent = null;
        }
    }
}
