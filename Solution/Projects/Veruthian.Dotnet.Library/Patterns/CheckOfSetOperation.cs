using Veruthian.Dotnet.Library.Numeric;
using Veruthian.Dotnet.Library.Numeric.Ranges;
using Veruthian.Dotnet.Library.Readers;

namespace Veruthian.Dotnet.Library.Patterns
{
    public class CheckOfSetOperation<TState, TReader, T> : CheckOperation<TState, TReader, T>
        where TState : HasType<TReader>
        where TReader : ILookaheadReader<T>
        where T : struct, ISequential<T>, IBounded<T>
    {
        RangeSet<T> set;

        protected CheckOfSetOperation(RangeSet<T> set, int lookahead) : base(lookahead) => this.set = set;

        public override string Description => $"MatchOfSet({set.ToString()})";

        protected override bool Match(T item) => set.Contains(item);
    }
}