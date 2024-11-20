using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Item(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }



    public class ItemManager
    {
        public List<Item> items = new List<Item>();

        public void AddItem(Item item)
        {
            if (string.IsNullOrEmpty(item.Name) || !IsValidName(item.Name))
            {
                throw new ArgumentException("ten item phải là chữ");
            }

            if (item.Name.Length > 10) { 
                throw new ArgumentException("ten item không được quá 10 kí tự ");
            }

            items.Add(item);
        }

        private bool IsValidName(string name)
        {
            return name.All(char.IsLetter);
        }

        public void UpdateItem(int id, string newName)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                if (string.IsNullOrEmpty(newName) || !IsValidName(newName))
                {
                    throw new ArgumentException("ten item phải là chữ");
                }

                if (newName.Length > 10)
                {
                    throw new ArgumentException("ten item không được quá 10 kí tự ");
                }
                item.Name = newName;
            }
        }

        public void DeleteItem(int id)
        {
            items.RemoveAll(i => i.Id == id);
        }
    }

    public class TestLogITem
    {
        private ItemManager itemManager;

        [SetUp]
        public void Setup()
        {
            itemManager = new ItemManager();
        }

        [Test]
        public void AddItem_ValidItem_ShouldAddItem()
        {
            var item = new Item(1, "ValidItem");

            itemManager.AddItem(item);

            Assert.AreEqual(1, itemManager.items.Count);
            Assert.AreEqual(item, itemManager.items[0]);
        }

        [Test]
        public void AddItem_ItemNameTooLong_ShouldThrowException()
        {
            var item = new Item(1, new string('a', 51));

            Assert.Throws<ArgumentException>(() => itemManager.AddItem(item));
        }

        [Test]
        public void AddItem_ItemNameNotString_ShouldThrowException()
        {
            var item = new Item(1, "123");

            Assert.Throws<ArgumentException>(() => itemManager.AddItem(item));
        }

        [Test]
        public void UpdateItem_ValidItem_ShouldUpdateItem()
        {
            var item = new Item(1, "Name");
            itemManager.AddItem(item);
            var newName = "Name1";

            itemManager.UpdateItem(1,newName);

            Assert.AreEqual(newName, itemManager.items[0].Name);
        }

        [Test]
        public void UpdateItem_ItemNotFound_ShouldDoNothing()
        {
            var newName = "Updated Name";

            itemManager.UpdateItem(1, newName);

            Assert.AreEqual(0, itemManager.items.Count);
        }

        [Test]
        public void UpdateItem_ItemNameTooLong_ShouldThrowException()
        {
            var item = new Item(1, "Name");
            itemManager.AddItem(item);
            var newName = "NewNameToolong";

            Assert.Throws<ArgumentException>(() => itemManager.UpdateItem(1, newName));
        }

        [Test]
        public void UpdateItem_ItemNameNotString_ShouldThrowException()
        {
            var item = new Item(1, "OriName");
            itemManager.AddItem(item);
            var newName = 123;

            Assert.Throws<ArgumentException>(() => itemManager.UpdateItem(1, newName.ToString()));
        }

        [Test]
        public void DeleteItem_ExistingItem_ShouldDeleteItem()
        {
            var item = new Item(1, "ItemtoDele");
            itemManager.AddItem(item);

            itemManager.DeleteItem(1);

            Assert.AreEqual(0, itemManager.items.Count);
        }

        [Test]
        public void DeleteItem_ItemNotFound_ShouldDoNothing()
        {
            itemManager.DeleteItem(1);

            Assert.AreEqual(0, itemManager.items.Count);
        }
    }
}
