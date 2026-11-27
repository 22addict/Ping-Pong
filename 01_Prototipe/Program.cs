int width = 80;
int length = 25;

int ballX = width / 2;
int ballY = length / 2;
int ballDirX = 1;
int ballDirY = 1;


int paddleSize = 3;

int leftPaddleX = 1;
int leftPaddleY = length / 2;

int rightPaddleX = 78;
int rightPaddleY = length / 2;


int leftPlayerScore = 0;
int rightplayerScore = 0;

while (true)
{
    DrawPlace();
    PaddleKeys();
    MoveBall();

    if (leftPlayerScore == 3)
    {
        Console.WriteLine("Победил левый игрок!");
        break;
    }

    if (rightplayerScore == 3)
    {
        Console.WriteLine("Победил правый игрок!");
        break;
    }
}

void DrawPlace()
{
    Console.Clear();
    GameScore();
    for (int y = 0; y < length; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (x == width - 1 || x == 0 || y == length - 1 || y == 0)
            {
                Console.Write("#");
            }
            else if (x == ballX && y == ballY)
            {
                Console.Write("O");
            }
            else if (x == leftPaddleX && y >= leftPaddleY && y < leftPaddleY + paddleSize)
            {
                Console.Write("|");
            }
            else if (x == rightPaddleX && y >= rightPaddleY && y < rightPaddleY + paddleSize)
            {
                Console.Write("|");
            }
            else
            {
                Console.Write(" ");
            }
        }
        Console.WriteLine();
    }
}

void PaddleKeys()
{
    ConsoleKeyInfo key = Console.ReadKey(true);

    if (key.Key == ConsoleKey.A)
    {
        if (leftPaddleY > 1)
        {
            leftPaddleY--;
        }
        else
        {
            leftPaddleY = 1;
        }
    }
    else if (key.Key == ConsoleKey.Z)
    {
        if (leftPaddleY < length - 4)
        {
            leftPaddleY++;
        }
        else
        {
            leftPaddleY = length - 4;
        }
    }

    if (key.Key == ConsoleKey.K)
    {
        if (rightPaddleY > 1)
        {
            rightPaddleY--;
        }
        else
        {
            rightPaddleY = 1;
        }
    }
    else if (key.Key == ConsoleKey.M)
    {
        if (rightPaddleY < length - 4)
        {
            rightPaddleY++;
        }
        else
        {
            rightPaddleY = length - 4;
        }
    }
}

void MoveBall()
{
    if (ballY == length - 2)
    {
        ballDirY = -ballDirY;
    }

    if (ballY == length - 24)
    {
        ballDirY = -ballDirY;
    }

    if (ballX == rightPaddleX - 1 && (ballY == rightPaddleY || ballY == rightPaddleY + 1 || ballY == rightPaddleY + 2))
    {
        ballDirX = -ballDirX;
    }

    if (ballX == leftPaddleX + 1 && (ballY == leftPaddleY || ballY == leftPaddleY + 1 || ballY == leftPaddleY + 2))
    {
        ballDirX = -ballDirX;
    }

    if (ballX == 1 || ballX == 79)
    {
        ballX = width / 2;
        ballY = length / 2;

        leftPaddleX = 1;
        leftPaddleY = length / 2;

        rightPaddleX = 78;
        rightPaddleY = length / 2;
    }
    else
    {
        ballX = ballX + ballDirX;
        ballY = ballY + ballDirY;
    }
}

void GameScore()
{
    if (ballX == 1)
    {
        rightplayerScore++;
    }

    if (ballX == 79)
    {
        leftPlayerScore++;
    }
    Console.WriteLine($"Счет левого игрока {leftPlayerScore}. Счет правого игрока {rightplayerScore}");
}