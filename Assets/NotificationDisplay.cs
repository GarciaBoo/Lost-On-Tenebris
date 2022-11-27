using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LostOnTenebris
{
    public class NotificationDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private void Start()
        {
            text.text = "";
        }

        public void Display(string text)
        {
            StopAllCoroutines();
            StartCoroutine(Display(text, 3.0f));
        }

        private IEnumerator Display(string text, float time)
        {
            this.text.text = text;
            yield return new WaitForSeconds(time);
            this.text.text = "";
        }
    }
}
