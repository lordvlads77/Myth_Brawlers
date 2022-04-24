using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float radius = default;
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
