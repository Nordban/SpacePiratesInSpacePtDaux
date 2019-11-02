using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    //Remove stray laser clones
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == ("playerNormalLaser(Clone)") || other.gameObject.name == ("missile(Clone)"))
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == ("Enemy"))
        {

            Destroy(other.gameObject);
        }
    }
}
