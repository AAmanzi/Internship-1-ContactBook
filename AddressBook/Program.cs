using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var addressBook = new Dictionary<int, Tuple<string, string, string>>(); //Key = number; Value = (Item1 = name, Item2 = lastName, Item3 = address)
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
                        var newNumber = int.Parse(Console.ReadLine());
                        Console.WriteLine("Confirm your number:");
                        var confirmNumberAdd = int.Parse(Console.ReadLine());

                        if (confirmNumberAdd == newNumber)
                            addressBook.Add(newNumber, (newName, newLastName, newAddress).ToTuple());
                        else
                            Console.WriteLine("Number hasn't been confirmed!");
                        break;

                    //edit person properties
                    case 2:
                        var numberForChange = 0;

                        while(true)
                        {
                            Console.WriteLine("Input the number of the person you want to update:");
                            numberForChange = int.Parse(Console.ReadLine());
                            if (addressBook.ContainsKey(numberForChange))
                                break;
                            Console.WriteLine("We cannot find the number you have entered in the address book!");
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
                                if (int.Parse(Console.ReadLine()) != numberForChange)
                                {
                                    Console.WriteLine("Number hasn't been confirmed!");
                                    break;
                                }
                                var temporaryProperties = (addressBook[numberForChange].Item1, addressBook[numberForChange].Item2, addressBook[numberForChange].Item3).ToTuple();
                                addressBook.Remove(numberForChange);
                                Console.WriteLine("Enter new number:");
                                addressBook.Add(int.Parse(Console.ReadLine()), temporaryProperties);
                                break;
                            default:
                                Console.WriteLine("\nTHAT CHOICE IS NOT AVAILABLE\n");
                                break;
                        }
                        break;

                    //Delete a person from the address book
                    case 3:
                        var numberForDelete = 0;
                        while (true)
                        {
                            Console.WriteLine("Input the number of the person you want to delete:");
                            numberForDelete = int.Parse(Console.ReadLine());
                            if (addressBook.ContainsKey(numberForDelete))
                            {
                                break;
                            }
                            Console.WriteLine("We cannot find the number you have entered in the address book!");
                        }
                        Console.WriteLine("Are you sure you want to delete this person from the address book?");
                        Console.WriteLine("1) Yes");
                        Console.WriteLine("2) No");

                        if (int.Parse(Console.ReadLine()) == 1)
                        {
                            Console.WriteLine("{0} has been deleted!\n", addressBook[numberForDelete].Item1);
                            addressBook.Remove(numberForDelete);
                        }
                        break;

                    //Search by number
                    case 4:
                        Console.WriteLine("Input the number of the person you want to search:");
                        var numberForSearch = int.Parse(Console.ReadLine());
                        Console.WriteLine("Name: {0}\nLast name: {1}\nAddress: {2}\n", SearchByNumber(addressBook, numberForSearch).Item1, SearchByNumber(addressBook, numberForSearch).Item2, SearchByNumber(addressBook, numberForSearch).Item3);
                        break;

                    //search by name
                    case 5:
                        Console.WriteLine("Input the NAME of the person you want to search:");
                        var nameForSearch = Console.ReadLine();
                        Console.WriteLine("Input the LAST NAME of the person you want to search:");
                        var lastNameForSearch = Console.ReadLine();
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

        static void PrintAddressBook(Dictionary<int, Tuple<string, string, string>> addressBook)
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
        static Tuple<string, string, string> SearchByNumber(Dictionary<int, Tuple<string, string, string>> addressBook, int number)
        {
            if (addressBook.ContainsKey(number))
                return addressBook[number];
            Console.WriteLine("\nThe number you have entered is not located in the address book!\n");
            return null;
        }
        static Tuple<int, string> SearchByName(Dictionary<int, Tuple<string, string, string>> addressBook, string name, string lastName)
        {
            foreach (var person in addressBook)
            {
                if (person.Value.Item1 == name && person.Value.Item2 == lastName)
                    return (person.Key, person.Value.Item3).ToTuple();
            }
            Console.WriteLine("\nThe name you have entered is not located in the address book!\n");
            return null;
        }

        static void PrintSorted(Dictionary<int, Tuple<string, string, string>> addressBook)
        {
            var listNames = new List<Tuple<string, string, int, string>>();

            foreach (var person in addressBook)
            {
                listNames.Add((person.Value.Item2, person.Value.Item1, person.Key, person.Value.Item3).ToTuple());
            }

            listNames.Sort();

            foreach (var key in listNames)
            {
                Console.WriteLine("Name: {0}", key.Item2);
                Console.WriteLine("Last name: {0}", key.Item1);
                Console.WriteLine("Address: {0}", key.Item4);
                Console.WriteLine("Number: {0}", key.Item3);
            }
        }
    }
}
