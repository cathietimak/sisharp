using Calculator;
using Multicast;
using Filter;
using Standarddelegates;
using Logger;
using Validator;

namespace sisharp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Calculator.Calculator.Run();
            Multicast.Multicast.Run();
            Filter.Filter.Run();
            Standarddelegates.StandardDelegates.Run();
            Logger.Logger.Run();
            Validator.Validator.Run();
        }
    }
}