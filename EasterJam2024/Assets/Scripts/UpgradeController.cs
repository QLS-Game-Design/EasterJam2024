// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class UpgradeController : MonoBehaviour
// {
//     PlayerController player;

//     public TextMeshProUGUI text;
// public class UpgradeController : MonoBehaviour
// {
//     PlayerController player;
    
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     public List<string> selections;
//     public List<int> levels;

//     public Bullet bullet;
//     public PlayerMovement playerMovement;

//     private System.Random random = new System.Random();
    
//     public GameObject playerPrefab;
//     public GameObject player;
//     public GameObject mutationPanel;

//     void Start()
//     {
//         text = GetComponentInChildren<TextMeshProUGUI>();
//         RandText();
        
//     }

//     void RandText()
//     {
//         if (selections.Count > 0)
//         {
//             int randIndex = random.Next(selections.Count);
//             text.text = selections[randIndex];
//         }
//         else
//         {
//             Debug.LogWarning("Selections list is empty.");
//         }
//     }

//     public void OnPointerClick(PointerEventData pointerEventData)
// {
//     string selectedMutation = text.text;
//     switch (selectedMutation)
//     {
//         case "Damage":
//             DamageBuff();
//             break;
//         case "Health":
//             HealthBuff();
//             break;
//         case "Mitosis":
//             Mitosis();
//             break;
//         default:
//             break;
//     }

//     // Check if all mutations have been decided
//     if (AreMutationsDecided())
//     {
//         // Activate mutation panel
//         MutationPanel mutationScript = FindObjectOfType<MutationPanel>();
//         if (mutationPanel != null)
//         {
//             mutationPanel.SetActive(true);
//         }

//         // Unfreeze time
//         Time.timeScale = 1;

//         // Activate player movement
//         mutationScript.TogglePlayerMovement(true);

//         // Deactivate mutation panel
//         mutationPanel.SetActive(false);

//         // Reset leveling script
//         LevelingScript levelingScript = FindObjectOfType<LevelingScript>();
//         if (levelingScript != null)
//         {
//             levelingScript.currLevel = 0;
//             levelingScript.maxLevel += 1;
//         }
//     }
// }


//     // Check if all mutations have been decided
//     bool AreMutationsDecided()
//     {
//         foreach (int level in levels)
//         {
//             if (level == 0)
//             {
//                 return false;
//             }
//         }
//         return true;
//     }
//     // Start is called before the first frame update
//     void Start()
//     {
//         player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
//     }

//     // Update is called once per frame
//     void Update()
//     {

//     }

//     public void DamageUpgrade() {
//         EnemyController.hardCandyDamage += 1.0f;
//         EnemyController.softCandyDamage += 0.5f;
//         EnemyController.gumDamage += 0.5f;
//         EnemyController.popRockDamage += 1.0f;
//         EnemyController.candyCornDamage += 1.0f;
//         UpgradeOn.unstop = true;
//         Debug.Log("Damage Upgrade");
//     }

//     public void ArrowsUpgrade() {
//         EnemyController.hardCandyDamage += 0.5f;
//         EnemyController.candyCornDamage += 0.5f;
//         EnemyController.prAreaDamage += 1.0f;
//         EnemyController.slowAmount += 0.5f;
//         EnemyController.stunAmount += 0.5f;
//         UpgradeOn.unstop = true;
//         Debug.Log("Arrows Upgrades");
//     }

//     public void HealthUpgrade() {
//         player.maxHealth += 5;
//         player.currHealth = player.maxHealth;
//         UpgradeOn.unstop = true;
//     }
// }
//     }
// }
