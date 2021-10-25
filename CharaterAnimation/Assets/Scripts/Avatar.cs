using UnityEngine;

public class Avatar : MonoBehaviour
{
    Animator control;
    void Start()
    {
        control = GetComponent<Animator>();
        Idle();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) Walk();
        else if (Input.GetKeyDown(KeyCode.S)) Sit();
        else if (Input.GetKeyDown(KeyCode.D)) Work();
        else if (Input.GetKeyDown(KeyCode.A)) Idle();
    }
    void Idle()
    {
        Debug.Log("Idle");
        control.SetInteger("animation", 0);
    }
    void Work()
    {
        control.SetInteger("animation", 2);
    }
    void Walk()
    {
        Debug.Log("Walk");
        control.SetInteger("animation", 1);
    }
    void Sit()
    {
        Debug.Log("Sit");
        control.SetInteger("animation", 3);
    }
    void Stand()
    {
        Debug.Log("Stand");
        control.SetInteger("animation", 0);
    }
}