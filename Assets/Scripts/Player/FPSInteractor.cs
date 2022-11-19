using UnityEngine;

namespace LostOnTenebris
{
    public class FPSInteractor : Interactor
    {
        [SerializeField] private FPSController controller;

        [SerializeField] private float maxDistance = 5.0f;
        [SerializeField] private float minDot = 0.8f;

        [SerializeField] private HUDController hud;
        
        protected override bool CanInteract(Interactable i)
        {
            Vector3 controllerToObj = i.transform.position - controller.Position;
            float distance = controllerToObj.magnitude;
            float dot = Vector3.Dot(controllerToObj.normalized, controller.LookDirection);
            return distance < maxDistance && dot > minDot;
        }

        protected override void OnCanInteract(Interactable i)
        {
            string prompt = i.GetPrompt();
            hud.SetPrompt(prompt);
        }
        
        protected override void OnCantInteract()
        {
            hud.ClearPrompt();
        }
    }
}