using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPrepareBehaviour : PrepareCallback
{
    void Update()
    {
        if (Vector3.Distance(transform.position, transform.parent.position) > 0.01f)
        {
            transform.Translate((transform.parent.position - transform.position) * Time.deltaTime, Space.World);
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }
        if (Mathf.Abs(transform.localScale.x - 1.0f) > 0.01f)
        {
            transform.localScale += Vector3.one * ((transform.localScale.x < 1) ? 1 : -1) * Time.deltaTime * 2.0f;
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }
}
