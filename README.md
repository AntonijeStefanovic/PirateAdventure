# Pirate Adventure Game

## What's the Game About?

**Pirate Adventure** is a console game made in C# where you play as Pirate Roberts exploring a 10x10 map. The map has different areas like oceans, forests, deserts, caves, and land. Your goal is to collect special items (like a boat, axe, torch, and water) to help you move around and fight enemies. 

### Enemies & Challenges:
- **Megalodon** (in the Ocean) → Beat it by guessing a random number
- **Sphinx** (in the Desert) → Solve a riddle to win
- **Giant Spider** (in the Forest) → Play Rock-Paper-Scissors-Lizard-Spock
- **Goblin** (in the Cave) → Solve a math problem

The game shows how to use design patterns (Factory, Observer, Strategy) to keep the code clean and easy to update.

## Map Symbols

Here's what the symbols mean in the game:

```csharp
private void DisplayLegend()
{
    Console.WriteLine("Symbol Legend:");
    Console.WriteLine("P : Player");
    Console.WriteLine("? : Undiscovered Tile");
    Console.WriteLine(". : Land Tile");
    Console.WriteLine("~ : Ocean Tile");
    Console.WriteLine("F : Forest Tile");
    Console.WriteLine("D : Desert Tile");
    Console.WriteLine("C : Cave Tile");
    Console.WriteLine("B : Boat");
    Console.WriteLine("T : Treasure");
    Console.WriteLine("X : Axe");
    Console.WriteLine("L : Torch");
    Console.WriteLine("W : Drinking Water");
    Console.WriteLine("M : Megalodon");
    Console.WriteLine("S : Giant Spider");
    Console.WriteLine("Q : Sphinx");
    Console.WriteLine("G : Goblin");
    Console.WriteLine();
}
```

## Problem Addressed

The project illustrates how classic design patterns can be applied to create a flexible, scalable, and engaging game. It demonstrates:
- **How to manage object creation** (using the Factory Pattern)
- **How to decouple game events from the UI** (using the Observer Pattern)
- **How to support flexible combat behavior** (using the Strategy Pattern)

## Design Decisions

### Factory Pattern
- **Why:**  
  Centralizing object creation simplifies adding new types of tiles, items, and enemies.  
- **How:**  
  `GameFactory.cs` encapsulates all object creation logic. When new terrains or enemy types are added, only this file needs updating.
- **Benefits:**  
  Enhanced modularity and ease of future expansion.

### Observer Pattern
- **Why:**  
  To decouple game logic from the user interface and ensure that event notifications (e.g., movement, item pickups, combat interactions) are managed cleanly.
- **How:**  
  `GameEventManager.cs` implements an event notification system that allows various parts of the game to subscribe to events. This system makes it easy to add features like an event log or on-screen alerts.
- **Benefits:**  
  Improved separation of concerns and simplified debugging, as all events are centrally managed.

### Strategy Pattern
- **Why:**  
  Different enemy types require different combat mechanisms. Using the Strategy Pattern allows these combat behaviors to be encapsulated and switched dynamically at runtime.
- **How:**  
  The `ICombatStrategy` interface defines the combat contract. Concrete implementations include:
  - **RiddleCombatStrategy:**
  - **MathChallengeCombatStrategy:**
  - **RPSLizardSpockCombatStrategy:**
  - **RandomGuessCombatStrategy:**
- **Benefits:**  
  The flexibility to add or modify combat mechanisms without affecting the overall game loop or engine logic.

## Insights Gained

- **Modular Code Design:**  
  Separating different functionalities into distinct modules (Game, Characters, Items, Tiles, Patterns) greatly improved code readability and maintainability.
- **Design Patterns in Practice:**  
  Implementing these patterns from scratch provided a deeper understanding of their advantages, particularly in making the application extensible and easy to modify.
- **Future Enhancements:**  
  The architecture supports further extensions (e.g., new terrains, enemy behaviors, or interactive elements) with minimal changes to the core codebase.
