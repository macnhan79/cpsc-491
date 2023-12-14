using System.Collections;

namespace PhoHa7.Library.UserControl.PopupTree
{
    [System.ComponentModel.Editor(typeof(ItemCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class ItemCollection : CollectionBase
    {
        public void Add(Item item)
        {
            if (item == null)
            {
                return;
            }
            this.List.Add(item);
        }

        public void Remove(int index)
        {
            if ((index > Count - 1) || (index < 0))
            {
                throw new System.IndexOutOfRangeException();
            }
            else
            {
                this.List.RemoveAt(index);
            }
        }

        public Item this[int i]
        {
            get 
            {
                if (this.List.Count == 0)
                {
                    return null;
                }
                return (Item)this.List[i]; 
            }
            set 
            {
                this.List[i] = value; 
            }
        }
    }
}