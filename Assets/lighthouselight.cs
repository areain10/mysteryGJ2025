using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lighthouselight : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0.0f, 5.0f)] public float speed;
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0,1,0) * speed * Time.deltaTime);
        
    }
}
