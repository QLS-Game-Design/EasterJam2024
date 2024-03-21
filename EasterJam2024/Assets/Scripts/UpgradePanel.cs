using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class UpgradePanel : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Unfreeze time
            Time.timeScale = 1;

            // Activate player movement
            TogglePlayerMovement(true);

            // Deactivate mutation panel
            gameObject.SetActive(false);

            EnemyController.xp = 0;
            EnemyController.threshold += 1;
        }
    }

    // Toggle player movement based on the specified flag
    public void TogglePlayerMovement(bool isActive)
    {
        PlayerController playerMovement = FindObjectOfType<PlayerController>();
        if (playerMovement != null)
        {
            playerMovement.enabled = isActive;
        }
    }

    // Method to activate the mutation panel
    public void ActivateMutationPanel()
    {
        // Check if the current level is greater than or equal to the max level

        // Freeze time
        Time.timeScale = 0;

        // Deactivate player movement
        TogglePlayerMovement(true);

        // Activate mutation panel
        gameObject.SetActive(true);

    }
}
