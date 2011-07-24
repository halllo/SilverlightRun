
namespace SilverlightRun.PhoneSpecific.UI.DynamicGrid
{
    /// <summary>
    /// These items are the view modeles for the items placed on the SelfSizedGrid.
    /// Mapping of arbitrary view models to this is done by the ToDynamicGridItemConverter.
    /// </summary>
    public class DynamicGridItem
    {
        public int GridRow { get; set; }
        public int GridColumn { get; set; }
        public int GridRowSpan { get; set; }
        public int GridColumnSpan { get; set; }
        public object DataContext { get; set; }

        public DynamicGridItem() { }
    }
}
