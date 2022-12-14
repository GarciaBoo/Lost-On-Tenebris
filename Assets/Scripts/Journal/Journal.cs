using System.Collections.Generic;
using UnityEngine;

namespace LostOnTenebris
{
    [CreateAssetMenu(menuName = "Game/Journal/Journal", fileName = "New Journal")]
    public class Journal : ScriptableObject
    {
        [SerializeField] private List<JournalEntry> entries;

        public List<JournalEntry> Entries => entries;

        public void Add(JournalEntry entry)
        {
            if(!entries.Contains(entry))
                entries.Add(entry);
        }

        public void Clear()
        {
            entries.Clear();
        }
    }
}