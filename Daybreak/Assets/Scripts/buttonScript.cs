using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class buttonScript : MonoBehaviour
{
    public string gameScene;
    public void changeScene()
    {
        SceneManager.LoadScene(gameScene);
    }
    
}
