using System.Collections;
using FMODUnity;
using LostOnTenebris;
using Unity.Mathematics;
using UnityEngine;

public class Die : MonoBehaviour
{
    public FPSController player;
    public Monster monster;
    public FloatVariable light;
    public StudioEventEmitter emitter;

    private float timer = 5.0f;
    private void Update()
    {
        if (light.value > 0.0f) return;
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
            return;
        }
        bool hit = Physics.Raycast(player.Position, player.LookDirection, 30f);

        if (!hit)
        {
            Vector3 spawnPos = player.Position + new Vector3(player.LookDirection.x, 0, player.LookDirection.z) * 30f + new Vector3(0, -2.5f, 0);
            monster.transform.position = spawnPos;
            monster.transform.rotation = Quaternion.LookRotation(player.Position - spawnPos, new float3(0, 1, 0));
            monster.gameObject.SetActive(true);
            emitter.Play();
            this.enabled = false;
        }
    }
    
}
