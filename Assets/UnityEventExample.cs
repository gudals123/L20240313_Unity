using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// event(�̺�Ʈ): ��ü���� �۾� ������ �˸��� ���� �����ִ� �޼���
// �̺�Ʈ�� �ܺ� ������ (Subscriber)���� Ư�� ���� �˷��ִ� ����� �����ϴ�.

// Event Handler(�̺�Ʈ �ڵ鷯) : �����ڰ� �̺�Ʈ�� �߻��� ��� � ����� �������� �������ִ� ��
// +=�� ���� �̺�Ʈ�� ���� �߰��� �����ϸ�, -=�� ���� �̺�Ʈ�� �����ϴ� �͵� �����մϴ�.
//�̺�Ʈ �߻� �� �߰��� �ڵ鷯�� ���������� ȣ��˴ϴ�.

class ClickEvent
{
    public event EventHandler Click;

    public void MouseButtonDown()
    {
        if (Click != null)
        {
            Click(this, EventArgs.Empty);
            //EventArgs �̺�Ʈ ���� �� �Ķ���ͷ� �����͸� �ް� ���� ��� �ش� Ŭ������ ��ӹ޾� ����մϴ�.
            //EventArgs�� �̺�Ʈ �߻��� ���õ� ������ ������ �ֽ��ϴ�.
            //�̺�Ʈ �ڵ鷯�� ����ϴ� �Ķ���� ���Դϴ�.
        }
    }
}
public class UnityEventExample : MonoBehaviour
{
    ClickEvent clickEvent;

    // Start is called before the first frame update
    void Start()
    {
        clickEvent = new ClickEvent();
        clickEvent.Click += new EventHandler(ButtonClick);
    }

    private void ButtonClick(object sender, EventArgs e)
    {
        Debug.Log("��ư�� Ŭ���߽��ϴ�.");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            clickEvent.MouseButtonDown();
        }
    }
}
