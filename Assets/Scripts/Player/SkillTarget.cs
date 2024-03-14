using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTarget : MonoBehaviour
{

    //타겟에 대한 리스트
    public List<Collider> targetList;

    // Use this for initialization
    void Start()
    {
        targetList = new List<Collider>();
    }

    //트리거 충돌 시 리스트 명단에 타겟을 등록
    private void OnTriggerEnter(Collider other)
    {
        targetList.Add(other);
    }
    
    private void OnTriggerExit(Collider other)
    {
        targetList.Remove(other);
    }
}
