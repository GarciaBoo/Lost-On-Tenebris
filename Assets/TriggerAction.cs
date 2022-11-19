using UnityEngine;
using UnityEngine.Events;

public class TriggerAction : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public bool triggerOnce = true;
    
    private void OnTriggerEnter(Collider other)
    {
        if (triggerOnce) gameObject.SetActive(false);
        onTriggerEnter.Invoke();
    }
}
