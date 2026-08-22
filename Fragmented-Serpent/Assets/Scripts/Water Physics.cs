using UnityEngine;

public class WaterPhsiysc : MonoBehaviour
{   
   private void OnTriggerEnter(Collider other)
{
    Debug.Log("Wet: " + other.name);

    Player player = other.GetComponent<Player>();

    Debug.Log("Player component: " + player);

    if (player != null)
    {
        
        player.isSubmerged = true;
        Debug.Log("Submerged set to true");
    }
}
   private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.isSubmerged = false;

        }
      Debug.Log("Dry: " + other.name);
    }
}
