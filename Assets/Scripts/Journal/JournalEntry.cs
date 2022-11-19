using UnityEngine;

namespace LostOnTenebris
{
    [CreateAssetMenu(menuName = "Game/Journal/Entry", fileName = "New Journal Entry")]
    public class JournalEntry : ScriptableObject
    {
        public string title;
        [TextArea(12, 12)] public string content;
    }
}
