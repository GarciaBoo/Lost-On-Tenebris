using TMPro;
using UnityEngine;

namespace LostOnTenebris
{
    public class JournalButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private JournalDisplay display;
        private JournalEntry entry;

        public void SetEntry(JournalEntry entry)
        {
            this.entry = entry;
            text.text = entry.title;
        }

        public void OpenEntry()
        {
            display.ShowEntry(entry);
        }
    }
}