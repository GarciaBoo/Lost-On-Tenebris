using FMODUnity;
using UnityEngine;

namespace LostOnTenebris
{
    public class SimpleDoor : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Animator animator;
        [SerializeField] private Collider doorCollider;
        [SerializeField] private GameObject doorTransform;
        
        [Header("Sound")]
        [SerializeField] private EventReference onOpen;
        [SerializeField] private EventReference onLocked;

        [Header("Door")]
        [SerializeField] private bool locked = false;
        [SerializeField] private string openAnimatorStateName;
        [SerializeField] private string closedAnimatorStateName;

        [Header("Alert message if closed")]
        [SerializeField] private HUDController hudController = null;
        [SerializeField] private string alertMessage = null;
        
        private bool open = false;
        private static readonly int toggle = Animator.StringToHash("Toggle");

        public void Toggle()
        {
            if (CanOpen())
            {
                animator.SetTrigger(toggle);
                open = !open;
                doorCollider.enabled = !open;
                RuntimeManager.PlayOneShotAttached(onOpen, doorTransform);
            } else if (locked)
            {
                RuntimeManager.PlayOneShotAttached(onLocked, doorTransform);
                if(hudController != null && alertMessage != null)
                {
                    hudController.Alert(alertMessage);
                }
            }
        }
        
        public void SetLocked(bool locked)
        {
            this.locked = locked;
        }

        private bool CanOpen()
        {
            return (animator.GetCurrentAnimatorStateInfo(0).IsName(openAnimatorStateName) 
                    || animator.GetCurrentAnimatorStateInfo(0).IsName(closedAnimatorStateName))
                   && !locked;
        }
    }
}
