using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;

public class Main: MonoBehaviour
{

    /* 아바타를 위한 */
    /*  spawn 으로 이
    Avatar myAvatar;
    float speed;
    float aspeed; // 회전
    float direction; // z 측을 기준으로 해서 시계 방향으로 얼만큼 회전 해서 바라보는 방향
    
    GameObject nameGo; // 아바타 이름
     */

    /* 카메라를 위한 */
    float distance = 5.0f; // 카메라 거리 비율
    float zoom; // 카메라
    float camoffset; // 켐 내가 회전 시키기
    Vector2 lastMousePos; // 마우스 마즈막 위치


    /* 통신을 위한  */
    SocketDesc socketDesc;  //  소켓 디스크립터

    /* 스폰을 위한 */
    Dictionary<string, Spawn> spawns;
    Spawn mySpawn;

    // Start is called before the first frame update
    void Start()
    {
        // 소켓 만들기
        socketDesc = SocketDesc.Create();


        // spawn들을 관리할 spawns를 생성합니다.
        spawns = new Dictionary<string, Spawn>();


    }

    // Update is called once per frame
    void Update()
    {

        //  현재 아바타가 생성되어 있지 않았다면 아무 작업 안 하도록 합니다.
        if (mySpawn == null) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            mySpawn.speed = 1.0f;
            SendMove();
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            mySpawn.speed = 0.0f;
            SendMove();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            mySpawn.speed = -0.5f;
            SendMove();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            mySpawn.speed = 0.0f;
            SendMove();
        }
        //  A키가 눌리면 좌회전, D키가 눌리면 우회전
        //  t 변수에 한 프레임의 시간 (초단위)를 저장
        if (Input.GetKeyDown(KeyCode.A))
        {
            mySpawn.aspeed = -90.0f;
            SendMove();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            mySpawn.aspeed = 0.0f;
            SendMove();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            mySpawn.aspeed = 90.0f;
            SendMove();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            mySpawn.aspeed = 0.0f;
            SendMove();
        }

        /* unity Spawn 에 맞게 수
        if(Input.GetKeyDown(KeyCode.W))
        {
            speed = 1.0f;
            myAvatar.Walk();
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            speed = 0.0f;
            myAvatar.Stand();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            speed = -0.5f;
            myAvatar.Stand();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            speed = 0.0f;
            myAvatar.Stand();
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            aspeed = -90.0f;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            aspeed = 0.0f;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            aspeed = 90.0f;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            aspeed = 0.0f;
        }
        */

        // 마우스 스크롤 이벤트
        zoom += Input.mouseScrollDelta.y * 0.1f;
        if (zoom < -1.0f) zoom = -1.0f; else if (zoom > 1.0f) zoom = 1.0f;

        // 마우스 버튼 1이 클릭된 상태에서 팬 처리
        if (Input.GetMouseButtonDown(1)) lastMousePos = Input.mousePosition;
        else if (Input.GetMouseButtonUp(1)) lastMousePos = Vector2.zero;
        if (lastMousePos != Vector2.zero)
        {

            camoffset += Input.mousePosition.x - lastMousePos.x;
            lastMousePos = Input.mousePosition;
        }
        else
        {
            if(camoffset < 0.0f)
            {
                camoffset += 0.1f;
                if (camoffset > 0.0f) camoffset = 0.0f;
            }
            else if(camoffset >0.0f)
            {
                camoffset -= 0.1f;
                if (camoffset < 0.0f) camoffset = 0.0f;

            }
        }

        /* Spawn 으로 이동
        // t 변수에 ㅎ한 프레임의 시간 (초단위 )를 저장
        var t = Time.deltaTime;
        // 회전 에 대한 곱 계산
        direction += t * aspeed;
        myAvatar.transform.localEulerAngles = new Vector3(0, direction, 0);

        //위치 이동
        var rad = direction * Mathf.PI / 180.0f;
        Vector3 dirv = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
        myAvatar.transform.localPosition += dirv * t * speed;
        */

        // 카메라 위치 수정 Spawn 에 맞게 수정
        var camd = distance*Mathf.Pow(2.0f, zoom);
        var rad = Mathf.Deg2Rad * (mySpawn.direction + camoffset);
        var cdirv = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
        Camera.main.transform.localPosition = mySpawn.transform.localPosition -
            cdirv * camd * Mathf.Cos(30*Mathf.PI/180.0f) +
            (new Vector3(0, camd*Mathf.Sin(30*Mathf.PI/180.0f)+1.8f, 0));
        Camera.main.transform.localEulerAngles = new Vector3(30.0f, (mySpawn.direction + camoffset), 0);


        /* Spawn 으로 이
        // 이름판이 항상 카메라를 바라보도록 회전한다.
        // LookAT(바라보는 방향 종이의 위치, 바라보는 방향의 각도 종이의 회 ) billboard라 부름 
        nameGo.transform.LookAt(nameGo.transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);
        */

    }


    private void FixedUpdate()
    {
        // 1. 소켓 디스크립터가 존재하지 않으면 아무것도 안하기
        if (socketDesc == null) return;
        // 2. processNetwork가 true가 아니라면 아무것도 안하기
        if (!socketDesc.ProcessNetwork()) return;
        // 3. 패킷가져오기
        var packet = Encoding.UTF8.GetString(socketDesc.GetPacket());
        Debug.Log(packet);

    }



    // 접속 버튼이 눌렸을 때의 작업
    public void OnbuttonConnect()
    {
        // 1. Find "LoginWindow"
        var loginWindow = GameObject.Find("LoginWindow");
        // 2. Find "InputField"
        var name = loginWindow.transform.Find("InputField").GetComponent<InputField>();
        // 3. Print result
        Debug.LogFormat("Connet with {0}.", name.text);
        // 4. Hide Login window
        loginWindow.SetActive(false);


        ////  Avatar 올려놓기
        ////  1. Resources 폴더에 있는 아바타 모델을 가져오기
        //var res = Resources.Load("Female/female", typeof(GameObject)) as GameObject;
        ////  2. Scene에 Instance로 만들기
        //var go = GameObject.Instantiate(res);
        ////  3. Instance화된 아바타의 이름을 바꿔보자.
        //go.name = name.text;
        ////  4. Animator Controller를 로드하자.
        //var ac = Resources.Load("Female/female", typeof(RuntimeAnimatorController))
        //    as RuntimeAnimatorController;
        ////  5. Avatar로부터 애니메이터 컨트롤러를 가져옵니다.
        //var animControl = go.GetComponent<Animator>();
        ////  6. Avatar의 애니메이터 컨트롤러를 위에서 로드한 컨트롤러로 설정
        //animControl.runtimeAnimatorController = ac;
        ////  7. Avatar의 애니메이션을 설정합니다.
        //animControl.SetInteger("animation", 0);

        //  1. Make a Empty Game object
        var go = new GameObject(name.text);
        //  2. Avatar component 추가하기
        mySpawn = go.AddComponent<Spawn>();
        //  3. Avatar 생성하기
        var model = UnityEngine.Random.Range(0, 2);
        mySpawn.CreateAvatar(name.text, UnityEngine.Random.Range(0, 2));
        var hair = UnityEngine.Random.Range(0, 4);
        var body = UnityEngine.Random.Range(0, 4);
        var legs = UnityEngine.Random.Range(0, 4);
        var shoes = UnityEngine.Random.Range(0, 4);
        mySpawn.ChangeLook(UnityEngine.Random.Range(0, 4), UnityEngine.Random.Range(0, 4), UnityEngine.Random.Range(0, 4), UnityEngine.Random.Range(0, 4));
        /* Spawn 으로 잉동ㅡ
        // 이름판 만들기
        nameGo = new GameObject("Text");
        var nameText = nameGo.AddComponent<TextMesh>();
        nameText.text = name.text;

        // 아바타 오브젝트에 이름판 붙이기
        nameGo.transform.parent = go.transform;
        nameGo.transform.localPosition = new Vector3(0, 1.9f, 0);
        nameGo.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        nameText.anchor = TextAnchor.MiddleCenter;
        */



        // 접속하기
        if (socketDesc.Connect("127.0.0.1", 8888))
        {
            Debug.Log("Connected");
            socketDesc.Send(Encoding.UTF8.GetBytes(string.Format("join {0}", name.text)));
            socketDesc.Send(Encoding.UTF8.GetBytes(string.Format("avatar {0} {1}", name.text, model)));
            socketDesc.Send(Encoding.UTF8.GetBytes(string.Format("look {0} {1} {2} {3} {4}", name.text, hair, body, legs, shoes)));
            spawns[name.text] = mySpawn;

        }
        else
        {
            Debug.LogError("Connection is failed");
        }

    }

    void SendMove()
    {
        var mesg = string.Format("move {0} {1} {2} {3} {4} {5}",
            mySpawn.name, mySpawn.transform.localPosition.x, mySpawn.transform.localPosition.z,
            mySpawn.direction, mySpawn.speed, mySpawn.aspeed);
        socketDesc.Send(Encoding.UTF8.GetBytes(mesg));

    }
}
