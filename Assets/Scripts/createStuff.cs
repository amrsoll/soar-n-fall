using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createStuff : MonoBehaviour {

	public GameObject bridge;
	public GameObject plank;
	public GameObject metal;
	public GameObject nails;
	public GameObject ladder;
	public GameObject bigFlower;
	public GameObject treasureChestOpen;
	public GameObject wateringPot;
	public GameObject wateringPotFilled;

	private bool contact;
	public GameObject collidingItem;
    public NarrativeTrigger narrativeSounds;

	// Use this for initialization
	void Start () {

		contact = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.C)){
			if (this.GetComponent<moveObject>().pickedUpState && contact) {
				Combine(this.GetComponent<moveObject>().item, collidingItem);
			}
		}
	}

	void Combine(GameObject item1, GameObject item2) {

		//Create bridge
		if ((item1.name == "Plank" && item2.name == "Rope") || (item1.name == "Rope" && item2.name == "Plank")) {
			Destroy(item1);
			Destroy(item2);
			this.GetComponent<moveObject>().item = null;
			this.GetComponent<moveObject>().pickedUpState = false;

            Instantiate(bridge, new Vector3(3.0f, 5f, 12f), Quaternion.identity);

            //open new biome

            // Play a narrator sound
            narrativeSounds.PlayClip("firstBridgeNarration");
        }

		//Create plank
		if ((item1.name == "Tree" && item2.name == "Axe") || (item1.name == "Axe" && item2.name == "Tree")) {
			GameObject Aplank = Instantiate(plank, this.GetComponent<moveObject>().guide.position, this.GetComponent<moveObject>().guide.rotation);
			Aplank.name = "Plank";

        }

		//Dig
		if ((item1.name == "Shovel" && item2.name == "SoftGround") || (item1.name == "SoftGround" && item2.name == "Shovel")) {
			//take away block
		}

		//Mine metal
		if ((item1.name == "Stone" && item2.name == "PickAxe") || (item1.name == "PickAxe" && item2.name == "Stone")) {
			GameObject Ametal = Instantiate(metal, this.GetComponent<moveObject>().guide.position, this.GetComponent<moveObject>().guide.rotation);
			Ametal.name = "Metal";	
		}

		//Forging nails
		if ((item1.name == "Metal" && item2.name == "Fire") || (item1.name == "Fire" && item2.name == "Metal")) {
			GameObject Anail = Instantiate(nails, this.GetComponent<moveObject>().guide.position, this.GetComponent<moveObject>().guide.rotation);
			Anail.name = "Nails";
		}

		//Create ladder
		if ((item1.name == "Plank" && item2.name == "Nails") || (item1.name == "Nails" && item2.name == "Plank")) {
			GameObject Aladder = Instantiate(ladder, this.GetComponent<moveObject>().guide.position, this.GetComponent<moveObject>().guide.rotation);
			Aladder.name = "Ladder";
		}

		//Open tresure chest
		if ((item1.name == "Key" && item2.name == "TreasureChest") || (item1.name == "TreasureChest" && item2.name == "Key")) {
			Destroy(item1);
			Destroy(item2);
			this.GetComponent<moveObject>().item = null;
			this.GetComponent<moveObject>().pickedUpState = false;
			GameObject AtresureChestOpen = Instantiate(treasureChestOpen, this.GetComponent<moveObject>().guide.position, this.GetComponent<moveObject>().guide.rotation);
			AtresureChestOpen.name = "TreasureChestOpen";

		}

		//Water plant
		if ((item1.name == "WateringPotFilled" && item2.name == "Flower") || (item1.name == "Flower" && item2.name == "WateringPotFilled")) {
			Instantiate(bigFlower, this.GetComponent<moveObject>().guide.position, this.GetComponent<moveObject>().guide.rotation);
			GameObject emptyWateringPot = Instantiate(wateringPot, this.GetComponent<moveObject>().guide.position, this.GetComponent<moveObject>().guide.rotation);
			emptyWateringPot.name = "WateringPot";
		
			Destroy(item1);
			Destroy(item2);
			this.GetComponent<moveObject>().item = emptyWateringPot;
			this.GetComponent<moveObject>().pickedUpState = false;
			this.GetComponent<moveObject>().pickUp();

		}

		//Filling up watering pot
		if ((item1.name == "WateringPot" && item2.name == "Water") || (item1.name == "Water" && item2.name == "WateringPot")) {
			GameObject filledWateringPot = Instantiate(wateringPotFilled, this.GetComponent<moveObject>().guide.position, this.GetComponent<moveObject>().guide.rotation);
			filledWateringPot.name = "WateringPotFilled";
		
			if (item1.name == "WateringPot") {
				Destroy(item1);
			}
			else {
				Destroy(item2);
			}
			this.GetComponent<moveObject>().item = filledWateringPot;
			this.GetComponent<moveObject>().pickedUpState = false;
			this.GetComponent<moveObject>().pickUp();
		}

		//Ride on bird
		if ((item1.name == "Flower" && item2.name == "Bird") || (item1.name == "Bird" && item2.name == "Flower")) {
			
		}

		if ((item1.name == "" && item2.name == "") || (item1.name == "" && item2.name == "")) {
			
		}

		else {
			Debug.Log("Can't combine elements");
			return;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Interactable") {
			collidingItem = other.gameObject;
			contact = true;
		}
		
	}

	void OnTriggerExit(Collider other) {
		collidingItem = null;
		contact = false;
	}
}
