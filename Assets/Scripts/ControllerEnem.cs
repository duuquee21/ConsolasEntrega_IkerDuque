using UnityEngine;

public class ControllerEnem : MonoBehaviour
{
   public float speed = 2f;
   private Transform player;

   void Start()
   {
      player = GameObject.FindGameObjectWithTag("Player").transform;
   }

   void Update()
   {
      if (player != null)
      {
         transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * speed);
      }
   }
}
