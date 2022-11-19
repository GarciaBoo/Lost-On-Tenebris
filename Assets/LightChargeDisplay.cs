using TMPro;
using UnityEngine;

namespace LostOnTenebris
{
    public class LightChargeDisplay : MonoBehaviour
    {
        [SerializeField] private FloatVariable charge;
        [SerializeField] private TMP_Text text;

        private void Update()
        {
            text.text = $"Light: {(int)charge.value}";
        }
    }
}
