using System.Collections.Immutable;
using System.Runtime.InteropServices;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using McdaToolkit.Core;
using McdaToolkit.Core.Abstractions;
using McdaToolkit.Extensions.Algorithms.Vikor.Result;

namespace McdaToolkit.Extensions.Algorithms.Vikor;

/// <summary>
/// Initializes a new instance of the <see cref="VikorMethod{T}"/> class.
/// </summary>
/// <param name="methodParameters">The parameters for the Vikor method.</param>
internal sealed class VikorMethod<T>(VikorMethodParameters<T> methodParameters) : IMcdaMethod<T, VikorMethodResult<T>>
    where T : struct, System.Numerics.IFloatingPointIeee754<T>
{
    public VikorMethodResult<T> Execute(McdaProblem<T> problem)
    {
        var fStar = problem.Data.GetColMax();
        var fMinus = problem.Data.GetColMin();

        var weights = problem.Criteria.Select(x => x.Weight).ToArray();
        var weightedMatrix = problem.Data.MapIndexed(
            (i, j, value) => weights[j] * (fStar[j] - value) / (fStar[j] - fMinus[j])
        );

        var s = weightedMatrix.RowSums();
        var r = weightedMatrix.Transpose().GetColMax();

        var sStar = s.Minimum();
        var sMinus = s.Maximum();
        var rStar = r.Minimum();
        var rMinus = r.Maximum();

        var sNormalized = (s - sStar) / (sMinus - sStar);
        var rNormalized = (r - rStar) / (rMinus - rStar);
        var qNormalized = methodParameters.V * sNormalized + (T.One - methodParameters.V) * rNormalized;

        var denom = T.CreateChecked(problem.Data.RowCount) - T.One;
        var dq = T.One / denom;

        var sortedQ = SortByQ(qNormalized);

        return new VikorMethodResult<T>()
        {
            DQ = dq,
            Alternatives = [.. Enumerable.Range(0, qNormalized.Count)
                .Select(i => new VikorMethodAlternativeResult<T>()
                {
                    Q = qNormalized[i],
                    R = r[i],
                    S = s[i]
                })],
            BestAlternativeIndex = IsAcceptable(sortedQ[0].AlternativeIndex, s, r) && IsStable(sortedQ, dq) ? sortedQ[0].AlternativeIndex : null,
            CompromiseSetIndices = GetCompromiseIndexes(qNormalized,qNormalized[sortedQ[0].AlternativeIndex],dq)
        };
    }

    private static QItem[] SortByQ(Vector<T> q)
    {
        var sortedQ = new QItem[q.Count];
        for (int i = 0; i < q.Count; i++)
        {
            sortedQ[i] = new QItem(i, q[i]);
        }
        Array.Sort(sortedQ, new QItemComparer());
        return sortedQ;
    }

    private static bool IsStable(QItem[] sortedQ, T dq)
    {
        return sortedQ[1].Q - sortedQ[0].Q >= dq;
    }

    private static bool IsAcceptable(
        int bestAlternativeIndex,
        Vector<T> s,
        Vector<T> r) => s.MinimumIndex() == bestAlternativeIndex || r.MinimumIndex() == bestAlternativeIndex;

    private static int[] GetCompromiseIndexes(
        Vector<T> qNormalized,
        T qStar,
        T dq)
    {
        var temp = new int[qNormalized.Count];
        int count = 0;

        for (int i = 0; i < qNormalized.Count; i++)
        {
            if (qNormalized[i] - qStar < dq)
            {
                temp[count++] = i + 1;
            }
        }
        return temp.AsSpan(0, count).ToArray();
    }

    private readonly struct QItem
    {
        public readonly int AlternativeIndex;
        public readonly T Q;

        public QItem(int alternativeIndex, T q)
        {
            AlternativeIndex = alternativeIndex;
            Q = q;
        }
    }

    private sealed class QItemComparer : IComparer<QItem>
    {
        public int Compare(QItem x, QItem y)
            => x.Q.CompareTo(y.Q);
    }
}
