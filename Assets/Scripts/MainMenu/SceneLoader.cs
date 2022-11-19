using UnityEngine;
using UnityEngine.SceneManagement;

namespace LostOnTenebris
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(int sceneBuildIndex)
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
