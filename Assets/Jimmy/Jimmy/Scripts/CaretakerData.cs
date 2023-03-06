using UnityEngine;

namespace Assets.Scripts
{
    public class CaretakerData : ScriptableObject
    {
        int health = 100;
        float moveSpeed = 3.5f;
        float rangeToAttack = 15;
        int delayToAttack = 3;
    }
}