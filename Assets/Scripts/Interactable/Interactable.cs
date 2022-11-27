using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LostOnTenebris
{
    public class Interactable : MonoBehaviour
    {
        [Header("Interactable settings")]
        [SerializeField] private bool canInteract = true;
        [SerializeField] private UnityEvent OnInteract;
        [SerializeField] private InteractableRuntimeSet set;
        [SerializeField] private bool invisible;

        [SerializeField] private string prompt;

        [SerializeField] private bool overrideOrigin;
        [SerializeField] private Transform origin;

        public Vector3 Position => overrideOrigin ? origin.position : transform.position;
        
        private const int layer_enabled = 7;
        private const int layer_enabled_invisible = 8;
        private const int layer_disabled = 9;
        private const int layer_disabled_invisible = 10;

        private void OnEnable()
        {
            set.Add(this);
        }

        private void OnDisable()
        {
            set.Remove(this);
        }

        public void SetCanInteract(bool canInteract)
        {
            this.canInteract = canInteract;

            if (canInteract)
                this.gameObject.layer = invisible ? layer_enabled_invisible : layer_enabled;
            else
                this.gameObject.layer = invisible ? layer_disabled_invisible : layer_disabled;
        }
        
        public void Interact()
        {
            if (canInteract) OnInteract.Invoke();
        }

        public virtual string GetPrompt() => prompt;
    }
}
