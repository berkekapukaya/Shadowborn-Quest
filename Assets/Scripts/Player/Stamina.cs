using System.Collections;
using SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Stamina : Singleton<Stamina>
    {
        public int CurrentStamina { get; private set; }
        
        [SerializeField] private Sprite fullStaminaImage, emptyStaminaImage;
        [SerializeField] private int timeBetweenStaminaRestore = 3;
        
        private int _startingStamina = 3;
        private int maxStamina;
        private Transform _staminaContainer;
        
        const string STAMINA_CONTAINER_TEXT = "StaminaContainer";

        protected override void Awake()
        {
            base.Awake();
            
            maxStamina = _startingStamina;
            CurrentStamina = maxStamina;
        }

        private void Start()
        {
            _staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
        }

        public void UseStamina()
        {
            CurrentStamina--;
            UpdateStaminaImages();
            StopAllCoroutines();
            StartCoroutine(RestoreStaminaRoutine());
        }
        
        public void RestoreStamina()
        {
            if (CurrentStamina < maxStamina && !PlayerHealth.Instance.IsDead)
            {
                CurrentStamina++;
            }
            UpdateStaminaImages();
        }
        
        public void ResetStaminaOnDeath()
        {
            CurrentStamina = _startingStamina;
            UpdateStaminaImages();
        }

        private IEnumerator RestoreStaminaRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenStaminaRestore);
                RestoreStamina();
            }
        }

        private void UpdateStaminaImages()
        {
            for (int i = 0; i < maxStamina; i++)
            {
                var child = _staminaContainer.GetChild(i);

                var image = child?.GetComponent<Image>();

                if (image != null) image.sprite = i <= CurrentStamina - 1 ? fullStaminaImage : emptyStaminaImage;
            }
        }
    }
}
