using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject Pivot;

    [SerializeField] Transform Target;

    // Start is called before the first frame update
    void Start()
    {
        Pivot = transform.root.gameObject;
    }

    void FixedUpdate()
    {
        if (Target)
        {
            FollowTarget();
        }
    }

    void FollowTarget()
    {
        Pivot.transform.position = Vector3.Lerp(Pivot.transform.position, Target.position, 0.5f);
    }

}
