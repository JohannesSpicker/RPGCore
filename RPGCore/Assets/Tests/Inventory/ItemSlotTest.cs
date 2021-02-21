using Core.Inventory.Data;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class ItemSlotTest
    {
        [Test]
        public void AddAmountOfItems([Random(0u, uint.MaxValue, 5)]
                                     uint amount)
        {
            Setup(out ItemSlot itemSlot, out Item item);

            itemSlot.Add(item, amount);

            Assert.AreEqual(itemSlot.Contains(item), amount);
            Assert.AreEqual(itemSlot.Amount,         amount);
        }

        [Test]
        public void ItemIsTheItemAdded()
        {
            Setup(out ItemSlot itemSlot, out Item item);

            itemSlot.Add(item, 1);

            Assert.AreEqual(item, itemSlot.Item);
            Assert.True(item == itemSlot.Item);
        }

        [Test]
        public void RemoveAmountOfItems([Random(0u, 100u, 5)]
                                        uint amount)
        {
            Setup(out ItemSlot itemSlot, out Item item);

            itemSlot.Add(item, amount);
            itemSlot.Remove(item, amount);

            Assert.AreEqual(itemSlot.Amount, 0);
        }

        [Test]
        public void EmptyAfterRemoving([Random(0u, 100u, 5)]
                                       uint amount)
        {
            Setup(out ItemSlot itemSlot, out Item item);

            itemSlot.Add(item, amount);
            itemSlot.Remove(item, amount);

            Assert.IsTrue(itemSlot.IsEmpty);
            Assert.AreEqual(itemSlot.Amount, 0);
            Assert.IsNull(itemSlot.Item);
        }

        [Test]
        public void EmptyAfterClear()
        {
            Setup(out ItemSlot itemSlot, out Item item);

            itemSlot.Add(item, 1);
            itemSlot.Clear();

            Assert.IsTrue(itemSlot.IsEmpty);
            Assert.AreEqual(itemSlot.Amount, 0);
            Assert.IsNull(itemSlot.Item);
        }

        private static void Setup(out ItemSlot itemSlot, out Item item)
        {
            itemSlot = new ItemSlot();
            item     = ScriptableObject.CreateInstance<Item>();
        }
    }
}