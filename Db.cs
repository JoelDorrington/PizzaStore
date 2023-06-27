using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaStore.DB;

public record Pizza
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class PizzaDB
{
    private static List<Pizza> _pizzas = new List<Pizza>() {
        new Pizza{ Id=1, Name="Cheese" },
        new Pizza{ Id=2, Name="Pepperoni" },
        new Pizza{ Id=3, Name="Pinapple Extravaganza" }
    };

    public static List<Pizza> GetPizzas() { return _pizzas; }

    public static Pizza ? GetPizza(int id)
    {
        return _pizzas.SingleOrDefault(pizza => pizza.Id == id);
    }

    public static bool IsValid(Pizza pizza)
    {
        if(string.IsNullOrEmpty(pizza.Name)) return false;
        Pizza? existing = PizzaDB.GetPizza(pizza.Id);
        if (existing != null) return false;
        return true;
    }

    public static Pizza CreatePizza(Pizza pizza)
    {
        Pizza? existing = PizzaDB.GetPizza(pizza.Id);
        if(existing != null) {
            throw new InvalidOperationException("A pizza with that Id already exists.");
        }
        _pizzas.Add(pizza);
        return pizza;
    }

    public static Pizza UpdatePizza(Pizza update)
    {
        _pizzas = _pizzas.Select(pizza =>
        {
            if(pizza.Id == update.Id)
            {
                pizza.Name = update.Name;
            }
            return pizza;
        }).ToList();
        return update;
    }

    public static void RemovePizza(int id)
    {
        _pizzas = _pizzas.FindAll(pizza => pizza.Id != id).ToList();
    }
}