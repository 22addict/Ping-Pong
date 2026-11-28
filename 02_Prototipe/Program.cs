bool main = true;
bool menu = true;
bool game;

int length = 27;
int width = 120;

int ballY = length / 2;
int ballX = width / 2;
int ballDirY = 1;
int ballDirX = 1;


int paddleSize = 5;
int halfPaddle = paddleSize / 2;
int paddleGap = 1;

int minPaddleY = halfPaddle + 1;
int maxPaddleY = length - 2 - halfPaddle;

int leftPaddleY = length / 2;
int leftPaddleX = paddleGap;
int rightPaddleY = length / 2;
int rightPaddleX = width - 1 - paddleGap;

int leftPlayer = 0;
int rightPlayer = 0;

Console.CursorVisible = false;
while (main)
{
    MainGame();
}
Console.CursorVisible = true;


void MainGame()
{
    while (menu)
    {
        Console.WriteLine("~~~ Пинг-Пинг ~~~\n");
        Console.WriteLine("Меню:\n'1' Играть\n'2' Выйти");
        Console.Write("Жду ввода...");

        ConsoleKeyInfo key = Console.ReadKey(true);
        switch (key.Key)
        {
            case ConsoleKey.D1:
                game = true;
                while (game)
                {
                    menu = false;
                    Console.Clear();
                    DrawField();
                    KeyControlsInGame();
                    if (game) MoveBall();

                    Thread.Sleep(50);
                }
                break;
            case ConsoleKey.D2:
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\nВы уверенны, что хотите выйти?");
                    Console.WriteLine("Нажмите '1', если уверены");
                    Console.WriteLine("Нажмите '2', если хотите вернутся в меню");
                    Console.Write("Жду ввода...");
                    ConsoleKeyInfo exitOrNot = Console.ReadKey(true);

                    switch (exitOrNot.Key)
                    {
                        case ConsoleKey.D1:
                            menu = false;
                            main = false;
                            Console.Clear();
                            break;
                        case ConsoleKey.D2:
                            Console.Clear();
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nError! Вы нажали неправильную клавишу.");
                            Console.ResetColor();
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                    break;
                }
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError! Вы нажали неправильную клавишу.");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                break;
        }
    }
}

void DrawField()
{
    GameScore();
    if (!game) return;
    for (int y = 0; y < length; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (y == length - 1 || y == length - length || x == width - 1 || x == width - width)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("#");
                Console.ResetColor();
            }
            else if (y == ballY && x == ballX)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("O");
                Console.ResetColor();
            }
            else if (y >= leftPaddleY - halfPaddle && y <= leftPaddleY + halfPaddle && x == leftPaddleX)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("|");
                Console.ResetColor();
            }
            else if (y >= rightPaddleY - halfPaddle && y <= rightPaddleY + halfPaddle && x == rightPaddleX)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("|");
                Console.ResetColor();
            }
            else Console.Write(" ");
        }
        Console.WriteLine();
    }
}

void KeyControlsInGame()
{
    if (Console.KeyAvailable)
    {
        KeyInitInGame();
    }
}

void KeyInitInGame()
{
    ConsoleKeyInfo moveKey = Console.ReadKey(true);
    switch (moveKey.Key)
    {
        case ConsoleKey.A:
            leftPaddleY -= paddleSize;
            if (leftPaddleY < minPaddleY) leftPaddleY = minPaddleY;
            break;
        case ConsoleKey.K:
            rightPaddleY -= paddleSize;
            if (rightPaddleY < minPaddleY) rightPaddleY = minPaddleY;
            break;
        case ConsoleKey.Z:
            leftPaddleY += paddleSize;
            if (leftPaddleY > maxPaddleY) leftPaddleY = maxPaddleY;
            break;
        case ConsoleKey.M:
            rightPaddleY += paddleSize;
            if (rightPaddleY > maxPaddleY) rightPaddleY = maxPaddleY;
            break;
        case ConsoleKey.Escape:
            leftPlayer = 0;
            rightPlayer = 0;
            game = false;
            Console.Clear();
            menu = true;
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Игра на паузе.");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Левая ракетка: клавиши 'A' | 'Z'");
            Console.WriteLine("Правая ракетка: клавиши 'K' | 'M'");
            Console.WriteLine("Что бы выйти в меню, нажмите 'Esc'");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            break;
    }
}

void MoveBall()
{
    ballX += ballDirX;
    ballY += ballDirY;

    if (ballY <= length - (length - 1) || ballY >= length - 2) ballDirY = -ballDirY;

    if (ballX == leftPaddleX + 1 && ballY >= leftPaddleY - halfPaddle && ballY <= leftPaddleY + halfPaddle) ballDirX = -ballDirX;
    if (ballX == rightPaddleX - 1 && ballY >= rightPaddleY - halfPaddle && ballY <= rightPaddleY + halfPaddle) ballDirX = -ballDirX;

    if (ballX <= 0 || ballX >= width - 1)
    {
        if (ballX >= width - 1) leftPlayer++;
        else if (ballX <= 0) rightPlayer++;

        ballY = length / 2;
        ballX = width / 2;

        ballDirX = -ballDirX;

        leftPaddleY = length / 2;
        leftPaddleX = paddleGap;
        rightPaddleY = length / 2;
        rightPaddleX = width - 1 - paddleGap;
    }
}

void GameScore()
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("======= Игровой счет =======");
    Console.ResetColor();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write($"== Левый игрок: {leftPlayer}");
    Console.ResetColor();
    Console.Write(" || ");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write($"Правый игрок: {rightPlayer} ==\n");
    Console.ResetColor();

    if (leftPlayer == 3)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        PlayerWin("Победил левый игрок!");
        Console.ResetColor();
    }
    else if (rightPlayer == 3)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        PlayerWin("Победил правый игрок!");
        Console.ResetColor();
    }
}

void PlayerWin(string win)
{
    Console.WriteLine(win);
    Console.ReadKey();
    Console.ReadKey();
    Console.Clear();
    rightPlayer = 0;
    leftPlayer = 0;
    game = false;
    menu = true;
}