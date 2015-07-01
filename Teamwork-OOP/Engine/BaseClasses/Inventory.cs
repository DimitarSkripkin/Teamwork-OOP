using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teamwork_OOP.Engine.Items;

namespace Teamwork_OOP.Engine.BaseClasses
{
	public class Inventory
	{
		private const int InventorySize = 15;
		private List<Item> inventoryItems = new List<Item>();

		//private Item equippedWeapon;

		//private Item equippedChest;
		//private Item equippedHelm;
		//private Item equippedBoots;
		//private Item equippedGloves;
		//private Item equippedPants;

		public Inventory()
		{
			//this.EquippedWeapon = equippedWeapon;
			//this.EquippedChest = equippedChest;
			//this.EquippedHelm = equippedHelm;
			//this.EquippedBoots = equippedBoots;
			//this.EquippedGloves = equippedGloves;
			//this.EquippedPants = equippedPants;
		}

		public MeleeWeapon EquippedWeapon { get; set; }

		public Chest EquippedChest { get; set; }

		public Helm EquippedHelm { get; set; }

		public Boots EquippedBoots { get; set; }

		public Gloves EquippedGloves { get; set; }

		public Pants EquippedPants { get; set; }

		public void EquipItem(Item itemToEquip)
		{
			inventoryItems.Remove(itemToEquip);
		}

		public void UnEquipItem(Item itemToUnequip) // bool?
		{
			if (inventoryItems.Count < InventorySize)
			{
				inventoryItems.Add(itemToUnequip);
				itemToUnequip = null;
			}
			else
			{
				return;
			}
		}

		public void SwapItems(Item itemInEquipment, Item itemInInventory)
		{

		}

		public void DestroyItem(Item itemToDestroy)
		{
			inventoryItems.Remove(itemToDestroy);
		}

		public bool PickUpItem(Item itemToPick)
		{
			if (inventoryItems.Count < InventorySize)
			{
				inventoryItems.Add(itemToPick);
				return true;
			}
			return false;
		}
	}
}
