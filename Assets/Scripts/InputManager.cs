using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bounce
{
    public class InputManager : MonoBehaviour
    {

        private void Update()
        {
            if (Input.GetKeyDown("r"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (Input.GetKeyDown("h"))
            {
                GUIController.Instance.ToggleHelp();
            }

            if (Input.GetButtonDown("Fire1"))
            {
                GameController.Instance.player.Fire = true;
            }

            if (Input.GetKeyDown("s"))
            {
                SceneManager.LoadScene("Scenes/Speed");
            }

            if (Input.GetKeyDown("p"))
            {
                SceneManager.LoadScene("Scenes/Precision");
            }
            
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            GameController.Instance.player.UpdateForce(vertical, horizontal);
        }
    }
}
