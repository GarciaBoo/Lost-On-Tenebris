using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LostOnTenebris
{
    public class FPSFlashlight : MonoBehaviour
    {
        [Header("Light settings")]
        [SerializeField] private float maxCharge;
        [SerializeField] private bool isDecreasing;
        [SerializeField] private float decreaseRate;

        [Header("References")]
        [SerializeField] private Light lightSource;
        [SerializeField] private Light playerLight;
        [SerializeField] private FloatVariable charge;
        [SerializeField] private EventReference shutdownSound;
        [SerializeField] private StudioEventEmitter rechargeEmitter;
        
        [Header("Pannel")]
        [SerializeField] private StudioEventEmitter lightLevelBeeping;
        [SerializeField] private Material panel;
        
        private float baseIntensity;
        private float playerLightIntensity;
        private float currentIntensity;
        private bool isRecharging;
        private Coroutine flickerCoroutine;
        private bool on;

        // Pausing
        private bool wasDecreasing;
        
        public void SetCharge(float value)
        {
            charge.value = value;
        }
        
        private void Start()
        {
            baseIntensity = lightSource.intensity;
            playerLightIntensity = playerLight.intensity;
            charge.value = maxCharge;
            TurnOn();
        }

        private void Update()
        {
            if (!isRecharging && charge.value == 0.0f)
                Shutdown();
            
            if (isDecreasing && !isRecharging)
                charge.value = Mathf.MoveTowards(charge.value, 0.0f, Time.deltaTime * decreaseRate);

            panel.SetFloat("_Level", charge.value / maxCharge);
            lightLevelBeeping.SetParameter("Level", charge.value / maxCharge);
        }

        private void OnPause()
        {
            wasDecreasing = isDecreasing;
            this.isDecreasing = false;
            lightLevelBeeping.Stop();
        }

        private void OnUnpause()
        {
            isDecreasing = wasDecreasing;
            lightLevelBeeping.Play();
        }
        
        public void SetIsDecreasing(bool isDecreasing)
        {
            this.isDecreasing = isDecreasing;
        }

        public void Recharge()
        {
            if (!isRecharging) StartCoroutine(RechargeCoroutine());
        }

        private void Shutdown()
        {
            RuntimeManager.PlayOneShot(shutdownSound);
            StopCoroutine(flickerCoroutine);
            lightSource.intensity = 0.0f;
            playerLight.intensity = 0.0f;
            charge.value = -1;
            isDecreasing = false;
        }

        private void TurnOn()
        {
            flickerCoroutine = StartCoroutine(Flicker());
            lightSource.intensity = baseIntensity;
            playerLight.intensity = playerLightIntensity;
            on = true;
        }
        
        private IEnumerator RechargeCoroutine()
        {
            isRecharging = true;
            TurnOn();
            rechargeEmitter.Play();
            
            if (!on) TurnOn();
            while (charge.value < maxCharge)
            {
                charge.value = Mathf.MoveTowards(charge.value, maxCharge, 50f * Time.deltaTime);
                yield return null;
            }

            isRecharging = false;
        }
        
        private IEnumerator Flicker()
        {
            Queue<float> queue = new Queue<float>();
            int smoothing = 50;
            float sum = 0.0f;

            while (charge.value >= 0.0f)
            {
                while (queue.Count >= smoothing)
                    sum -= queue.Dequeue();
                
                float newVal = Random.Range(baseIntensity / 2, baseIntensity);
                queue.Enqueue(newVal);
                sum += newVal;

                lightSource.intensity = sum / queue.Count;
                yield return new WaitForSeconds(0.016f);
            }
        }
        
    }
}
