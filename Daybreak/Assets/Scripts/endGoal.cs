using UnityEngine;
using UnityEngine.SceneManagement;

public class endGoal : MonoBehaviour
{
    public MeshCollider collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the player collides with the end goal, load the next scene
        if (collider.isTrigger)
        {
            // Load the next scene
            SceneManager.LoadScene("victory");
        }
    }
}
