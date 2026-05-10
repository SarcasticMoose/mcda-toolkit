namespace McdaToolkit;

/// <summary>Specifies whether a criterion should be maximized or minimized.</summary>
public enum CriterionType
{
    /// <summary>Higher values are preferred (maximize).</summary>
    Benefit = -1,

    /// <summary>Lower values are preferred (minimize).</summary>
    Cost = 1
}
