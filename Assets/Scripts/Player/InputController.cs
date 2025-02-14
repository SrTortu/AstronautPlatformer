using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    #region Fields

    [SerializeField] private Jetpack _jetpack;
    [SerializeField] private Player _player;
    private Vector3 mousePosition;

    #endregion

    #region Unity Callbacks

    void Start()
    {
    }


    void Update()
    {
        if (!_player.isDamage)
        {
            //Horizontal Fly
            if (Input.GetAxis("Horizontal") < 0)
            {
                _player.FlipPlayer(Player.Direction.Left);
                _jetpack.FlyHorizontal(Jetpack.Direction.Left);
            }

            if (Input.GetAxis("Horizontal") > 0)
            {
                _player.FlipPlayer(Player.Direction.Right);
                _jetpack.FlyHorizontal(Jetpack.Direction.Right);
            }

            //Vertical Fly
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jetpack.FlyUp();
                _player.playerGun.DetachHook();
            }
            else
            {
                _jetpack.StopFlying();
            }


            //Aim
            mousePosition =
                Camera.main.ScreenToWorldPoint(Input.mousePosition); //Captura la posicion del mouse en el juego
            if (mousePosition.x >
                _player.transform.position.x) //Verifica si la posicion del mouse esta a la derecha del player
            {
                _player.FlipPlayer(Player.Direction
                    .Left); //Visualmente cambia la direccion del player relativamente a la posicion del mouse
                _player.playerGun.Aim(mousePosition); //El arma apunta a la posisicion relativa del mouse
            }

            else if
                (mousePosition.x <
                 _player.transform.position.x) //Verifica si la posicion del mouse esta a la izquierda del player
            {
                _player.FlipPlayer(Player.Direction.Right);
                _player.playerGun.Aim(mousePosition);
            }

            //Shoot
            if (Input.GetMouseButtonDown(0))
            {
                _player.Shoot(mousePosition);
            }

            if (Input.GetMouseButtonDown(1))
            {
                _player.playerGun.DetachHook();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_player.isDamage)
        {
            //Walk
            if ((Input.GetAxis("Vertical") == 0) && (Input.GetAxis("Horizontal") < 0))
            {
                _player.Walk(Player.Direction.Left);
                _player.WalkOn();
            }

            if ((Input.GetAxis("Vertical") == 0) && (Input.GetAxis("Horizontal") > 0))
            {
                _player.Walk(Player.Direction.Right);
                _player.WalkOn();
            }

            if (Input.GetAxis("Horizontal") == 0 || (Input.GetAxis("Vertical") > 0))
            {
                _player.WalkOff();
            }
        }
    }

    #endregion
}