using UnityEngine;
using UnityEngine.UI;

namespace MrWatts.Internal.Utilities
{
    public sealed class FlexibleGridLayout : GridLayoutGroup
    {
        [SerializeField]
        private int _columnCount = 12;
        public int ColumnCount
        {
            get => _columnCount;
            set
            {
                _columnCount = value;
                UpdateCellSize();
            }
        }

        [SerializeField]
        private int _rowCount = 20;
        public int RowCount
        {
            get => _rowCount;
            set
            {
                _rowCount = value;
                UpdateCellSize();
            }
        }

        public override void SetLayoutHorizontal()
        {
            UpdateCellSize();
            base.SetLayoutHorizontal();
        }

        public override void SetLayoutVertical()
        {
            UpdateCellSize();
            base.SetLayoutVertical();
        }

        private void UpdateCellSize()
        {
            float x = (rectTransform.rect.size.x - padding.horizontal - (spacing.x * (ColumnCount - 1))) / ColumnCount;
            float y = (rectTransform.rect.size.y - padding.vertical - (spacing.y * (RowCount - 1))) / RowCount;

            if (startAxis == Axis.Horizontal)
            {
                constraint = Constraint.FixedColumnCount;
                constraintCount = ColumnCount;
            }
            else
            {
                constraint = Constraint.FixedRowCount;
                constraintCount = RowCount;
            }

            cellSize = new Vector2(x, y);
        }
    }
}