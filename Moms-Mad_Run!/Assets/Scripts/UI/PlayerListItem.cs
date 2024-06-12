using UnityEngine;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviour
{
    public Text playerNumberText;
    public Text playerReadyText;

    public void UpdatePlayerInfo(int playerNumber, bool isReady)
    {
        if (playerNumberText != null && playerReadyText != null)
        {
            playerNumberText.text = "Player " + playerNumber;
            playerReadyText.text = isReady ? "Ready" : "Not Ready";
        }
        else
        {
            Debug.LogWarning("PlayerNumberText or PlayerReadyText is not assigned.");
        }
    }
}
