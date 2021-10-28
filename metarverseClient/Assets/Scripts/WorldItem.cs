using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    Material[] mtlList;
    Material mtlBoard;

    // Start is called before the first frame update
    void Awake()
    {
        mtlList = new Material[3];
        mtlList[1] = Resources.Load("Materials/White") as Material;
        mtlList[2] = Resources.Load("Materials/Black") as Material;
        mtlBoard = Resources.Load("Materials/Board", typeof(Material)) as Material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CreateItem(string itemName, float x, float y, float dir)
    {
        if (itemName == "Reversi") CreateReversi(transform);
        else
        {

            // 1. item 오브젝트 만들기
            var item = GameObject.CreatePrimitive(PrimitiveType.Cube);
            name = itemName;
            // 2. 아이템 오브젝트 자식으로 등록
            item.transform.parent = transform;
            item.name = "0";
        }
        name = itemName;
        // 3. 위치 지정하기
        transform.localPosition = new Vector3(x, 0, y);
        transform.localEulerAngles = new Vector3(0, dir, 0);
        return true;
    }
    void CreateReversi(Transform  transform)
    {
        // 판 만들기 8 x 8
        for(int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.localScale = new Vector3(0.19f, 1, 0.19f);
                go.transform.localPosition = new Vector3(0.2f * j - 0.8f, 0.0f, 0.2f * i - 0.8f);
                go.transform.parent = transform;
                var rend = go.GetComponent<Renderer>();
                rend.material = mtlBoard;
                go.name = string.Format("{0}", i * 8 + j);
                go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                go.transform.localScale = new Vector3(0.18f, 0.05f, 0.18f);
                go.transform.localPosition = new Vector3(0.12f * j - 0.8f, 0.5f, 0.2f * i - 0.8f);
                go.transform.parent = transform;
                go.name = string.Format("c{0}", i * 8 + j);
                go.SetActive(false);
            }
        }
    }
    public void UpdateItem(string name, string data)
    {
        if (name != "Reversi") return;
        for (int i = 0; i < transform.childCount; i++)
        {
            var t = transform.GetChild(i);
            if(t.name[0] == 'c')
            {
                var idx = int.Parse(t.name.Substring(1));

                if (data[idx] == '1')
                {
                    t.gameObject.SetActive(true);
                    var rend = t.gameObject.GetComponent<Renderer>();
                    rend.material = mtlList[1];
                }
                else if (data[idx] == '2')
                {
                    t.gameObject.SetActive(true);
                    var rend = t.gameObject.GetComponent<Renderer>();
                    rend.material = mtlList[2];
                }

            }
        }
    }
}
