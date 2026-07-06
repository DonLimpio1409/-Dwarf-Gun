using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI liveText;

    [Header("Alimentos")]
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moneyText.text = "Money: 0";
        liveText.text = "Live: 0";
    }

    void Update()
    {
        PlayerStats();
    }

    private void PlayerStats()
    {
        moneyText.text = "Money: " + player.GetComponent<Player>().money.ToString();
        liveText.text = "Live: " + player.GetComponent<Player>().live.ToString();
    }
}
