using UnityEngine;
public class EnemyHealth : MonoBehaviour
{
    [Header("슬라임의 체력 정보")]
    public int startingHealth = 100;
    public int currentHealth;


    [Header("공격 받을 시 색 변경")]
    public float floashSpeed = 5.0f;
    public Color flashColor = new Color(1, 0, 0, 0.1f);

    [Header("죽은 이후 처리")]
    public float sinkSpeed = 1.0f;

    AudioSource playerAudio;
    //슬라임의 상태를 구분해 상황에 맞는 효과를 슬라임에게 전달하는 역할
    bool isDead;
    bool isSinking;
    bool damaged;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
