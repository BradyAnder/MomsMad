using System.Collections.Generic;
using UnityEngine;

public class PlayerListManager : MonoBehaviour
{
    public GameObject playerListItemPrefab;
    private List<GameObject> playerListItems = new List<GameObject>();

    void Update()
    {
        var players = LobbyManager.Instance.GetPlayers();

        while (playerListItems.Count < players.Count)
        {
            var newListItem = Instantiate(playerListItemPrefab, transform);
            playerListItems.Add(newListItem);
        }

        for (int i = 0; i < playerListItems.Count; i++)
        {
            var listItem = playerListItems[i].GetComponent<PlayerListItem>();
            if (i < players.Count)
            {
                listItem.gameObject.SetActive(true);
                listItem.UpdatePlayerInfo(i + 1, players[i].isReady);

                // Set Location
                RectTransform rectTransform = listItem.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(0, -i * 50); // 30 height for each person
            }
            else
            {
                listItem.gameObject.SetActive(false);
            }
        }
    }
}
