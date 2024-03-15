using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemyHealth : MonoBehaviour
{
    #region Fields
    [Header("슬라임의 체력 정보")]
    public int startingHealth = 100;
    public int currentHealth;


    [Header("공격 받을 시 색 변경")]
    public float flashSpeed = 5.0f;
    public Color flashColor = new Color(1, 0, 0, 0.1f);

    [Header("죽은 이후 처리")]
    public float sinkSpeed = 1.0f;

    AudioSource audioSource;
    //슬라임의 상태를 구분해 상황에 맞는 효과를 슬라임에게 전달하는 역할
    bool isDead;
    bool isSinking;
    bool damaged;
    #endregion

    // Use this for initialization
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //데미지 받을 경우 색 변경
        if (damaged)
        {
            //Slime - > Model에 접근
            transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", flashColor);
        }
        //그게 아니면 다시 자연스럽게 색이 변환될수있도록 처리
        //Color.Larp(A,B);  //A컬러를 B컬러로 천천히 바꿈
        else
        {
            transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.Lerp(transform.GetChild(0).
                GetComponent<Renderer>().material.GetColor("_Color"), Color.white, flashSpeed * Time.deltaTime));
        }
        //데미지 처리를 비활성화
        damaged = false;

        //싱크 처리 시 스르라임을 아래로 서서히 내려가게 연출합니다
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    /// <summary>
    /// 슬라임이 플레이어로부터 공격을 받았을 때의 상황을 처리하는 함수
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        //죽음 처리가 안됐고, 체력이 0보다 작은 경우 죽음 판정
        if (currentHealth <= 0 && !isDead)
        {
            Death();

        }
    }
    /// <summary>
    /// 슬라임이 플레이어로 부터 공격을 밭았을 때 넉백 효과 연출
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="playerPosition"></param>
    /// <param name="delay"></param>
    /// <param name="pushback"></param>
    /// <returns></returns>
    public IEnumerator StartDamage(int damage, Vector3 playerPosition, float delay, float pushback)
    {
        yield return new WaitForSeconds(delay);

        //try는 예외 상황이 발생할 수 있는 코드에 작성해주는 예외 처리문
        try
        {
            TakeDamage(damage);
            Vector3 diff = playerPosition - transform.position;
            diff /= diff.sqrMagnitude;
            GetComponent<Rigidbody>().AddForce((transform.position - new Vector3(diff.x, diff.y, 0.0f)) * 50 * pushback);
        }
        catch (MissingReferenceException e)
        {
            Debug.LogError(e.ToString());
        }
    }
    /// <summary>
    /// 슬라임의 죽음 시 호출할 함수
    /// </summary>
    void Death()
    {
        isDead = true;
        StageController.instance.AddPoint(10);

        transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = true;

        //에니메이션 트리거 작동
        //죽을 때 사용할 클립으로 변경 후 오디오 실행
        StartSinking();

    }

    /// <summary>
    /// 사 후 처리
    /// </summary>
    public void StartSinking()
    {
        //NavMeshAgent 비활성화
        GetComponent<NavMeshAgent>().enabled = false;

        //외부에서 가해지는 물리적인 힘에 반응하지 않겠습니다.
        GetComponent<Rigidbody>().isKinematic = true;

        isSinking = true;

        Destroy(gameObject, 5.0f);
    }
}
