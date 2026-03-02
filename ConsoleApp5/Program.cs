namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Qestion01

            // Q1: Encapsulation in BankAccount
            // a) Problems:
            //    - 'Owner' and 'Balance' are public fields → anyone can set them to invalid values.
            //    - Withdraw() has no validation (negative amount, overdraft), so invariants can be broken.
            // b) Fix idea (no full code):
            //    - Make fields private; expose properties with validation (e.g., Balance read-only or guarded setter).
            //    - Validate in Withdraw(): amount > 0 and amount <= Balance; return bool or throw if invalid.
            //    - Provide a constructor to enforce valid initial state (owner not empty, balance >= 0).
            // c) Why public fields are bad:
            //    - Breaks encapsulation (no single control point).
            //    - Prevents adding future logic (validation, logging, events) without breaking callers.
            //    - Increases coupling and risk of inconsistent/invalid object state.

            #endregion
        }
    }
}
