using UnityEngine;
using UnityEngine.SceneManagement;

namespace LostOnTenebris
{
    public class Redirect : MonoBehaviour
    {
        [SerializeField] private int sceneBuildIndex;

        private void Start()
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
