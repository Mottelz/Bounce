using TMPro;
using UnityEngine;

namespace Bounce
{
    public class GUIController : MonoBehaviour
    {
        public static GUIController Instance { get; private set; }
        
        public TMP_Text forceText;
        public TMP_Text helpText;
        public TMP_Text lifeText;
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
        
        public void UpdateLife(float lifeRemaining)
        {
            string lifeType = GameController.Instance.mode == GameMode.Speed ? "Time" : "Shots";   
            lifeText.text = $"{lifeType} Left: {lifeRemaining:0.00}";
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