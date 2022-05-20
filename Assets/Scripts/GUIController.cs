using TMPro;
using UnityEngine;

namespace Bounce
{
    public class GUIController : MonoBehaviour
    {
        public static GUIController Instance { get; private set; }
        
        public TMP_Text forceText;
        public TMP_Text helpText;
        public TMP_Text shotsText;
        public TMP_Text scoreText;
        
        private int _score = 0;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        
        public void AddScore(int points)
        {
            _score += points;
            scoreText.text = $"Score: {_score}";
        }
        
        public void UpdateShots(int shotsRemaining)
        {
            shotsText.text = $"Shots Left: {shotsRemaining}";
        }

        public void UpdateForce(float horizontal, float vertical)
        {
            forceText.text = $"H: {horizontal:0.00}, V: {vertical:0.00}";
        }

        public void ToggleHelp()
        {
            helpText.gameObject.SetActive(!helpText.gameObject.activeSelf);
        }
    }
}