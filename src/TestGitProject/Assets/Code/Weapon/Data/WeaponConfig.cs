using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Data/Scriptable Objects/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private float _force;
        [SerializeField] private int _clipSize;
        [SerializeField] private float _shotDelay;

        public float Force => _force;
        public int ClipSize => _clipSize;
        public float ShotDelay => _shotDelay;
    }
}