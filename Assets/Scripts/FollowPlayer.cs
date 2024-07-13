using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    [SerializeField]
    Transform player;  

    private Vector3 offset;   

    void Start() {
        
        offset = transform.position - player.position;
    }

    void LateUpdate() {
       
        Vector3 newPosition = new Vector3(player.position.x + offset.x, transform.position.y, player.position.z + offset.z);

        
        transform.position = newPosition;
    }
}
