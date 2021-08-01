using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Pyramid_AI : Agent
{
    public GameObject area;
    PyramidArea m_MyArea;
    Rigidbody m_AgentRb;
    PyramidSwitch m_SwitchLogic;
    public GameObject areaSwitch;
    public Transform targetswitch;

    float timeSpan;
    float checkTime;

    public Renderer AI_Armsrd;
    public Renderer AI_Bodyrd;
    public Renderer AI_Headrd;
    public Renderer AI_Legrd;

    public Material normalMt;
    public Material GodMt;

    //public RayPerceptionSensorComponentBase[] rays;

    public override void Initialize()
    {
        m_AgentRb = GetComponent<Rigidbody>();
        m_MyArea = area.GetComponent<PyramidArea>();
        m_SwitchLogic = areaSwitch.GetComponent<PyramidSwitch>();
        timeSpan = 0.0f;
        checkTime = 3.0f;



        //rays = GetComponents<RayPerceptionSensorComponent3D>();
    }

    //public override void CollectObservations(VectorSensor sensor)
    //{
    //    sensor.AddObservation(targetswitch.localPosition);
    //}

    public void MoveAgent(ActionSegment<int> act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var action = act[0];
        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 1f;
                break;
            case 2:
                dirToGo = transform.forward * -1f;
                break;
            case 3:
                rotateDir = transform.up * 1f;
                break;
            case 4:
                rotateDir = transform.up * -1f;
                break;
        }
        transform.Rotate(rotateDir, Time.deltaTime * 200f);
        m_AgentRb.AddForce(dirToGo * 2f, ForceMode.VelocityChange);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)

    {
        AddReward(-1f / MaxStep);
        MoveAgent(actionBuffers.DiscreteActions);

        timeSpan += Time.deltaTime;

        if (timeSpan > checkTime)
        {
            AI_Armsrd.material = normalMt;
            AI_Bodyrd.material = normalMt;
            AI_Headrd.material = normalMt;
            AI_Legrd.material = normalMt;
        }


        // Ÿ�� ������ ���� ���� �ο�
        //RayPerceptionSensor ray1 = rays[0].RaySensor;
        //RayPerceptionSensor ray2 = rays[1].RaySensor;
        //RayPerceptionSensor ray3 = rays[2].RaySensor;
        //RayPerceptionOutput ray_out1 = ray1.RayPerceptionOutput;
        //RayPerceptionOutput ray_out2 = ray2.RayPerceptionOutput;
        //RayPerceptionOutput ray_out3 = ray3.RayPerceptionOutput;
        //Debug.Log(ray1.GetName());
        //foreach (RayPerceptionOutput.RayOutput ray in ray_out1.RayOutputs)
        //{
        //    if (ray.HasHit && ray.HitGameObject && ray.HitGameObject.tag == "switchOff")
        //    {
        //        AddReward(+0.0001f);
        //        //Debug.Log("OK");
        //    }
        //}

        //foreach (RayPerceptionOutput.RayOutput ray in ray_out2.RayOutputs)
        //{
        //    if (ray.HasHit && ray.HitGameObject && ray.HitGameObject.tag == "switchOff")
        //    {
        //        AddReward(+0.0001f);
        //        //Debug.Log("OK");
        //    }
        //}

        //foreach (RayPerceptionOutput.RayOutput ray in ray_out3.RayOutputs)
        //{
        //    if (ray.HasHit && ray.HitGameObject && ray.HitGameObject.tag == "switchOff")
        //    {
        //        AddReward(+0.0001f);
        //        //Debug.Log("OK");
        //    }
        //}

    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[0] = 3;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[0] = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[0] = 4;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[0] = 2;
        }
    }

    public override void OnEpisodeBegin()
    {
        var enumerable = Enumerable.Range(0, 9).OrderBy(x => Guid.NewGuid()).Take(9);
        var items = enumerable.ToArray();

        m_MyArea.CleanPyramidArea();

        m_AgentRb.velocity = Vector3.zero;
        m_MyArea.PlaceObject(gameObject, items[0]);
        transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));

        m_SwitchLogic.ResetSwitch(items[1], items[2]);
        m_MyArea.CreateStonePyramid(1, items[3]);
        m_MyArea.CreateStonePyramid(1, items[4]);
        m_MyArea.CreateStonePyramid(1, items[5]);
        m_MyArea.CreateStonePyramid(1, items[6]);
        m_MyArea.CreateStonePyramid(1, items[7]);
        m_MyArea.CreateStonePyramid(1, items[8]);


        AI_Armsrd.material = GodMt;
        AI_Bodyrd.material = GodMt;
        AI_Headrd.material = GodMt;
        AI_Legrd.material = GodMt;
        timeSpan = 0.0f;

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("switchOff"))
        {
            AddReward(2f);
            EndEpisode();
        }
        if (collision.gameObject.CompareTag("wall"))
        {
            AddReward(-0.03f);
            //EndEpisode();
        }
        if (timeSpan > checkTime && collision.gameObject.CompareTag("stone"))
        {
            AddReward(-0.5f);
            EndEpisode();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            AddReward(-0.02f);
        }
    }


}
