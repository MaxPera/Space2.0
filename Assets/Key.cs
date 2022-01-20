using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Transform pTransform;
    Transform thisTransform;
    public PlayerLevelUp playerLevelUp;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(thisTransform.position,pTransform.position) <= 2)
        {
            playerLevelUp.hasKey = true;
            Destroy(gameObject);
        }
    }
}
