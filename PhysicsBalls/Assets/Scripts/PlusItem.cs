using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject obj = Instantiate(collider.gameObject);
        obj.transform.SetParent(collider.gameObject.transform.parent);

        obj.GetComponent<Ball>().isRunning = true;
        var oldRig = collider.gameObject.GetComponent<Rigidbody2D>();
        var rig = obj.GetComponent<Rigidbody2D>();
        rig.velocity = oldRig.velocity;
        rig.angularVelocity = oldRig.angularVelocity;

        Destroy(gameObject);
    }
}
