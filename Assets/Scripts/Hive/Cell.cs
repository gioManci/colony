﻿using UnityEngine;
using System.Collections;
using Colony;
using Colony.UI;

[RequireComponent(typeof(Selectable))]
[RequireComponent(typeof(Aged))]
public class Cell : MonoBehaviour {

	public enum State { Storage, CreateEgg, Refine };

	public GameObject eggSprite, storageSprite, refineSprite;
	public int eggSpawnTimeout = 30; // seconds
	//public Aged aged;

	private State state = State.Storage;

	public State CellState {
		get { return state; }
		set { state = value; }
	}

	// Use this for initialization
	void Start() {
		var sel = gameObject.GetComponent<Selectable>();
		sel.OnSelect += selectCell;
		sel.OnDeselect += deselectCell;
		//aged = gameObject.GetComponent<Aged>();
		//aged.DestroyOnExpire = false;
	}

	public void CreateEgg() {
		storageSprite.SetActive(false);
		refineSprite.SetActive(false);
		eggSprite.SetActive(true);
		state = State.CreateEgg;
		//aged.Age = eggSpawnTimeout;
		//aged.gameObject.SetActive(true);
		Invoke("spawnBee", eggSpawnTimeout);
	}

	public void UseAsStorage() {
		storageSprite.SetActive(true);
		refineSprite.SetActive(false);
		eggSprite.SetActive(false);
		//aged.gameObject.SetActive(false);
		state = State.Storage;
	}

	public void Refine() {
		storageSprite.SetActive(false);
		refineSprite.SetActive(true);
		eggSprite.SetActive(false);
		//aged.gameObject.SetActive(false);
		state = State.Refine;
	}

	private void selectCell() {
		UIController.Instance.SetHiveButtonsVisible(true);
	}

	private void deselectCell() {
		UIController.Instance.SetHiveButtonsVisible(false);
	}

	private void spawnBee() {
		//aged.gameObject.SetActive(false);
		// TODO: spawn bee
	}
}