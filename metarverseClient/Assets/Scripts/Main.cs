using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main: MonoBehaviour
{
    Avatar myAvatar;
    float speed;
    float aspeed; // 회전
    float direction; // z 측을 기준으로 해서 시계 방향으로 얼만큼 회전 해서 바라보는 방향
    float distance = 5.0f; // 카메라 거리 비율
    float zoom = 0.0f; // 카메라
    float camoffset; // 켐내가 회전 시키기
    Vector2 lastMousePos; // 마우스 마즈막 위치
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (myAvatar == null) return;
        if (Input.GetKeyDown(KeyCode.Alpha0)) myAvatar.Stand();
        else if (Input.GetKeyDown(KeyCode.Alpha1)) myAvatar.Walk();
        else if (Input.GetKeyDown(KeyCode.Alpha2)) myAvatar.Work();
        else if (Input.GetKeyDown(KeyCode.Alpha3)) myAvatar.Sit();

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

        // 마우스 스크롤 이벤트
        zoom += Input.mouseScrollDelta.y * 0.1f;
        if (zoom < -1.0f) zoom = -1.0f; else if (zoom > 1.0f) zoom = 1.0f;
        // 마우스 버튼 1이 클릭된 상태에서 팬 처리
        if (Input.GetMouseButtonDown(0)) lastMousePos = Input.mousePosition;
        else if (Input.GetMouseButtonUp(0)) lastMousePos = Vector2.zero;
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
        // t 변수에 ㅎ한 프레임의 시간 (초단위 )를 저장
        var t = Time.deltaTime;
        // 회전 에 대한 곱 계산
        direction += t * aspeed;
        myAvatar.transform.localEulerAngles = new Vector3(0, direction, 0);

        //위치 이동
        var rad = direction * Mathf.PI / 180.0f;
        Vector3 dirv = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
        myAvatar.transform.localPosition += dirv * t * speed;

        // 카메라 위치
        var camd = distance + zoom;
        rad = Mathf.Deg2Rad * (direction + camoffset);
        var cdirv = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
        Camera.main.transform.localPosition = myAvatar.transform.localPosition -
            dirv * distance*Mathf.Cos(30*Mathf.PI/180.0f) +
            (new Vector3(0, camd*Mathf.Sin(30*Mathf.PI/180.0f)+1.8f, 0));

        Camera.main.transform.localEulerAngles = new Vector3(30.0f, (direction + camoffset), 0);


    }


    // 접속 버튼이 눌렸을 때의 작업
    public void OnbuttonConnect()
    {
        // 1. Find "LoginWindow"
        var loginWindow = GameObject.Find("LoginWindow");
        // 2. Find "InputField"
        var name = loginWindow.transform.Find("InputField").GetComponent<InputField>();
        // 3. Print result
        Debug.LogFormat("Connet with {0} {1}.", name.text, loginWindow);
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

        // Avatar 올려놓기
        // 1. Make a Empty Game object
        var go = new GameObject(name.text);
        myAvatar = go.AddComponent<Avatar>();
        myAvatar.Create(Random.Range(0,2));
        
    }
}
