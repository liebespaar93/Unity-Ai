using UnityEngine;

public class Avatar : MonoBehaviour
{
    static GameObject[] models;
    static RuntimeAnimatorController[] anims;
    Animator animator;
    public static void Init()
    {
        models = new GameObject[2];
        models[0] = Resources.Load("Male/male", typeof(GameObject)) as GameObject;
        models[1] = Resources.Load("Female/female", typeof(GameObject)) as GameObject;
        anims = new RuntimeAnimatorController[2];
        anims[0] = Resources.Load("Male/male", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
        anims[1] = Resources.Load("Female/female", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;

    }
    void Start()
    {

    }
    void Update()
    {

    }

    // Create avatar
    public void Create(int model)
    {
        if (models == null) Init();
        var go = Instantiate(models[model]);
        // Add child
        go.transform.parent = transform;
        animator = go.GetComponent<Animator>();
        animator.runtimeAnimatorController = anims[model];
    }

    public void Walk() { animator.SetInteger("animation", 1); }
    public void Sit() { animator.SetInteger("animation", 3); }
    public void Stand() { animator.SetInteger("animation", 0); }
    public void Work() { animator.SetInteger("animation", 2); }
}