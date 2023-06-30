using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnCollider : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
