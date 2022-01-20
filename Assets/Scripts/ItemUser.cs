using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUser : MonoBehaviour
{

    public Light playerLight;

    // Update is called once per frame
    void Update()
    {
        Lamp();
    }

    void Lamp()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            playerLight.enabled = !playerLight.enabled;
        }
    }
}
