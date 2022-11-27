using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance
        {
            get;
            private set;
        }

        public int redScore, blueScore = 0;
        public TextMeshPro scoreRed, scoreBlue;
        private void Awake()
        {
            Instance = this;
        }

        public void IncreasedRedScore(int amount)
        {
            redScore += amount;
            scoreRed.text = redScore.ToString();
        }
        public void IncreasedBlueScore(int amount)
        {
            blueScore += amount;
            scoreBlue.text = blueScore.ToString();
        }
    }
}