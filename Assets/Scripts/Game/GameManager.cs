using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TheGridMatrix
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        [Header("Game")]
        [SerializeField] private int score;
        [SerializeField] private bool paused;

        [Header("Reference")]
        [SerializeField] private Text scoreTxt = null;
        [SerializeField] private UnityEvent onEndGame = null;

        internal static void EndGame()
        {
            if (instance.onEndGame != null) instance.onEndGame.Invoke();
            Pause = true;
        }

        private void Start()
        {
            instance = this;
            paused = true;
            score = 0;
        }

        public static bool Pause { get => instance.paused; internal set { instance.paused = value; } }
        public static int Score { get => instance.score; internal set { instance.score = value; instance.scoreTxt.text = "Score: " + value.ToString(); } }
    }
}