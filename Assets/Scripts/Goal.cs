using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] World world;

    private void Update()
    {
        if (player.transform.position == transform.position)
        {
            world.ObjectiveFulfilled();
        }
    }


}
