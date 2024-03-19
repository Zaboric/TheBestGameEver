using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeCaster : MonoBehaviour
{
    public Rigidbody grenadePrefab;
    public Transform grenadeSourceTransform;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var granate = Instantiate(grenadePrefab);
            granate.transform.position = grenadeSourceTransform.position;
            granate.GetComponent<Rigidbody>().AddForce(grenadeSourceTransform.forward * force);
        }
    }
}
