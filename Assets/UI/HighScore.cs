using TMPro;
using UnityEngine;

namespace UI
{
    public class HighScore : MonoBehaviour
    {
        void Start()
        {
            GetComponent<TextMeshProUGUI>().text = "Final Score: " + Wizard.score.ToString();
        }
    }
}