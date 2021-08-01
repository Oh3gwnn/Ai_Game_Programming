using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class myAgent : Agent
{
    private Transform tr;
    private Rigidbody rb;
    public GameObject targetTr;
    public Transform clear;
    private Transform box;

    //초기화 작업을 위해 한번 호출되는 메소드
    public Renderer floorRd;        //<== 추가
    private Material originMt;      //<== 추가
    public Material badMt;          //<== 추가
    public Material goodMt;         //<== 추가


    public override void Initialize()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        originMt = floorRd.material;  //<== 추가
        MaxStep = 3000;
    }



    //에피소드(학습단위)가 시작할때마다 호출
    public override void OnEpisodeBegin()
    {
        //물리력을 초기화
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        //에이젼트의 위치 초기화
        tr.localPosition = new Vector3(-13.85f, 14.33f, 12.16f);

        // 랜덤박스
        // for (int i=0; i < targetTr.transform.childCount; i++) {
        //     box = targetTr.transform.GetChild(i).gameObject.GetComponent<Transform>();
        //     box.localPosition = new Vector3(Random.Range(-4.0f, 4.0f)
        //                                         ,0
        //                                         ,Random.Range(-4.0f, 4.0f));            
        // }



        // foreach (var i in Enumerable.Range(0, targetTr.transform.childCount)) {
        //     Transform box = targetTr.transform.GetChild(i).gameObject.Transform;
        //     box.localPosition = new Vector3(Random.Range(-4.0f, 4.0f)
        //                                         ,14.22881f
        //                                         ,Random.Range(-4.0f, 4.0f));
        // }


        // Debug.Log(targetTr.transform.GetChild(0).gameObject);
        // targetTr.localPosition = new Vector3(Random.Range(-4.0f, 4.0f)
        //                                     ,14.22881f
        //                                     ,Random.Range(-4.0f, 4.0f));
        StartCoroutine(RevertMaterial());      //<== 추가   

    }

    IEnumerator RevertMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        floorRd.material = originMt;
    }

    //환경 정보를 관측 및 수집해 정책 결정을 위해 브레인에 전달하는 메소드
    public override void CollectObservations(Unity.MLAgents.Sensors.VectorSensor sensor)
    {
        sensor.AddObservation(clear.localPosition);     //3 (x,y,z)
        sensor.AddObservation(tr.localPosition);        //3 (x,y,z)
        sensor.AddObservation(rb.velocity.x);           //1 (x)
        sensor.AddObservation(rb.velocity.z);           //1 (z)
    }

    //브레인(정책)으로 부터 전달 받은 행동을 실행하는 메소드
    public override void OnActionReceived(float[] vectorAction) {
        //데이터를 정규화
        float h = Mathf.Clamp(vectorAction[0], -1.0f, 1.0f);
        float v = Mathf.Clamp(vectorAction[1], -1.0f, 1.0f);
        Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);
        rb.AddForce(dir.normalized * 50.0f);

        //지속적으로 이동을 이끌어내기 위한 마이너스 보상
        SetReward(-0.001f);

    }

    //개발자(사용자)가 직접 명령을 내릴때 호출하는 메소드(주로 테스트용도 또는 모방학습에 사용)
    public override void Heuristic(float[] actionsOut) {
        actionsOut[0] = Input.GetAxis("Horizontal"); //좌,우 화살표 키 //-1.0 ~ 0.0 ~ 1.0
        actionsOut[1] = Input.GetAxis("Vertical");   //상,하 화살표 키 //연속적인 값
        // Debug.Log($"[0]={actionsOut[0]} [1]={actionsOut[1]}");
    }
    void OnCollisionEnter(Collision coll) {
        if (coll.collider.CompareTag("DEADZONE"))
        {
            floorRd.material = badMt;       //<== 추가
            //잘못된 행동일 때 마이너스 보상을 준다.
            SetReward(-1.0f);
            //학습을 종료시키는 메소드
            EndEpisode();
        }

        if (coll.collider.CompareTag("Box"))
        {

            SetReward(-0.01f);

        }

        if (coll.collider.CompareTag("Finish"))
        {
            floorRd.material = goodMt;      //<== 추가
            //올바른 행동일 때 플러스 보상을 준다.
            SetReward(+200.0f);
            //학습을 종료시키는 메소드
            EndEpisode();
        }
    }
    void OnCollisionStay(Collision coll)
    {
        if(coll.collider.CompareTag("Box"))
        {

            SetReward(-0.01f);

        }
    }
    void OnCollisionExit(Collision coll)
    {
        if (coll.collider.CompareTag("Box"))
        {

            SetReward(+0.001f);

        }
    }
}