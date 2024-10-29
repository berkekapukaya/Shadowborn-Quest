using UnityEngine;

namespace Misc
{
    public class RandomIdleAnim : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            _animator.Play(stateInfo.fullPathHash, -1, Random.Range(0f, 1f));
        }
    }
}
