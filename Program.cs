using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Store store = new Store();
        store.Run();
    }
}

public class Store
{
    private List<Customer> customers = new List<Customer>(); //gör en lista för de olika kunderna
    private Customer currentCustomer; //den aktiva kunden

    public void Run()
    {
        customers.Add(new Customer("Knatte", "123")); //lägger förinstälda kunder i listan
        customers.Add(new Customer("Fnatte", "321"));
        customers.Add(new Customer("Tjatte", "213"));
        Console.WriteLine("Välkommen till Köppis");

        while (true)
        {
            Console.WriteLine("");
            Console.WriteLine("Snälla välj genom att skriva ett enstakt number, sedan enter");
            Console.WriteLine("1. Registrera ny kund");
            Console.WriteLine("2. Logga in");
            Console.WriteLine("3. Avsluta");
            Console.WriteLine("");

            string choice = Console.ReadLine();

            switch (choice) 
            {
                case "1":
                    RegisterCustomer();
                    break;
                case "2":
                    Login();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    break;
            }
        }
    }

    private void RegisterCustomer()
    {
        Console.Write("Ange namn: ");
        string name = Console.ReadLine();
        Console.Write("Ange lösenord: ");
        string password = Console.ReadLine();

        if (customers.Exists(d => d.Name == name)) //kollar om namnet redan är registrerat genom en lambda expression
        {
            Console.WriteLine("Kunden finns redan registrerad.");
            return;
        }

        customers.Add(new Customer(name, password)); // lägger till registrerad kund i kund listan
        Console.WriteLine("Kund registrerad!");
    }

    private void Login()
    {
        Console.Write("Ange namn: ");
        string name = Console.ReadLine();
        Console.Write("Ange lösenord: ");
        string password = Console.ReadLine();

        currentCustomer = customers.Find(c => c.Name == name); //hittar kunden från listan och gör det till den aktiva kunden

        if (currentCustomer == null) //om det int
        {
            Console.WriteLine("Kunden finns inte. Vill du registrera en ny kund?");
            Console.WriteLine("1.Ja 2.Nej");

            if (Console.ReadLine() == "1")
            {
                RegisterCustomer();
            }
            return;
        }

        if (!currentCustomer.VerifyPassword(password))
        {
            Console.WriteLine("Lösenordet är fel. Försök igen.");
            return;
        }

        Console.WriteLine($"Välkommen {currentCustomer.Name}!");
        CustomerMenu();
    }

    private void CustomerMenu()
    {
        while (true)
        {
            Console.WriteLine("");
            Console.WriteLine("Välkommen! Vad vill du göra?:");
            Console.WriteLine("1. Handla");
            Console.WriteLine("2. Se kundvagn");
            Console.WriteLine("3. Gå till kassan");
            Console.WriteLine("4. Logga ut");
            Console.WriteLine("");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Shop();
                    break;
                case "2":
                    ViewCart();
                    break;
                case "3":
                    Checkout();
                    return;
                case "4":
                    currentCustomer = null;
                    return;
                default:
                    Console.WriteLine("Skriv 1,2,3 eller 4 följt av enter");
                    break;
            }
        }
    }

    private void Shop()
    {
        List<Product> products = new List<Product> //lista med produkterna kan lätt läggas till
        {
            new Product("Korv", 10),
            new Product("Baguette", 15),
            new Product("Äpple", 5)
        };

        Console.WriteLine("Tillgängliga produkter:");
        foreach (var product in products) //räknar automatiskt och skriver ut produkter
        {
            Console.WriteLine(product);
        }
        Console.WriteLine("");

        Console.Write("Ange produktnamn för att lägga till i kundvagnen: ");
        string productName = Console.ReadLine();
        Product selectedProduct = products.Find(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase)); //Kollar vilken produkt som kunden skrev -
        // -och hittar den i produkt listan. 
        if (selectedProduct != null) 
        {
            Console.Write("Ange antal: ");
            if (int.TryParse(Console.ReadLine(), out int quantity))
            {
                currentCustomer.AddToCart(selectedProduct, quantity);
                Console.WriteLine($"{quantity} {selectedProduct.Name} har lagts till i kundvagnen.");
            }
            else
            {
                Console.WriteLine("Ogiltigt antal.");
            }
        }
        else
        {
            Console.WriteLine("Finns inte sådan produkt.");
        }
    }

    private void ViewCart()
    {
        Console.WriteLine(currentCustomer);
    }

    private void Checkout()
    {

        Console.WriteLine($"Totala kostnaden för din kundvagn: {currentCustomer.GetTotalPrice()} kr");
        Console.WriteLine("Tack att du handlat hos oss!");
        Console.WriteLine("");

        currentCustomer.ClearCart();
    }
}

public class Customer
{
    public string Name { get; }
    private string password;
    private List<CartItem> cart = new List<CartItem>();

    public Customer(string name, string password)
    {
        Name = name;
        this.password = password;
    }

    public bool VerifyPassword(string password)
    {
        return this.password == password;
    }

    public void AddToCart(Product product, int quantity) //lägger till det i vagnen
    {
        cart.Add(new CartItem(product, quantity));
    }

    public double GetTotalPrice()
    {
        double total = 0;
        foreach (var item in cart) //kör för varje unik produkt i vagnen
        {
            total += item.Product.Price * item.Quantity; //produktens pris gången mängd
        }
        return total;
    }

    public void ClearCart() 
    {
        cart.Clear();
    }

    public override string ToString()
    {
        string cartDetails = "Kundvagn:\n";
        foreach (var item in cart)
        {
            cartDetails += $"{item.Quantity} x {item.Product.Name} - {item.Product.Price} kr\n"; //lägger till alla komponenter till cartdetails
        }
        cartDetails += $"Totalt: {GetTotalPrice()} kr"; 
        return $"Namn: {Name}\n{cartDetails}";
    }
}

public class CartItem //produkt som finns i vagnen
{
    public Product Product { get; }
    public int Quantity { get; }

    public CartItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }
}

public class Product
{
    public string Name { get; }
    public double Price { get; }

    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name} - {Price} kr";
    }
}
