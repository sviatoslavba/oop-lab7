using System;
using System.Collections.Generic;
using System.Linq;

public class DoublyLinkedListNode
{
    public char Value;
    public DoublyLinkedListNode Next;
    public DoublyLinkedListNode Previous;

    public DoublyLinkedListNode(char value)
    {
        Value = value;
        Next = null;
        Previous = null;
    }
}

public class DoublyLinkedList
{
    private DoublyLinkedListNode head;
    private DoublyLinkedListNode tail;
    public int Count { get; private set; }

    public DoublyLinkedList()
    {
        head = null;
        tail = null;
        Count = 0;

    }

    public void AddToEnd(char value)
    {
        var newNode = new DoublyLinkedListNode(value);
        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Previous = tail;
            tail = newNode;
        }
        Count++;
    }

    public int FindFirstOccurrence(char value)
    {
        for (int i = 0; i < Count; i++)
        {
            if (this[i] == value)
                return i + 1; 
        }
        return -1;
    }

    public int SumOfOddPositionElements()
    {
        int sum = 0;
        for (int i = 1; i < Count; i += 2)
        {
            sum += this[i];
        }
        return sum;
    }

    public void GetElementsGreaterThan(char threshold)
    {
        var newHead = (DoublyLinkedListNode)null;
        var newTail = (DoublyLinkedListNode)null;
        int newCount = 0;

        for (int i = 0; i < Count; i++)
        {
            if (this[i] > threshold)
            {
                var newNode = new DoublyLinkedListNode(this[i]);
                if (newHead == null)
                {
                    newHead = newNode;
                    newTail = newNode;
                }
                else
                {
                    newTail.Next = newNode;
                    newNode.Previous = newTail;
                    newTail = newNode;
                }
                newCount++;
            }
        }

        head = newHead;
        tail = newTail;
        Count = newCount;
    }

    public void RemoveElementsGreaterThanAverage()
    {
        var elements = new List<char>();
        for (int i = 0; i < Count; i++)
        {
            elements.Add(this[i]);
        }

        if (elements.Count == 0)
            return;

        double average = elements.Average(c => (int)c);

        for (int i = 0; i < Count;)
        {
            if (this[i] > average)
            {
                Remove(i);
            }
            else
            {
                i++;
            }
        }
    }

    private void Remove(int index)
    {
        if (index < 0 || index >= Count)
            throw new ArgumentOutOfRangeException("index", "Index must be non-negative and less than the size of the list.");

        var current = head;
        int currentIndex = 0;

        while (current != null)
        {
            if (currentIndex == index)
            {
                if (current.Previous != null)
                    current.Previous.Next = current.Next;
                else
                    head = current.Next;

                if (current.Next != null)
                    current.Next.Previous = current.Previous;
                else
                    tail = current.Previous;

                Count--;
                return;
            }
            current = current.Next;
            currentIndex++;
        }
    }

    public char this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException("index", "Index must be non-negative and less than the size of the list.");

            var current = head;
            int currentIndex = 0;

            while (current != null)
            {
                if (currentIndex == index)
                    return current.Value;
                current = current.Next;
                currentIndex++;
            }

            throw new ArgumentOutOfRangeException("index", "Index out of range.");
        }
        set
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException("index", "Index must be non-negative and less than the size of the list.");

            var current = head;
            int currentIndex = 0;

            while (current != null)
            {
                if (currentIndex == index)
                {
                    current.Value = value;
                    return;
                }
                current = current.Next;
                currentIndex++;
            }

            throw new ArgumentOutOfRangeException("index", "Index out of range.");
        }
    }

    public void PrintList()
    {
        var current = head;
        while (current != null)
        {
            Console.Write(current.Value + " ");
            current = current.Next;
        }
        Console.WriteLine();
    }
}

class lab7
{
    static void StartMenu(DoublyLinkedList list, ref bool exit) 
    {
        Console.WriteLine("\nMenu:");
        Console.WriteLine("1. Add element to the end of the list");
        Console.WriteLine("2. Find the first occurrence of an element");
        Console.WriteLine("3. Sum of elements at odd positions");
        Console.WriteLine("4. Get elements greater than a specified value");
        Console.WriteLine("5. Remove elements greater than the average value");
        Console.WriteLine("6. Print the list");
        Console.WriteLine("7. Exit");
        Console.Write("Enter your choice: ");

        int choice;
        if (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
            Console.ReadLine();
        }

        switch (choice)
        {
            case 1:
                Console.Write("Enter a character to add: ");
                char valueToAdd = Console.ReadKey().KeyChar;
                Console.WriteLine();
                list.AddToEnd(valueToAdd);
                break;

            case 2:
                Console.Write("Enter a character to find: ");
                char valueToFind = Console.ReadKey().KeyChar;
                Console.WriteLine();
                int position = list.FindFirstOccurrence(valueToFind);
                if (position != -1)
                    Console.WriteLine($"Element '{valueToFind}' found at position: {position}");
                else
                    Console.WriteLine($"Element '{valueToFind}' not found in the list.");
                Console.ReadLine();
                break;

            case 3:
                int sum = list.SumOfOddPositionElements();             
                Console.Write("Sum of elements at odd positions:");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" {sum}");
                Console.ResetColor();
                Console.ReadLine();
                break;

            case 4:
                Console.Write("Enter a character value: ");
                char threshold = Console.ReadKey().KeyChar;
                Console.WriteLine();
                list.GetElementsGreaterThan(threshold);
                Console.WriteLine("List updated with elements greater than the specified value.");
                Console.ReadLine();
                break;

            case 5:
                list.RemoveElementsGreaterThanAverage();
                Console.WriteLine("Removed elements greater than the average value.");
                Console.ReadLine();
                break;

            case 6:
                Console.WriteLine("List contents:");
                list.PrintList();
                Console.ReadLine();
                break;

            case 7:
                exit = true;
                break;

            default:
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
                break;
        }
    }
    static void Main(string[] args)
    {
        var list = new DoublyLinkedList();
        bool exit = false;

        while (!exit)
        {
            StartMenu(list, ref exit);
            Console.Clear();
        }
    }
}
