using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform player;
    private float timeToStartRun = 1.0f;
    
    private void Update()
    {
        if (player is null) return;
        if (timeToStartRun >= 0.0f)
        {
            timeToStartRun -= Time.deltaTime;
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, player.position, 20.0f * Time.deltaTime);
    }
}
