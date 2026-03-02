using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata;
using static System.Formats.Asn1.AsnWriter;
using static System.Reflection.Metadata.BlobBuilder;

namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
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





public enum TicketType
        {
            Standard = 0,
            VIP = 1,
            IMAX = 2
        }

        public struct SeatLocation
        {
            public char Row { get; set; }
            public int Number { get; set; }

            public SeatLocation(char row, int number)
            {
                Row = row;
                Number = number;
            }

            public override string ToString() => $"{Row}-{Number}";
        }

        // Ticket (Encapsulated) 
        public class Ticket
        {
          
            private static int ticketCounter = 0;

            private string _movieName = "Unknown";
            private TicketType _type;
            private SeatLocation _seat;
            private double _price = 1.0;

            public int TicketId { get; }

            public string MovieName
            {
                get => _movieName;
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        _movieName = value.Trim();
                    }
                    
                }
            }

            public TicketType Type
            {
                get => _type;
                set => _type = value; 
            }

            public SeatLocation Seat
            {
                get => _seat;
                set => _seat = value;
            }

            public double Price
            {
                get => _price;
                set
                {
                    if (value > 0) _price = value;
                   
                }
            }

           
            public double PriceAfterTax => Math.Round(_price * 1.14, 2);

            
            public static int GetTotalTicketsSold() => ticketCounter;

            public Ticket(string movieName, TicketType type, SeatLocation seat, double price)
            {
               
                ticketCounter++;
                TicketId = ticketCounter;


                MovieName = movieName;
                Type = type;
                Seat = seat;
                Price = price;
            }

            public override string ToString()
            {
                return $"Ticket #{TicketId}\n" +
                       $"{MovieName}\n" +
                       $"{Type}\n" +
                       $"Seat: {Seat}\n" +
                       $"Price: {Price} EGP\n" +
                       $"After Tax: {PriceAfterTax} EGP";
            }
        }

  
        public class Cinema
        {
            private readonly Ticket[] _tickets = new Ticket[20];

            public Ticket this[int index]
            {
                get
                {
                    if (index < 0 || index >= _tickets.Length) return null;
                    return _tickets[index];
                }
                set
                {
                    if (index < 0 || index >= _tickets.Length) return;
                    _tickets[index] = value;
                }
            }

            public bool AddTicket(Ticket t)
            {
                for (int i = 0; i < _tickets.Length; i++)
                {
                    if (_tickets[i] == null)
                    {
                        _tickets[i] = t;
                        return true;
                    }
                }
                return false;   
            }

            public Ticket GetMovieByName(string movieName)
            {
                if (string.IsNullOrWhiteSpace(movieName)) return null;
                var target = movieName.Trim();
                for (int i = 0; i < _tickets.Length; i++)
                {
                    var t = _tickets[i];
                    if (t != null && string.Equals(t.MovieName, target, StringComparison.OrdinalIgnoreCase))
                        return t;
                }
                return null;
            }
        }

            
        public static class BookingHelper
        {
            private static int _bookingCounter = 0;

            public static double CalcGroupDiscount(int numberOfTickets, double pricePerTicket)
            {
                if (numberOfTickets <= 0 || pricePerTicket < 0) return 0;

                double total = numberOfTickets * pricePerTicket;
                if (numberOfTickets >= 5)
                {
                    total *= 0.90;  
                }
                return Math.Round(total, 2);
            }

            public static string GenerateBookingReference()
            {
                _bookingCounter++;
                return $"BK-{_bookingCounter}";
            }
        }

        
        
            static void Main()
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                var cinema = new Cinema();

                Console.WriteLine("========== Ticket Booking ==========");

                for (int i = 1; i <= 3; i++)
                {
                    Console.WriteLine($"Enter data for Ticket {i}:");

                    string movieName = ReadNonEmpty("Movie Name");
                    TicketType type = ReadTicketType("Ticket Type (0=Standard, 1=VIP, 2=IMAX)");
                    char row = ReadSeatRow("Seat Row (A-Z)");
                    int number = ReadInt("Seat Number", min: 1);
                    double price = ReadDouble("Price", minExclusive: 0);

                    var seat = new SeatLocation(row, number);
                    var ticket = new Ticket(movieName, type, seat, price);
                    bool added = cinema.AddTicket(ticket);

                    if (!added)
                    {
                        Console.WriteLine("Cinema is full. Could not add the ticket.");
                    }
                }

                Console.WriteLine("========== All Tickets ==========");
                for (int i = 0; i < 3; i++)
                {
                    var t = cinema[i];
                    if (t != null)
                    {
                        Console.WriteLine(t.ToString());
                    }
                }

                Console.WriteLine("========== Search by Movie ==========");
                Console.Write("Enter movie name to search: ");
                string toSearch = Console.ReadLine() ?? string.Empty;
                var found = cinema.GetMovieByName(toSearch);
                if (found != null)
                {
                    Console.WriteLine("Found:");
                    Console.WriteLine(found.ToString().Replace("\nAfter Tax", "\nPrice")); 
                }
                else
                {
                    Console.WriteLine("Not found.");
                }

                Console.WriteLine("========== Statistics ==========");
                Console.WriteLine($"Total Tickets Sold: {Ticket.GetTotalTicketsSold()}");

                string ref1 = BookingHelper.GenerateBookingReference();
                string ref2 = BookingHelper.GenerateBookingReference();
                Console.WriteLine($"Booking Reference 1: {ref1}");
                Console.WriteLine($"Booking Reference 2: {ref2}");

                double group = BookingHelper.CalcGroupDiscount(5, 80);
                Console.WriteLine($"Group Discount (5 tickets x 80 EGP): {group} EGP {(group < 5 * 80 ? "(10% off applied)" : "")}");
            }

        
            private static string ReadNonEmpty(string prompt)
            {
                while (true)
                {
                    Console.Write($"{prompt}: ");
                    string? s = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(s)) return s.Trim();
                    Console.WriteLine("Value cannot be empty. Try again.");
                }
            }

            private static TicketType ReadTicketType(string prompt)
            {
                while (true)
                {
                    Console.Write($"{prompt}: ");
                    string? s = Console.ReadLine();
                    if (int.TryParse(s, out int val) && Enum.IsDefined(typeof(TicketType), val))
                    {
                        return (TicketType)val;
                    }
                    Console.WriteLine("Invalid ticket type. Enter 0, 1, or 2.");
                }
            }

            private static char ReadSeatRow(string prompt)
            {
                while (true)
                {
                    Console.Write($"{prompt}: ");
                    string? s = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        char c = char.ToUpperInvariant(s.Trim()[0]);
                        if (c >= 'A' && c <= 'Z') return c;
                    }
                    Console.WriteLine("Invalid row. Enter a letter A-Z.");
                }
            }

            private static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
            {
                while (true)
                {
                    Console.Write($"{prompt}: ");
                    string? s = Console.ReadLine();
                    if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out int n) && n >= min && n <= max)
                    {
                        return n;
                    }
                    Console.WriteLine($"Invalid number. Enter an integer in range [{min}, {max}].");
                }
            }

            private static double ReadDouble(string prompt, double minExclusive = double.NegativeInfinity, double maxInclusive = double.PositiveInfinity)
            {
                while (true)
                {
                    Console.Write($"{prompt}: ");
                    string? s = Console.ReadLine();
                    if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double d) && d > minExclusive && d <= maxInclusive)
                    {
                        return Math.Round(d, 2);
                    }
                    Console.WriteLine($"Invalid number. Must be > {minExclusive} and <= {maxInclusive}.");
                }

            }
        }
}
