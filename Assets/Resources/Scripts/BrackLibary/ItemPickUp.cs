
using UnityEngine;

public class ItemPickUp : Interact
{

	//public Item item;   // Item to put in the inventory if picked up

	// When the player interacts with the item
	public override void Interacting()
	{
		base.Interacting();

		//PickUp();
	}

	// Pick up the item
	void PickUp()
	{
		//data.GeneralInventory.AddItem(item); // Add to inventory

	}

}