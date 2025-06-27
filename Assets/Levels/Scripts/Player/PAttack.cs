using UnityEngine;
public class PAttack : MonoBehaviour
{
    public GameObject waterColumnPrefab;
    public float duration = 2f;
    private float lastCastTime = -Mathf.Infinity;
    public float cooldown = 1f;
    private Behave behave;


       [Header("Cài đặt kỹ năng")]
    [Tooltip("Khoảng cách skill xuất hiện trước mặt nhân vật")]
    [SerializeField] public float khoangcachSkillX = 3f; // khoang cach skill 
    
    
    void Start(){
        behave = GetComponent<Behave>(); 
        //behave = FindObjectOfType<Behave>(); //nếu khác GameObject
    }

    void Update()
    {
         

        if (Input.GetKeyDown(KeyCode.F) && Behave.Instance.isGrounded() && Time.time - lastCastTime >= cooldown)
        {
            UseMainSkill();
            lastCastTime = Time.time;
        }
    }

    void UseMainSkill()
    {

        float manaCost = 0.1f; // Chi phí mana khi dùng skill


    if (PStats.instance != null && PStats.instance.UseMana(manaCost))
    {
        float direction = transform.localScale.x > 0 ? 1 : -1;
        Vector3 spawnPos = transform.position + new Vector3(khoangcachSkillX * direction, 0, 0);
        GameObject skill = Instantiate(waterColumnPrefab, spawnPos, Quaternion.identity);
        Destroy(skill, duration);
    }
    else
    {
        Debug.Log("Không đủ mana để dùng kỹ năng!");
    }
}
}