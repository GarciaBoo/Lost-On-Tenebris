using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LostOnTenebris
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private int SceneIndex;

        public void Load()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneIndex);
        }

    }
}
