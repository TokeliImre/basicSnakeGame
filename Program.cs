using SnakeGame;

Cordinate gridDimension = new Cordinate(50, 30);
Cordinate snakePos = new Cordinate(10, 4);
List<Cordinate> snakeBody = new List<Cordinate>(); // Snake's body
Random rand = new Random();
Cordinate applePos = new Cordinate(rand.Next(1, gridDimension.X - 1), rand.Next(1, gridDimension.Y - 1));
int frameDelayMili = 150;

// Initial direction (right)
string currentDirection = "right";

// Score variable
int score = 0;

// Draw the border once
Console.Clear();
for (int y = 0; y < gridDimension.Y; y++)
{
	for (int x = 0; x < gridDimension.X; x++)
	{
		if (x == 0 || x == gridDimension.X - 1 || y == 0 || y == gridDimension.Y - 1)
		{
			Console.Write("#");
		}
		else
		{
			Console.Write(" ");
		}
	}
	Console.WriteLine();
}

// Game loop
while (true)
{
	// Clear the snake from the screen (write space)
	Console.SetCursorPosition(snakePos.X, snakePos.Y);
	Console.Write(" ");

	foreach (var part in snakeBody)
	{
		Console.SetCursorPosition(part.X, part.Y);
		Console.Write(" ");
	}

	// Change direction if user input is available
	if (Console.KeyAvailable)
	{
		var key = Console.ReadKey(true).Key;

		switch (key)
		{
			case ConsoleKey.W:
				currentDirection = "up"; // Up direction
				break;
			case ConsoleKey.S:
				currentDirection = "down"; // Down direction
				break;
			case ConsoleKey.A:
				currentDirection = "left"; // Left direction
				break;
			case ConsoleKey.D:
				currentDirection = "right"; // Right direction
				break;
		}
	}

	// Move the snake's head based on the direction
	Cordinate newHeadPos = snakePos;
	switch (currentDirection)
	{
		case "up":
			newHeadPos = new Cordinate(snakePos.X, snakePos.Y - 1); // Move up
			break;
		case "down":
			newHeadPos = new Cordinate(snakePos.X, snakePos.Y + 1); // Move down
			break;
		case "left":
			newHeadPos = new Cordinate(snakePos.X - 1, snakePos.Y); // Move left
			break;
		case "right":
			newHeadPos = new Cordinate(snakePos.X + 1, snakePos.Y); // Move right
			break;
	}

	// Check if the snake hits the border (game over)
	if (newHeadPos.X <= 0 || newHeadPos.X >= gridDimension.X - 1 || newHeadPos.Y <= 0 || newHeadPos.Y >= gridDimension.Y - 1)
	{
		Console.Clear();
		Console.SetCursorPosition(gridDimension.X / 2 - 10, gridDimension.Y / 2);
		Console.WriteLine($"Game Over! You hit the wall. Your score: {score}");
		break; // Exit the game loop
	}

	// Update the snake's body (each part moves to the previous position)
	if (snakeBody.Count > 0)
	{
		for (int i = snakeBody.Count - 1; i > 0; i--)
		{
			snakeBody[i] = snakeBody[i - 1];
		}
		snakeBody[0] = snakePos; // Set head position to the first part
	}

	// Set the snake's head to the new position
	snakePos = newHeadPos;

	// Check if the snake crashes into itself
	if (snakeBody.Any(part => part.X == snakePos.X && part.Y == snakePos.Y))
	{
		Console.Clear();
		Console.SetCursorPosition(gridDimension.X / 2 - 10, gridDimension.Y / 2);
		Console.WriteLine($"Game Over! You crashed into yourself. Your score: {score}");
		break; // Exit the game loop
	}

	// Check if the snake has eaten the apple
	if (snakePos.X == applePos.X && snakePos.Y == applePos.Y)
	{
		// New apple position
		applePos = new Cordinate(rand.Next(1, gridDimension.X - 1), rand.Next(1, gridDimension.Y - 1));

		// Grow the snake (add a new part to the end of the body)
		snakeBody.Add(new Cordinate(snakePos.X, snakePos.Y));

		// Increase score
		score++;
	}

	// Draw the snake's head
	Console.SetCursorPosition(snakePos.X, snakePos.Y);
	Console.Write("O");

	// Draw the snake's body
	foreach (var part in snakeBody)
	{
		Console.SetCursorPosition(part.X, part.Y);
		Console.Write("o");
	}

	// Draw the apple
	Console.SetCursorPosition(applePos.X, applePos.Y);
	Console.Write("a");

	// Wait for the frame delay
	Thread.Sleep(frameDelayMili);
}
public class Cordinate{
	private int x;
	private int y;

    public int X { get { return x; }  }
	public int Y { get { return y; } }


    public Cordinate(int x ,int y)
    {
        this.x = x;
        this.y = y;
    }

	public override bool Equals(object? obj)
	{
        if (obj==null || !GetType().Equals(obj.GetType()))
        {
            return false;
        }

        
        Cordinate other = (Cordinate)obj;
        return x==other.x && y==other.y;    
    }
}




}

