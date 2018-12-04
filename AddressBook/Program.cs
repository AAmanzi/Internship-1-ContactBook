using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var addressBook = new Dictionary<string, Tuple<string, string, string>>(); //Key = number; Value = (Item1 = name, Item2 = lastName, Item3 = address)
            var choice = 0;
            do
            {
                Console.WriteLine("1) Add a new person to the address book");
                Console.WriteLine("2) Change name, address, or number");
                Console.WriteLine("3) Delete a person from the address book");
                Console.WriteLine("4) Search by number");
                Console.WriteLine("5) Search by name");
                Console.WriteLine("6) Exit application");

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    //add new person
                    case 1:
                        //Add input checks
                        Console.WriteLine("Name:");
                        var newName = (Console.ReadLine());
                        Console.WriteLine("Last name:");
                        var newLastName = (Console.ReadLine());
                        Console.WriteLine("Address:");
                        var newAddress = (Console.ReadLine());
                        Console.WriteLine("Number:");
                        var newNumber = RemoveWhitespace(Console.ReadLine());
                        Console.WriteLine("Confirm your number:");
                        var confirmNumberAdd = RemoveWhitespace(Console.ReadLine());
                        
                        if (confirmNumberAdd.Equals(newNumber))
                            addressBook.Add(newNumber, (newName, newLastName, newAddress).ToTuple());
                        else
                            Console.WriteLine("Number hasn't been confirmed!");
                        break;

                    //edit person properties
                    case 2:
                        var numberForChange = "";

                        Console.WriteLine("Input the number of the person you want to update:");
                        numberForChange = RemoveWhitespace(Console.ReadLine());
                        if (!addressBook.ContainsKey(numberForChange))
                        {
                            Console.WriteLine("\nWe cannot find the number you have entered in the address book!\n");
                            break;
                        }   

                        Console.WriteLine("\t1) Change name");
                        Console.WriteLine("\t2) Change address");
                        Console.WriteLine("\t3) Change number");

                        var choiceChange = int.Parse(Console.ReadLine());

                        switch (choiceChange)
                        {
                            case 1: //change name (addressBook.Key = numberForChange)
                                Console.WriteLine("New name:");
                                addressBook[numberForChange] = (Console.ReadLine(), addressBook[numberForChange].Item2, addressBook[numberForChange].Item3).ToTuple();
                                Console.WriteLine("New last name:");
                                addressBook[numberForChange] = (addressBook[numberForChange].Item1, Console.ReadLine(), addressBook[numberForChange].Item3).ToTuple();
                                break;
                            case 2: //change address
                                Console.WriteLine("New address:");
                                addressBook[numberForChange] = (addressBook[numberForChange].Item1, addressBook[numberForChange].Item2, Console.ReadLine()).ToTuple();
                                break;
                            case 3: //change number
                                Console.WriteLine("Please confirm the number you want to change:");
                                var confirmNumberChange = RemoveWhitespace(Console.ReadLine());
                                if (!confirmNumberChange.Equals(numberForChange))
                                {
                                    Console.WriteLine("Number hasn't been confirmed!");
                                    break;
                                }
                                var temporaryProperties = (addressBook[numberForChange].Item1, addressBook[numberForChange].Item2, addressBook[numberForChange].Item3).ToTuple();
                                addressBook.Remove(numberForChange);
                                Console.WriteLine("Enter new number:");
                                addressBook.Add(Console.ReadLine(), temporaryProperties);
                                break;
                            default:
                                Console.WriteLine("\nTHAT CHOICE IS NOT AVAILABLE\n");
                                break;
                        }
                        break;

                    //Delete a person from the address book
                    case 3:
                        var numberForDelete = "";
                        var confirmation = 0;

                        Console.WriteLine("Input the number of the person you want to update:");
                        numberForChange = RemoveWhitespace(Console.ReadLine());
                        if (!addressBook.ContainsKey(numberForChange))
                        {
                            Console.WriteLine("\nWe cannot find the number you have entered in the address book!\n");
                            break;
                        }
                        do
                        {
                            Console.WriteLine("Are you sure you want to delete this person from the address book?");
                            Console.WriteLine("1) Yes");
                            Console.WriteLine("2) No");

                            confirmation = int.Parse(Console.ReadLine());
                            if (confirmation == 1)
                            {
                                Console.WriteLine("{0} has been deleted!\n", addressBook[numberForDelete].Item1);
                                addressBook.Remove(numberForDelete);
                            }
                            else if (confirmation != 2)
                                Console.WriteLine("\nTHAT CHOICE IS NOT AVAILABLE\n");
                        }
                        while (confirmation == 1 || confirmation == 2);

                        break;

                    //Search by number
                    case 4:
                        Console.WriteLine("Input the number of the person you want to search:");
                        var numberForSearch = RemoveWhitespace(Console.ReadLine());
                        if (SearchByNumber(addressBook, numberForSearch) == null)
                            break;
                        Console.WriteLine("Name: {0}\nLast name: {1}\nAddress: {2}\n", SearchByNumber(addressBook, numberForSearch).Item1, SearchByNumber(addressBook, numberForSearch).Item2, SearchByNumber(addressBook, numberForSearch).Item3);
                        break;

                    //search by name
                    case 5:
                        Console.WriteLine("Input the NAME of the person you want to search:");
                        var nameForSearch = Console.ReadLine();
                        Console.WriteLine("Input the LAST NAME of the person you want to search:");
                        var lastNameForSearch = Console.ReadLine();
                        if (SearchByName(addressBook, nameForSearch, lastNameForSearch) == null)
                            break;
                        Console.WriteLine("Number: {0}\nAddress: {2}\n", SearchByName(addressBook, nameForSearch, lastNameForSearch).Item1, SearchByName(addressBook, nameForSearch, lastNameForSearch).Item2);
                        break;
                    
                    //exit
                    case 6:
                        break;
                    
                    //print address book
                    case 0:
                        PrintAddressBook(addressBook);
                        break;
                    default:
                        Console.WriteLine("\nTHAT CHOICE IS NOT AVAILABLE\n");
                        break;
                }
            }
            while (choice != 6);

            //print in alphabetic order
            PrintSorted(addressBook);
        }

        static void PrintAddressBook(Dictionary<string, Tuple<string, string, string>> addressBook)
        {
            var i = 1;
            foreach (var person in addressBook)
            {
                Console.WriteLine("Person number " + i);
                Console.WriteLine("Name: {0}", person.Value.Item1);
                Console.WriteLine("Last name: {0}", person.Value.Item2);
                Console.WriteLine("Address: {0}", person.Value.Item3);
                Console.WriteLine("Number: {0}", person.Key + "\n");
                i++;
            }
        }
        static Tuple<string, string, string> SearchByNumber(Dictionary<string, Tuple<string, string, string>> addressBook, string number)
        {
            if (addressBook.ContainsKey(number))
                return addressBook[number];
            Console.WriteLine("\nThe number you have entered is not located in the address book!\n");
            return null;
        }
        static Tuple<string, string> SearchByName(Dictionary<string, Tuple<string, string, string>> addressBook, string name, string lastName)
        {
            foreach (var person in addressBook)
            {
                if (person.Value.Item1 == name && person.Value.Item2 == lastName)
                    return (person.Key, person.Value.Item3).ToTuple();
            }
            Console.WriteLine("\nThe name you have entered is not located in the address book!\n");
            return null;
        }

        static void PrintSorted(Dictionary<string, Tuple<string, string, string>> addressBook)
        {
            var listNames = new List<Tuple<string, string, string, string>>();

            foreach (var person in addressBook)
            {
                listNames.Add((person.Value.Item2, person.Value.Item1, person.Key, person.Value.Item3).ToTuple());
            }

            listNames.Sort();

            foreach (var key in listNames)
            {
                Console.WriteLine("\nName: {0}", key.Item2);
                Console.WriteLine("Last name: {0}", key.Item1);
                Console.WriteLine("Address: {0}", key.Item4);
                Console.WriteLine("Number: {0}\n", key.Item3);
            }
        }

        static string RemoveWhitespace(string toRemove)
        {
            var newString = "";
            foreach (var character in toRemove)
            {
                if (character.Equals(' '));
                else
                    newString += character;
            }
            return newString;
        }
    }
}
