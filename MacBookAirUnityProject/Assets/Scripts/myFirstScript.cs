using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myFirstScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start() called" + name);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update() called" + name);
    }
    private void Awake()
    {
        Debug.Log("Awake() called" + name);
    }
    private void LateUpdate()
    {
        Debug.Log("LateUpdate() called" + name);
    }
}
