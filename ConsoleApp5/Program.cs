using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata;
using static System.Formats.Asn1.AsnWriter;
using static System.Reflection.Metadata.BlobBuilder;

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


            #region Question02
            // Q02: Field vs Property
            //---------------------- -
            //-Difference: A field is just a variable to store data. A property is like a wrapper around a field that uses 'get' and 'set' blocks to manage how the data is handled.
            //-Can it contain logic?: Yes, properties can have logic inside them
            //- Example of read - only calculated property
            //  public double TotalPrice => Quantity * Price;

            #endregion


            #region Question03

            // Q3: Indexers
            // a) 'this[int index]' is an indexer. It lets an object be used like an array
            //    (e.g., register[0]) to get/set internal elements.
            // b) 'register[10] = "Ali";' will throw IndexOutOfRangeException (array size is 5).
            //    Make it safer by checking bounds in get/set; either return null / ignore set,
            //    or throw a friendly exception message.
            // c) Multiple indexers? Yes—by changing the parameter list (overloads).
            //    Example: one by int (position) and another by string (key/name)

            #endregion



            #region Question04

            // Q4: static vs instance
            // a) 'static' TotalOrders is shared by the class (one value for all orders).
            //    'Item' is an instance field—each object has its own copy.
            // b) A static method cannot access 'Item' directly because it has no instance ('this').
            //    It must receive an Order instance as a parameter to read its Item.

            #endregion
        }
    }
}
