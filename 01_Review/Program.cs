using System.ComponentModel;

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
    GetScore();
    Draw();
    ReadInputs();
    MoveBall();

    if (DrawWinner()) break;
}

void Draw()
{
    Console.Clear();
    Console.WriteLine($"Счет левого игрока {leftPlayerScore}. Счет правого игрока {rightplayerScore}");
    DrawField();
}

void DrawField()
{
    for (int i = 0; i < length; i++)
    {
        for (int j = 0; j < width; j++)
        {
            if (findBorderField(i, j)) Console.Write("#");
            else if (j == ballX && i == ballY) Console.Write("O");
            else if (findBorderPaddles(leftPaddleX, leftPaddleY, j, i)) Console.Write("|");
            else if (findBorderPaddles(rightPaddleX, rightPaddleY, j, i)) Console.Write("|");
            else Console.Write(" ");
        }
        Console.WriteLine();
    }
}

bool findBorderField(int x, int y)
{
    return x == 0 || x == length - 1 || y == 0 || y == width - 1;
}

bool findBorderPaddles(int PaddleX, int PaddleY, int x, int y)
{
    return x == PaddleX && y >= PaddleY && y < PaddleY + paddleSize;
}

void GetScore()
{
    int leftBoard = width - width + 1;
    int RightBoard = width - 1;

    if (ballX == leftBoard) rightplayerScore++;
    if (ballX == RightBoard) leftPlayerScore++;

    if (ballX == leftBoard || ballX == RightBoard) resetBallAndPaddle();
}

void ReadInputs()
{
    ConsoleKeyInfo key = Console.ReadKey(true);

    int minY = length - length + 1;
    int maxY = length - paddleSize - 1;

    if (key.Key == ConsoleKey.A && leftPaddleY > minY) leftPaddleY--;
    if (key.Key == ConsoleKey.Z && leftPaddleY < maxY) leftPaddleY++;
    if (key.Key == ConsoleKey.K && rightPaddleY > minY) rightPaddleY--;
    if (key.Key == ConsoleKey.M && rightPaddleY < maxY) rightPaddleY++;
}

void MoveBall()
{
    if (isUpOrDownBorder()) ballDirY = -ballDirY;
    if (isRightOrLwftBorder()) ballDirX = -ballDirX;

    ballX = ballX + ballDirX;
    ballY = ballY + ballDirY;
}

bool isUpOrDownBorder()
{
    int minY = length - 2;
    int maxY = length - length + 1;

    return ballY == minY || ballY == maxY;
}

bool isRightOrLwftBorder()
{
    bool isRigthBorder = ballX == rightPaddleX - 1 && (ballY == rightPaddleY || ballY == rightPaddleY + 1 || ballY == rightPaddleY + 2);
    bool isLeftBorder = ballX == leftPaddleX + 1 && (ballY == leftPaddleY || ballY == leftPaddleY + 1 || ballY == leftPaddleY + 2);
    return isRigthBorder || isLeftBorder;
}

void resetBallAndPaddle()
{
    ballX = width / 2;
    ballY = length / 2;

    leftPaddleX = 1;
    leftPaddleY = length / 2;

    rightPaddleX = 78;
    rightPaddleY = length / 2;
}

bool DrawWinner()
{
    if (leftPlayerScore == 3)
    {
        Console.WriteLine("Победил левый игрок!");
        return true;
    }

    if (rightplayerScore == 3)
    {
        Console.WriteLine("Победил правый игрок!");
        return true;
    }

    return false;
}