using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "RPG/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyID;
    public string displayName;
    public int maxHP;
    public int attack;
    public Sprite sprite;
    public Vector3 scale = new Vector3(2.2f, 1f, 1f); // <- THÊM DÒNG NÀY

}
