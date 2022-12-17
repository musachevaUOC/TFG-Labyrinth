using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public delegate GameObject go();

    // Start is called before the first frame update
    public void loadGame()
    {
        SceneManager.LoadScene(1);
    }
}
