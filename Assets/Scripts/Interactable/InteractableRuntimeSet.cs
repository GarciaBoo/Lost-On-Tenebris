using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostOnTenebris
{
    [CreateAssetMenu(menuName = "Custom/RuntimeSets/Interactable", fileName = "New InteractableRuntimeSet")]
    public class InteractableRuntimeSet : ScriptableObject
    {
        private List<Interactable> items = new List<Interactable>();

        public void Add(Interactable item)
        {
            if (!items.Contains(item)) items.Add(item);
        }

        public void Remove(Interactable item)
        {
            if (items.Contains(item)) items.Remove(item);
        }

        public void SetCanInteract(int index)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (i == index) items[i].SetCanInteract(true);
                else items[i].SetCanInteract(false);
            }
                
        }
        
        public List<Interactable> GetInteractables()
        {
            return items;
        }
    }
}
