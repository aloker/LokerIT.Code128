namespace LokerIT.Code128
{
    public interface ICode128Encoder
    {
        IEncodedCode128 Encode(string input);
    }
}