using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
    public float fade_time= 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,fade_time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
