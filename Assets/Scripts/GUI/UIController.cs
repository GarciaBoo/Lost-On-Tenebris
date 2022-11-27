using UnityEngine;
using UnityEngine.InputSystem;

namespace LostOnTenebris
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private JournalDisplay journalDisplay;
        [SerializeField] private GameObject player;

        // State
        private bool journalOpen = false;
        
        // Input Events
        public void OnOpenJournal(InputValue value)
        {
            ToggleJournal();
        }
        
        public void ToggleJournal()
        {
            if (journalOpen)
            {
                player.SendMessage("OnUnpause");
                journalDisplay.Close();
                journalDisplay.gameObject.SetActive(false);
            }
            else
            {
                player.SendMessage("OnPause");
                journalDisplay.gameObject.SetActive(true);
                journalDisplay.Show();
            }

            journalOpen = !journalOpen;
        }
    }
}
