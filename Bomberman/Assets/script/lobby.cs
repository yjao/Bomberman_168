﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System.Net;

public class lobby : MonoBehaviour 
{
	public Text[] ptxt = new Text[4];
	public static string[] lists = new string[4];
	public Client c;
	public static bool ready = false;
	public static bool loaded = false;
	// Use this for initialization
	void Start () 
	{
		lists[0] = "not ready";
		lists[1] = "not ready";
		lists[2] = "not ready";
		lists[3] = "not ready";
	}

	//method reacts to the button press, will send a message to the server say it's ready
	//Server should have a record of all players
	public void readyup()
	{
		ready = !ready;
		Debug.Log((ready? "ready" : "not ready"));
		if(ready)
		{
			c.sendmsg("This player is ready");
		}
	}

	public static void setup(string p,int index)
	{
		lists[index] = p;
		Debug.Log(lists[index]);
	}
	public void updatetxt()
	{
		ptxt[0].text = lists[0];
		ptxt[1].text = lists[1];
		ptxt[2].text = lists[2];
		ptxt[3].text = lists[3];
	}

	void FixedUpdate () 
	{
		updatetxt();
	}
	/*
	 * Client connects to server on login
	 * Gets to lobby page, client data is intact
	 * Server sends back login success and its position in the player array
	 */

}