using UnityEngine;
using UnityEngine.UI;

public class LayoutSetting : MonoBehaviour
{
    public int rowCount = 1;
    public int columnCount = 10;

    private GridLayoutGroup gridLayout;

    private void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        AdjustConstraints();
    }

    private void AdjustConstraints()
    {
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = columnCount;
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        gridLayout.constraintCount = rowCount;
    }
}