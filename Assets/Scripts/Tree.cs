using System;
using System.Collections;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private Rigidbody[] children;
    private Vector3 axeHit;
    private float angle;
    [ContextMenu("collapse")]
    public void CollapseTree()
    {
        children = this.transform.GetComponentsInChildren<Rigidbody>();
        

        /*float angle = findCollapseAngle(axeHit);
        Debug.Log(angle);*/
        if (axeHit.z < 0.5 && axeHit.z > -0.5)
        {
            if(axeHit.z > 0)
            {
                angle = -88;
            }
            else
            {
                angle = 88;
            }
            foreach (Rigidbody child in children)
            {
                LeanTween.rotateX(this.gameObject, angle, 1.5f);
                child.isKinematic = false;
            }
            return;
        }
        /*else if (axeHit.z > -0.5)
        {
            foreach (Rigidbody child in children)
            {
                LeanTween.rotateX(this.gameObject, -88, 1.5f);
                child.isKinematic = false;
            }
            return;
        }*/
        else if (axeHit.x > -0.5 && axeHit.x < 0.5)
        {
            if (axeHit.x > 0)
            {
                angle = 88;
            }
            else
            {
                angle = -88;
            }
            foreach (Rigidbody child in children)
            {
                LeanTween.rotateZ(this.gameObject, angle, 1.5f);
                child.isKinematic = false;
            }
            return;
        }

        /*else if (axeHit.x < 0.5)
        {
                foreach (Rigidbody child in children)
                {
                    LeanTween.rotateZ(this.gameObject, -88, 1.5f);
                    child.isKinematic = false;
                }
                return;
        }*/

       StartCoroutine(TurnOnGravity());
    }

    private float findCollapseAngle(Vector3 closestPoint)
    {
        if (closestPoint.z < 0)
        {
            return 88f;
        }
        else if (closestPoint.z > 0 )
        {
            return -88f;
        }
        return 0;
    }

    public void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Ground"))
        {
            foreach (Rigidbody child in children)
            {
                child.useGravity = false;
                child.constraints = RigidbodyConstraints.FreezePositionY;
            }
            StartCoroutine(ChangeTreeState());
        }
        if (coll.CompareTag("Axe"))
        {
            axeHit = coll.ClosestPointOnBounds(coll.gameObject.transform.position);
            Debug.Log(axeHit);
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
