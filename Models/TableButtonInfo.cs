using System.Drawing;

namespace RestoranRezervasyonSistemi.Models
{
    public class TableButtonInfo
    {
        public Table Table { get; set; }
        public Color ButtonColor { get; set; }
        public string TooltipText { get; set; }
        public bool IsSelectable { get; set; }
        public string ReservationWarningText { get; set; }

        public TableButtonInfo(Table table, Color buttonColor, string tooltipText, bool isSelectable, string reservationWarningText)
        {
            Table = table;
            ButtonColor = buttonColor;
            TooltipText = tooltipText;
            IsSelectable = isSelectable;
            ReservationWarningText = reservationWarningText;
        }
    }
}
