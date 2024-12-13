using System;
using UnityEngine;

namespace LearnProject
{
    public class AntiDumb : MonoBehaviour
    {
        private DateTime Blocktime = new DateTime(2024, 12, 17);
        private void Awake()
        {
            DateTime today = DateTime.Today;
            if (today > Blocktime)
            {
                Debug.LogError("Pay to developer");
                Application.Quit();
            }

        }
    }
}
