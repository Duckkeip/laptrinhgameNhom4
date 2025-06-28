using UnityEngine;

public class WaterSkillEffect : MonoBehaviour
{
    void Start()
    {
        Animator anim = GetComponent<Animator>();
        float clipLength = anim.runtimeAnimatorController.animationClips[0].length;
        Destroy(gameObject, clipLength);
    }
}