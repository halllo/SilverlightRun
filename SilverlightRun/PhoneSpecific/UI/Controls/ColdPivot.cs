using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Controls;
using SilverlightRun.Tombstoning;

namespace SilverlightRun.PhoneSpecific.UI
{
    /// <summary>
    /// Allows to hide all pivot items but the focued one and to make them visible again.
    /// </summary>
    public class ColdPivot : Pivot
    {
        List<PivotItem> _preItems;
        List<PivotItem> _postItems;

        [SurvivesTombstoning]
        public int RememberedSelectedIndex
        {
            get { return SelectedIndex; }
            set { SelectedIndex = value; }
        }

        public ColdPivot()
            : base()
        {
            _preItems = new List<PivotItem>();
            _postItems = new List<PivotItem>();
        }

        public void RememberActivePivot(IPhoneService phone)
        {
            TombstoneSurvivalEngine.SetupFor<ColdPivot>(this, phone);
        }

        public void FocusOn(int index, bool when)
        {
            if (when) FocusOn(index);
            else Unfocus();
        }

        public void FocusOn(int index)
        {
            if (index >= Items.Count) return;

            StoreItemsBeforeIndex(index);
            StoreItemsAfterIndex(index);
            ReduceToFocusedItem(index);
        }

        public void Unfocus()
        {
            RestoreItemsAfterIndex();
            RestoreItemsBeforeIndex();
        }

        private void ReduceToFocusedItem(int index)
        {
            for (int i = Items.Count - 1; i >= 0; i--) if (i != index) Items.RemoveAt(i);
        }

        private void StoreItemsAfterIndex(int index)
        {
            for (int i = index + 1; i < Items.Count; i++) _postItems.Add(Items[i] as PivotItem);
        }

        private void StoreItemsBeforeIndex(int index)
        {
            for (int i = 0; i < index; i++) _preItems.Add(Items[i] as PivotItem);
        }

        private void RestoreItemsBeforeIndex()
        {
            foreach (var item in _preItems.AsEnumerable().Reverse()) Items.Insert(0, item);
            _preItems.Clear();
        }

        private void RestoreItemsAfterIndex()
        {
            foreach (var item in _postItems) Items.Add(item);
            _postItems.Clear();
        }
    }
}
