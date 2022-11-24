using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LostOnTenebris
{
    public class JournalDisplay : MonoBehaviour
    {
        public Journal journal;

        [Header("Display")]
        [SerializeField] private TMP_Text text;
        [SerializeField] private List<JournalButton> buttons;
        [SerializeField] private EventSystem eventSystem;

        public void Awake() {
            journal.Clear();
        }

        public void ShowEntry(JournalEntry entry)
        {
            text.text = entry.content;
        }
        
        public void Show()
        {
            for (int i = 0; i < journal.Entries.Count; i++)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].SetEntry(journal.Entries[i]);
            }

            text.text = "";
            eventSystem.SetSelectedGameObject(buttons[0].gameObject);
        }

        public void Close()
        {
            for (int i = 0; i < buttons.Count; i++) buttons[i].gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        
    }
}