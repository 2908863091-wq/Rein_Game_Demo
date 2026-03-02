using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator anime;
    [SerializeField] private Player player;

    private const string Is_Walking = "is_walking";

    // Start is called before the first frame update
    void Start()
    {
       anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anime.SetBool(Is_Walking,player.IsWalking);
    }
}
