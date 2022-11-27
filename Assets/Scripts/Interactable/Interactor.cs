using System;
using UnityEngine;

namespace LostOnTenebris
{
    public abstract class Interactor : MonoBehaviour
    {
        [SerializeField] private InteractableRuntimeSet interactables;

        [SerializeField] private Material outline;
        private float outlineAlpha = 0.0f;

        private bool lastFrameCanInteractAny = false;
        
        private void Update()
        {
            Interactable canInteract = CheckCanInteract();

            if (canInteract)
            {
                outlineAlpha = Mathf.MoveTowards(outlineAlpha, 1.0f, Time.deltaTime * 5.0f);
                if (!lastFrameCanInteractAny) OnCanInteract(canInteract);
            }
            else
            {
                outlineAlpha = Mathf.MoveTowards(outlineAlpha, 0.0f, Time.deltaTime * 5.0f);
                if (lastFrameCanInteractAny) OnCantInteract();
            }

            outline.SetFloat("_OutlineAlpha", outlineAlpha);
            lastFrameCanInteractAny = canInteract;
        }

        private Interactable CheckCanInteract()
        {
            for (int i = 0; i < interactables.GetInteractables().Count; i++)
            {
                if (!CanInteract(interactables.GetInteractables()[i])) continue;
                interactables.SetCanInteract(i);
                return interactables.GetInteractables()[i];
            }
            return null;
        }
        
        public void OnInteract()
        {
            for (int i = 0; i < interactables.GetInteractables().Count; i++)
            {
                if (!CanInteract(interactables.GetInteractables()[i])) continue;
                interactables.GetInteractables()[i].Interact();
            }
        }

        protected abstract bool CanInteract(Interactable i);
        protected abstract void OnCanInteract(Interactable i);
        protected abstract void OnCantInteract();
    }
}
 