﻿using System.Collections.Generic;
using Core.Inventory.Data;
using NUnit.Framework;
using UnityEngine;
using Random = System.Random;

namespace Tests
{
    public class InventoryTest
    {
        #region Clearing

        [Test]
        public void EmptyAfterClear()
        {
            Setup(out Inventory inventory, out Item item);

            inventory.Add(item, 1);
            inventory.Clear();

            Assert.IsTrue(inventory.IsEmpty);
            Assert.AreEqual(0, inventory.Contains(item));
            Assert.AreEqual(0, inventory.Slots.Count);
        }

        [Test]
        public void EmptyAfterClearManyItems([Random(1, 8, 5)] int amountOfItems)
        {
            Setup(out Inventory inventory, out Item[] items, amountOfItems);

            Dictionary<Item, uint> addDictionary    = new Dictionary<Item, uint>();
            Dictionary<Item, uint> removeDictionary = new Dictionary<Item, uint>();

            Random randomizer = new Random();
            Item   randomItem = items[randomizer.Next(0, items.Length - 1)];

            foreach (Item item in items)
                inventory.Add(randomItem, (uint) randomizer.Next(0, int.MaxValue / 8));

            inventory.Clear();

            Assert.IsTrue(inventory.IsEmpty);
            Assert.AreEqual(0, inventory.Slots.Count);
        }

        #endregion

        #region Adding

        [Test]
        public void AddOneItem()
        {
            Setup(out Inventory inventory, out Item item);

            inventory.Add(item, 1);

            Assert.AreEqual(1, inventory.Contains(item));
        }

        [Test]
        public void AddManyItems([Random(0u, uint.MaxValue, 5)]
                                 uint amount)
        {
            Setup(out Inventory inventory, out Item item);

            inventory.Add(item, amount);

            Assert.AreEqual(inventory.Contains(item), amount);
        }

        #endregion

        #region Removing

        [Test]
        public void RemoveOneItem()
        {
            Setup(out Inventory inventory, out Item item);

            inventory.Add(item, 1);
            inventory.Remove(item, 1);

            Assert.IsTrue(inventory.IsEmpty);
        }

        [Test]
        public void RemoveManyItems([Random(             0u, uint.MaxValue / 2, 5)]
                                    uint amount, [Random(0u, uint.MaxValue / 2, 5)]
                                    uint extra)
        {
            Setup(out Inventory inventory, out Item item);

            inventory.Add(item, amount + extra);
            inventory.Remove(item, extra);

            Assert.AreEqual(inventory.Contains(item), amount);
        }

        [Test]
        public void RemoveManyDifferentItems([Random(1, 8, 5)] int amountOfItems)
        {
            Setup(out Inventory inventory, out Item[] items, amountOfItems);

            Dictionary<Item, uint> addDictionary    = new Dictionary<Item, uint>();
            Dictionary<Item, uint> removeDictionary = new Dictionary<Item, uint>();

            Random randomizer = new Random();

            foreach (Item item in items)
            {
                addDictionary[item]    = (uint) randomizer.Next(0, int.MaxValue / 8);
                removeDictionary[item] = (uint) randomizer.Next(0, (int) addDictionary[item]);
            }

            foreach (Item item in items)
                inventory.Add(item, addDictionary[item]);

            foreach (Item item in items)
                inventory.Remove(item, removeDictionary[item]);

            foreach (Item item in items)
                Assert.AreEqual(inventory.Contains(item), addDictionary[item] - removeDictionary[item]);
        }

        #endregion

        #region Setup

        private static void Setup(out Inventory inventory, out Item item)
        {
            inventory = new Inventory();
            item      = ScriptableObject.CreateInstance<Item>();
        }

        private static void Setup(out Inventory inventory, out Item[] items, int numberOfItems)
        {
            inventory = new Inventory();
            items     = new Item[numberOfItems];

            for (int i = 0; i < items.Length; i++)
                items[i] = ScriptableObject.CreateInstance<Item>();
        }

        #endregion
    }
}