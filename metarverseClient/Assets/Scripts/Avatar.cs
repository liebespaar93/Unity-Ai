using System.Collections;
using System.Collections.Generic;
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
        anims[0] = Resources.Load("Male/male", typeof(RuntimeAnimatorController))
            as RuntimeAnimatorController;
        anims[1] = Resources.Load("Female/female", typeof(RuntimeAnimatorController))
            as RuntimeAnimatorController;

        // 문제가 있는 모델의 파트 이름을 수정
        var part = models[0].transform.Find("male_leg01 1");
        if (part != null)
        {
            part.name = "male_leg00";
        }
        var part2 = models[1].transform.Find("Object012");
        if (part2 != null)
        {
            part2.name = "female_hair04";
        }


        for (int i = 0; i < models[0].transform.childCount; i++)
        {
            var t = models[0].transform.GetChild(i);
            if (t.name.StartsWith("male_"))
                t.name = t.name.Substring(5);
        }
        for (int i = 0; i < models[1].transform.childCount; i++)
        {
            var t = models[1].transform.GetChild(i);
            if (t.name.StartsWith("female_"))
                t.name = t.name.Substring(7);
        }
    }
    void Start()
    {
    }
    void Update()
    {
    }
    //  Create avatar
    public void Create(int model)
    {
        if (models == null) Init();
        var go = Instantiate(models[model]);
        //  모델에서 만들어져서 인스턴스화된 오브젝트의 부모를 현재 아바타로 한다.
        go.transform.parent = transform;
        go.name = "model";
        animator = go.GetComponent<Animator>();
        animator.runtimeAnimatorController = anims[model];
    }
    public void Walk() { animator.SetInteger("animation", 1); }
    public void Sit() { animator.SetInteger("animation", 3); }
    public void Stand() { animator.SetInteger("animation", 0); }
    public void Work() { animator.SetInteger("animation", 2); }
    public void ChangeLook(int hair, int body, int legs, int shoes)
    {
        Transform trans = transform.Find("model");
        for (int i = 0; i < trans.childCount; i++)
        {
            var t = trans.GetChild(i);
            if (!t.name.StartsWith("hair")) continue;
            var idx = int.Parse(t.name.Substring(4));
            t.gameObject.SetActive(idx == hair);

        }
        for (int i = 0; i < trans.childCount; i++)
        {
            var t = trans.GetChild(i);
            if (!t.name.StartsWith("body") || t.name.Length <= 4) continue;
            var idx = int.Parse(t.name.Substring(4));
            t.gameObject.SetActive(idx == body);

        }
        for (int i = 0; i < trans.childCount; i++)
        {
            var t = trans.GetChild(i);
            if (!t.name.StartsWith("leg")) continue;
            var idx = int.Parse(t.name.Substring(3));
            t.gameObject.SetActive(idx == legs);
        }
        for (int i = 0; i < trans.childCount; i++)
        {
            var t = trans.GetChild(i);
            if (!t.name.StartsWith("shoes")) continue;
            var idx = int.Parse(t.name.Substring(5));
            t.gameObject.SetActive(idx == shoes);
        }
    }
}