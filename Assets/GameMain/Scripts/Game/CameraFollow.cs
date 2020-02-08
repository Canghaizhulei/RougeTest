using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MyMonoSingleton<CameraFollow>
{
    public Transform Target;
    private Vector3 offset;
    public float Speed = 30;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 11, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if(Target!= null)
        {
            transform.position = Vector3.Lerp(transform.position, Target.position + offset, Speed * Time.deltaTime);
        }
    }
}
