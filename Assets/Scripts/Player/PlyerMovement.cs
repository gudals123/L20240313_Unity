using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//Animator�� ���� �䱸(�ݵ�� �ʿ�)
//���� ��� �ڵ� �߰�.
//Animator�� �����Ϳ��� ������Ʈ ������ �� ����.
//���࿡ Ainmator ������Ʈ�� ���ٸ� ���� �������� ���� �ʴ´�.
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    //���� ������Ʈ ���� ����Ǿ��ִ� Animator ������Ʈ�� �����ͼ� ����Ѵ�,
    protected Animator avatar;
    float h;
    float v;

    //�ִϸ��̼� ����� �ð� üũ�� ����
    float lastAttackTime;
    float lastSkillTime;
    float lastDashTime;

    [Header("Animation Condition Flag")]
    public bool attacking = false;
    public bool dashing = false;



    void Start()
    {
        avatar = GetComponent<Animator>();


    }
    /// <summary>
    /// ���� ��Ʈ�ѷ����� ��Ʈ�ѷ��� ������ �Ͼ ��� ȣ���� �Լ�
    /// </summary>
    /// <param name="stickPos">��ƽ�� ��ǥ</param>
    public void OnStickChanged(Vector2 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;
    }

    void Update()
    {
        if (avatar)
        {
            float back = 1.0f;
            if (v < 0.0f)
            {
                back = -1.0f;
            }

            avatar.SetFloat("Speed", (h * h + v * v));
            Rigidbody rigidbody = GetComponent<Rigidbody>();

            if (rigidbody)
            {
                if (h != 0.0f && v != 0.0f)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(h, 0.0f, v));
                }
            }
        }
    }

    #region EventTrigger
    public void OnAttackDown()
    {
        attacking = true;
        avatar.SetBool("Combo", true);
        StartCoroutine(StartAttack());      //�ڸ�ƾ�� �۵���Ű�� �ڵ�
        //�ڷ�ƾ�� update�� �ƴ� �������� �ݺ������� �ڵ尡 ����Ǿ�� �� �� ����ϸ� �ſ� ȿ�����Դϴ�.
        //Update���� ���к��ϰ� ������ �ڵ带 �ڷ�ƾ���� ��ȯ�ϸ�, �ڿ� ������ ȿ�����Դϴ�.
        //�ڷ�ƾ�� ���� �ð� ���߰� �ڿ� �����̴� �۾�, Ư�� ������ �ο��� �ڵ带 �����ϴ� �۾��� �����մϴ�.
        //�ڷ�ƾ�� IEnumerator ������ �Լ��� �����մϴ�.
        //�ش� �Լ� ���ο��� �ݵ�� yield return���� �����ؾ� �մϴ�.

        //���� �Լ��δ� Invoke�� ������, �̰� ���״�� �� �ð���ŭ ���� �� �Լ� �����̶� �ڷ�ƾ���� �ణ �ٸ�.
        //�ڷ�ƾ�� �ݺ� ��ƾ���� Ż���ϰ� �ٽ� �� �������� ���ƿ��� ���� �����մϴ�.

        //StartCoroutine("StartAttack");
    }
    public void OnAttackUp()
    {
        avatar.SetBool("Combo", false);
        attacking = false;
    }

    //yield���� ���� ��Ҹ� �����ϴ� Ű�����Դϴ�.
    IEnumerator StartAttack()
    {
        if(Time.time - lastAttackTime > 1.0f)
        {
            lastAttackTime = Time.time;
            while(attacking)
            {
                avatar.SetTrigger("AttackStart");
                yield return new WaitForSeconds(1.0f);
            }
        }
    }

    /// <summary>
    /// ��ư 2�� ������ ���� ��ų
    /// </summary>
    public void OnSkillDown()
    {
        if (Time.time - lastAttackTime > 1.0f)
        {
            avatar.SetBool("Skill", true);
            lastSkillTime = Time.time;
        }
    }
    public void OnSkillUp()
    {
        avatar.SetBool("Skill", false);
    }

    /// <summary>
    /// ��ư 1�� ������ ���� ��ų
    /// </summary>
    public void OnDashDown()
    {
        if (Time.time - lastAttackTime > 1.0f)
        {
            lastDashTime = Time.time;
            dashing = true;
            avatar.SetTrigger("Dash");
        }
    }
    public void OnDashUp()
    {
        dashing = false;
    }

    #endregion



}
