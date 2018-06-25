using Veruthian.Dotnet.Library.Collections;

namespace Veruthian.Dotnet.Library.Numeric
{
    public interface IBinary<B>
        where B : struct, IBinary<B>
    {
        IIndex<bool> Bits{ get; }

        B And(B value);

        B Nand(B value);

        B Or(B value);

        B Nor(B value);

        B Xor(B value);

        B Equivalence(B value);

        B Not();
    }
}