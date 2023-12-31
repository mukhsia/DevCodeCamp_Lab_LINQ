﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LINQLab.Models;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using Org.BouncyCastle.Asn1;
using Microsoft.Extensions.Options;

namespace LINQLab
{
    class Problems
    {
        private EcommerceContext _context;

        public Problems()
        {
            _context = new EcommerceContext();
        }
        public void RunLINQQueries()
        {
            //// <><><><><><><><> R Actions (Read) <><><><><><><><><>
            //RDemoOne();
            //RProblemOne();
            //RDemoTwo();
            //RProblemTwo();
            //RProblemThree();
            //RProblemFour();
            //RProblemFive();

            //// <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>
            //RDemoThree();
            //RProblemSix();
            //RProblemSeven();
            //RProblemEight();

            //// <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

            //// <><> C Actions (Create) <><>
            //CDemoOne();
            //CProblemOne();
            //CDemoTwo();
            //CProblemTwo();

            //// <><> U Actions (Update) <><>
            //UDemoOne();
            //UProblemOne();
            //UProblemTwo();

            //// <><> D Actions (Delete) <><>
            //DDemoOne();
            //DProblemOne();
            //DProblemTwo();

            //// <><> Bonus Problems <><>
            //BonusOne();
            BonusTwo();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void RDemoOne()
        {
            // This LINQ query will return all the users from the User table.
            var users = _context.Users.ToList();

            Console.WriteLine("RDemoOne: Emails of All users");
            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void RProblemOne()
        {
            // Print the COUNT of all the users from the User table.
            var count = _context.Users.ToList().Count();

            Console.WriteLine("\nRProblemOne: Count of all users");
            Console.WriteLine($"User Count: {count}");
        }

        /*
       Expected Result:
       User Count: 5
        */

        public void RDemoTwo()
        {
            // This LINQ query will get each product whose price is greater than $150.
            var productsOver150 = _context.Products.Where(p => p.Price > 150);
            Console.WriteLine("RDemoTwo: Products greater than $150");
            foreach (Product product in productsOver150)
            {
                Console.WriteLine($"{product.Name} - ${product.Price}");
            }
        }

        public void RProblemTwo()
        {
            // Write a LINQ query that gets each product whose price is less than or equal to $100.
            // Print the name and price of all products
            var productsLessOrEqual100 = _context.Products.Where(p => p.Price <= 100);
            Console.WriteLine("\nRProblemTwo: Products less than or equal to 100");
            foreach (Product product in productsLessOrEqual100)
            {
                Console.WriteLine($"\n{product.Name} \n ${product.Price}");
            }
        }

        /*
            Expected Result:

            Name: Freedom from the Known - Jiddu Krishnamurti
            Price: $14.99

            Name: Ball Mason Jar-32 oz.
            Price: $8.85

            Name: Catan The Board Game
            Price: $43.67
         */

        public void RProblemThree()
        {
            // Write a LINQ query that gets each product whose name that CONTAINS an "s".
            var productsNameContainsS = _context.Products.Where(p => p.Name.Contains("s"));
            Console.WriteLine("\nRProblemThree: Products whose name contains an 's'");
            foreach (Product product in productsNameContainsS)
            {
                Console.WriteLine($"\nName: {product.Name}");
            }
        }
        /*
            Expected Result:

            Name: Freedom from the Known - Jiddu Krishnamurti

            Name: Ball Mason Jar-32 oz.

            Name: Stellar Basic Flute Key of G - Native American Style Flute

            Name: Apple Watch Series 3

            Name: Nintendo Switch
         */

        public void RProblemFour()
        {
            // Write a LINQ query that gets all the users who registered BEFORE 2016.
            // Then print each user's email and registration date to the console.
            var usersBefore2016 = _context.Users.Where(p => p.RegistrationDate < new DateTime(2016, 1, 1));
            Console.WriteLine("\nRProblemFour: User's email and registration date of users who registered before 2016.\n");
            foreach(User users in usersBefore2016)
            {
                Console.WriteLine($"Email: {users.Email}\nRegistration Date: {users.RegistrationDate}");
            }
        }
        /*
            Expected Result:

            Email: janett@gmail.com
            Registration Date: 10/15/2015 12:00:00 AM
            Email: gary@gmail.com
            Registration Date: 10/15/2012 12:00:00 AM
         */

        public void RProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018.
            // Then print each user's email and registration date to the console.
            var usersAfter2016Before2018 = _context.Users.Where(p => p.RegistrationDate > new DateTime(2016, 1, 1) && p.RegistrationDate < new DateTime(2018,1,1));
            Console.WriteLine("\nRProblemFive: User's email and registration date of users who registered after 2016 and before 2018.\n");
            foreach (User users in usersAfter2016Before2018)
            {
                Console.WriteLine($"Email: {users.Email}\nRegistration Date: {users.RegistrationDate}");
            }
        }
        /*
            Expected Result:

            Email: bibi@gmail.com
            Registration Date: 4/6/2017 12:00:00 AM
         */

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void RDemoThree()
        {
            // This LINQ query will retreive all of the users who are assigned to the role of Customer.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            Console.WriteLine("RDemoThree: Customer Users");
            foreach (UserRole userrole in customerUsers)
            {
                Console.WriteLine($"Email: {userrole.User.Email} Role: {userrole.Role.RoleName}");
            }
        }
        public void RProblemSix()
        {
            // Write a LINQ query that retrieves all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            var aftonShoppingCart = _context.ShoppingCartItems.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Email == "afton@gmail.com");
            Console.WriteLine("\nRProblemSix: Afton Shopping Cart.");
            foreach (ShoppingCartItem products in aftonShoppingCart)
            {
                Console.WriteLine($"Name: {products.Product.Name}\nPrice: {products.Product.Price}\nQuantity: {products.Quantity}\n");
            }
        }
        /*
            Expected Result:
            Name: Freedom from the Known - Jiddu Krishnamurti
            Price: $14.99
            Quantity: 1

            Name: Ball Mason Jar-32 oz.
            Price: $8.85
            Quantity: 10

            Name: Catan The Board Game
            Price: $43.67
            Quantity: 1

            Name: Nintendo Switch
            Price: $299.00
            Quantity: 1
        */

        public void RProblemSeven()
        {
            // Write a LINQ query that retrieves all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Print the total of the shopping cart to the console.
            // Remember to break the problem down and take it one step at a time!
            var odaShoppingTotal = _context.ShoppingCartItems
                .Include(sc => sc.Product)
                .Include(sc => sc.User)
                .Where(sc => sc.User.Email == "oda@gmail.com")
                .Select(sc => sc.Product.Price)
                .Sum();
            Console.WriteLine("\nRProblemSeven: Oda Shopping Total.");
            Console.WriteLine($"\nTotal: ${odaShoppingTotal}");
        }
        /*
         Total: $715.34
         */

        public void RProblemEight()
        {
            // Write a query that retrieves all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the product's name, price, and quantity to the console along with the email of the user that has it in their cart.
            var employeeShoppingCarts = _context.UserRoles
                .Where(ur => ur.Role.RoleName == "Employee")
                .Include(ur => ur.User)
                .Include(u => u.User.ShoppingCartItems)
                .ThenInclude(sc => sc.Product)
                .ToList();

            // Note: Took a while to figure out ThenInclude
            Console.WriteLine("\nRProblemEight: Employee Shopping Carts.");

            // Note: Breakpoint helped in figuring out the structure of the list for the nested foreach loop
            foreach (UserRole userCarts in employeeShoppingCarts)
            {
                Console.WriteLine($"\nUser's email: {userCarts.User.Email}\n-----------");
                foreach(ShoppingCartItem shoppingCart in userCarts.User.ShoppingCartItems)
                {
                    Console.WriteLine($"\nProduct name: {shoppingCart.Product.Name}\nPrice: {shoppingCart.Product.Price}\nQuantity: {shoppingCart.Quantity}");
                }
            }
        }
        /*
            Expected Result

            User's email: bibi@gmail.com
            -----------
            Product name: Apple Watch Series 3
            Price: 169.00
            Quantity:5



            User's email: janett@gmail.com
            -----------
            Product name: Freedom from the Known - Jiddu Krishnamurti
            Price: 14.99
            Quantity:1

            Product name: Catan The Board Game
            Price: 43.67
            Quantity:1
         */

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        //IMPORTANT: The following methods will MODIFY your database. Even if you stop and restart the application, any changes made to the database will persist!
        //Calling a Create method more than once will result in duplicate data added to the table.
        //Calling an Update or Delete method more than once might cause an error. For instance, if you call a method that deletes the Nintendo Switch from the database, then try to call the method again, there will no longer be a Nintendo Switch to delete!
        //You may want to use Breakpoints or WriteLines to verify your LINQ Queries are finding the correct items before you add the .SaveChanges() to the method!

        // <><> C Actions (Create) <><>

        private void CDemoOne()
        {
            // This will create a new User object and add it to the Users table.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

        }

        private void CProblemOne()
        {
            // Create a new Product object and add that product to the Products table. Choose any name and product info you like.
            Product newProduct = new Product()
            {
                Name = "Wooting Two HE",
                Description = "Analog Mechanical Keyboard utilizing Hall Effect technology.",
                Price = 200.0M
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        public void CDemoTwo()
        {
            // This will add the role of "Customer" to the user we created in CDemoOne by adding a new row to the Userroles junction table.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserrole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserrole);
            _context.SaveChanges();
            // If you encounter problems running this method, it likely means you ran CDemoOne multiple times and have created duplicate customers with the email "david@gmail.com"
        }

        public void CProblemTwo()
        {
            // Create a new ShoppingCartItem to represent the new product you created in CProblemOne being added to the shopping cart of the user created in CDemoOne.
            // This will add a new row to ShoppingCart junction table.
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            var productId = _context.Products.Where(p => p.Name == "Wooting Two HE").Select(p => p.Id).SingleOrDefault();
            ShoppingCartItem newShoppingCartItem = new ShoppingCartItem()
            {
                ProductId = productId,
                UserId = userId,
                Quantity = 0
            };
            _context.ShoppingCartItems.Add(newShoppingCartItem);
            _context.SaveChanges();
        }


        // <><> U Actions (Update) <><>

        private void UDemoOne()
        {
            // This will update the email of the user from CDemoOne to "dan@gmail.com"
            // Remember that after this update occurs, there should be no more "david@gmail.com" on the database!
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "dan@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void UProblemOne()
        {
            // Update the price of the product you created in CProblemOne to something different using LINQ.
            var product = _context.Products.Where(p => p.Name == "Wooting Two HE").SingleOrDefault();
            product.Price = 200.69M;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void UProblemTwo()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new Userrole object and add it to the Userroles table
            // See the DDemoOne below as an example of removing a role relationship

            // Tentative direct update solution could work, but Delete and Create for junction table entries help reduce 
            var userrole = _context.UserRoles.Where(ur => ur.User.Email == "dan@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userrole);
            _context.SaveChanges();

            var roleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "dan@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserrole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserrole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void DDemoOne()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userrole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userrole);

            _context.SaveChanges();

        }

        private void DProblemOne()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Use a Loop
            var userShoppingCart = _context.ShoppingCartItems.Where(u => u.User.Email == "oda@gmail.com").ToList();
            foreach(var productRel in userShoppingCart)
            {
                _context.ShoppingCartItems.Remove(productRel);
            }
            _context.SaveChanges();
        }

        private void DProblemTwo()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var user = _context.Users.Where(u => u.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(user);

            _context.SaveChanges();
        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".

            Console.WriteLine("Enter Email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();

            var user = _context.Users.Where(u => u.Email == email && u.Password == password).SingleOrDefault();
            if(user != null)
            {
                Console.WriteLine("Signed In!");
            } 
            else
            {
                Console.WriteLine("Invalid Email or Password.");
            }
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the toals to the console.
            var usersShoppingCart = _context.ShoppingCartItems
                .Include(sc => sc.User)
                .Include(sc => sc.Product)
                .GroupBy(sc => sc.User.Email)
                .Select( grp => new {
                    grp.Key,
                    cartTotal = grp.Sum(g => g.Product.Price)
                })
                .ToList();

            Console.WriteLine("Shopping Carts: ");

            decimal totalOfTotals = 0.0M;
            foreach(var cart in usersShoppingCart)
            {
                Console.WriteLine($"\nName: {cart.Key}\nTotal: ${cart.cartTotal}");
                totalOfTotals += cart.cartTotal;
            }

            Console.WriteLine($"\n\nTotal of all carts: {totalOfTotals}");
        }

        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in, give them a menu where they can perform the following actions within the console:
            // -View the products in their shopping cart
            // -View all products in the Products table
            // -Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // -Remove a product from their shopping cart
            // 3. If the user does not successfully sign in
            // -Display "Invalid Email or Password"
            // -Re-prompt the user for credentials
            bool isAuthenticated = false;
            User user = new User();
            int choice;

            while (!isAuthenticated)
            {
                Console.WriteLine("Enter Email: ");
                string email = Console.ReadLine();
                Console.WriteLine("Enter Password: ");
                string password = Console.ReadLine();

                user = _context.Users.Where(u => u.Email == email && u.Password == password).SingleOrDefault();
                if (user != null)
                {
                    isAuthenticated = true;
                    Console.WriteLine("\nSigned In!\n");
                }
                else
                {
                    Console.WriteLine("\nInvalid Email or Password.\n");
                }
            }


            do
            {
                Console.WriteLine("1. View Shopping Cart.\n" +
                    "2. View available products.\n" +
                    "3. Add a product to the Shopping Cart.\n" +
                    "4. Remove a Product from the shopping Cart." +
                    "5. Exit.");
                Console.Write("Select an option (1-4):");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1: // View products in the shopping cart
                        ViewShoppingCart(user);
                        break;

                    case 2: // View all products
                        ViewAllProducts();
                        break;
                        
                    case 3: // Add a product to shopping cart (or increment quantity)
                        AddProductToShoppingCart(user);
                        break;
                        
                    case 4: // Remove a product from shopping cart
                        RemoveProductFromShoppingCart(user);
                        break;
                        
                    case 5:
                        Console.WriteLine("Exiting program.");
                        break;
                        
                    default:
                        Console.WriteLine("Please enter a valid choice between 1 to 5.");
                        break;


                }
            } while (choice != 5);
        }

        // Helper functions for Bonus 3
        private void ViewShoppingCart(User user)
        {
            var aftonShoppingCart = _context.ShoppingCartItems.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Email == user.Email);
            Console.WriteLine($"\n{user.Email}'s Shopping Cart:\n");
            foreach (ShoppingCartItem products in aftonShoppingCart)
            {
                Console.WriteLine($"Name: {products.Product.Name}\nPrice: {products.Product.Price}\nQuantity: {products.Quantity}\n");
            }
        }

        private void ViewAllProducts()
        {
            var allProducts = _context.Products.ToList();
            Console.WriteLine("\nAvailable Products:\n");
            foreach (Product product in allProducts)
            {
                Console.WriteLine($"{product.Name} - ${product.Price}");
            }
        }

        private void AddProductToShoppingCart(User user)
        {
            var productNames = _context.Products.Select(p => p.Name).ToList();
            Console.WriteLine("\nProducts:\n");
            int productOption = 0;
            foreach (string productName in productNames)
            {
                Console.WriteLine($"{productOption + 1}. {productName}");
            }
            Console.Write("Select a product: ");
            productOption = Convert.ToInt32(Console.ReadLine());
            if (productOption < 1 && productOption > productNames.Count())
            {
                Console.WriteLine("Invalid choice entered.\n");
            }
            else
            {
                // Check for if product in shopping cart.
                var productInShoppingCart = _context.ShoppingCartItems
                    .Where(sc => sc.User.Email == user.Email)
                    .Include(sc => sc.Product)
                    .Where(sc => sc.Product.Name == productNames[productOption-1])
                    .SingleOrDefault();

                // It exists, increment the quantity
                if(productInShoppingCart != null)
                {
                    ++productInShoppingCart.Quantity;
                    _context.Update(productInShoppingCart);
                    _context.SaveChanges();
                    Console.WriteLine($"Product quantity incremented. {productInShoppingCart.Product.Name} in shopping cart: {productInShoppingCart.Quantity}");
                }
                else // Product not in shopping cart, add it.
                {
                    ShoppingCartItem productItem = new ShoppingCartItem()
                    {
                        ProductId = productOption - 1,
                        UserId = user.Id,
                        Quantity = 1
                    };
                    _context.Add(productItem);
                    _context.SaveChanges();
                    Console.WriteLine($"Added {productNames[productOption-1]} product to shopping cart.");
                }
            }
        }

        private void RemoveProductFromShoppingCart(User user)
        {
            // Print shopping cart.

            // if quantity is 1, remove it from the shopping cart.

            // If quantity is more than 1, decrement it from the .


        }

    }
}
