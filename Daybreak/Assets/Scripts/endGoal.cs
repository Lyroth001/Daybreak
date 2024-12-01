using System;
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
    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene("Victory");
    }
}
