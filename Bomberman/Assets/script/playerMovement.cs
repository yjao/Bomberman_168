﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerMovement : MonoBehaviour {

	public float speed;
	Rigidbody rb, bomb, clone;
	public Vector3 movement;
	public int timer;
	public int dropTime;
	public int strength;
	public int playerid, clientid;
	public Client client;
	public bool active = false;
	public bool alive = true;
	string mydata;
	List<Game.Player> allplayer = Client.game.allPlayers;

	int times;
	void Start() {
		rb = GetComponent<Rigidbody>();
		clientid = Client.getIndex();
	}

	void Update() {

		if (Input.GetButtonDown("Fire1") && !active && playerid == clientid && timer >= dropTime) {
			timer -= dropTime;
			Client.lazySend("Bomb;" + rb.position[0].ToString() + ";" + rb.position[2].ToString() + ";" + strength.ToString() + ";"+client.getlobbyname()+";");
			//Client.lazySend(...)
		}
		if (timer < 100) {
			timer++;;
		}
		Debug.Log (timer);
	}
	
	void FixedUpdate () {


		if (playerid == clientid && alive) {
			//client.GetComponent<Client> ().x = rb.position [0];
			//client.GetComponent<Client> ().z = rb.position [2];
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.AddForce (movement * speed);

			string index = playerid.ToString ();
			string xs = rb.position [0].ToString ();
			string zs = rb.position [2].ToString ();
			times++;
			if (times > 10) {
				Debug.Log ("Player;" + index + ";T;" + xs + ";" + zs + ";" + rb.velocity.x + ";" + rb.velocity.y + ";"+client.getlobbyname()+";");
				Client.lazySend ("Player;" + index + ";T;" + xs + ";" + zs + ";" + rb.velocity.x + ";" + rb.velocity.y + ";"+client.getlobbyname()+";");
				times = 0;
			}
		} else if (clientid == playerid && !alive) {
			string index = playerid.ToString ();
			string xs = rb.position [0].ToString ();
			string zs = rb.position [2].ToString ();
			Debug.Log ("Player;" + index + ";F;" + xs + ";" + zs + ";" + rb.velocity.x + ";" + rb.velocity.y + ";"+client.getlobbyname()+";");
			Client.lazySend ("Player;" + index + ";F;" + xs + ";" + zs + ";" + rb.velocity.x + ";" + rb.velocity.y + ";"+client.getlobbyname()+";");
			this.gameObject.SetActive (false);
		} else {
			rb.useGravity = false;

			if (allplayer.Count >= playerid + 1 && allplayer[playerid].active){
				float locx = allplayer[playerid].x;
				float locz = allplayer[playerid].z;
				// Jeffrey this is for the velocity movement on dead reckoning
				float velx = allplayer[playerid].xv;
				float velz = allplayer[playerid].zv;
				transform.position = new Vector3 (locx, .5f, locz);
				movement = new Vector3 (velx, 0.0f, velz);
				rb.velocity = (movement * speed);
			}
			else {
				this.gameObject.SetActive (false);
			}
		}
	}


	void OnApplicationQuit()
	{
		this.gameObject.SetActive(false);
		this.alive = false;
	}

}


