using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float radius = default;
    private bool isFocusing = false;
    private Transform player = default;
    private bool hasInteracted = false;
    [SerializeField] private Transform _properInteraction = default;

    public virtual void Interact()
    {
        // This method is to be Overwritten || I truly need this comment please let me keep it *sweats*
        Debug.Log("Interacting with " + transform.name);
    }

    private void Update()
    {
        if (isFocusing && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, _properInteraction.position);
            if (distance <= radius)
            {
                Debug.Log("INTERACT");
                hasInteracted = true;
            }
        }
    }

    public void OnFocusing(Transform playertTransform)
    {
        isFocusing = true;
        player = playertTransform;
        hasInteracted = false;
    }

    public void OnDefocusing()
    {
        isFocusing = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_properInteraction.position, radius);
    }
}
