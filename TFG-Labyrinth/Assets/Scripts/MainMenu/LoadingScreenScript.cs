
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenScript : MonoBehaviour
{
    public Image loadBar;


    private void Update()
    {
        loadBar.fillAmount = SceneController.inst.getLoadProgress();
    }

}
