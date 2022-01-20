using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{

    public void PickedUp()
    {
        Destroy(gameObject);
    }

}
