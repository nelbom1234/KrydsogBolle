namespace KrydsogBolle;

class Program
{
    static void Main(string[] args)
    {
        string[][] board = [["","",""],["","",""],["","",""]]; // tracker for where characters have already been placed
        int turn = 0;
        string? result;
        int valg;

        while (turn < 9) {
            Board.DrawBoard(board); // draw the board
            Console.SetCursorPosition(5, 23);
            Console.WriteLine(turn % 2 == 0 ? "Kryds: Indtast dit valg <1-9>:" : "Bolle: Indtast dit valg <1-9>:"); // ask the person who's turn it is where they want to place
            result = Console.ReadLine();
            if (result == null) { // error if we get null input
                Console.WriteLine("Du skal give et gyldigt input. Tryk enter for at prøve igen");
                Console.ReadKey();
                continue;
            }
            if (!int.TryParse(result, out valg)) { // error if non-integer input
                Console.WriteLine("Du skal give et gyldigt input. Tryk enter for at prøve igen");
                Console.ReadKey();
                continue;
            }
            if (valg < 1 || valg > 9) { // error if input not between 1 and 9
                Console.WriteLine("Du skal give et tal mellem 1 og 9. tryk enter for at prøve igen");
                Console.ReadKey();
                continue;
            }
            int x = (valg-1) % 3; // x value for the position in 2d space
            int y = (valg-1) / 3; // y value for the position in 2d space
            if (board[y][x] != "") { // error if position already taken
                Console.WriteLine("Det felt er allerede taget. tryk enter for at prøve igen");
                Console.ReadKey();
                continue;
            }
            if (turn % 2 == 0) { 
                board[y][x] = "X"; // if it was X's turn fill in with X
            }
            else { //
                board[y][x] = "O"; // if it was O's turn fill in with O
            }
            int winner = checkWin(board);
            if (winner != 0) { // if someone has succesfully won
                Finish(board, winner);
                return;
            }
            turn++;
        }
        Finish(board, 0); // if we're this far, it's a tie
    }

    // method to check if the game is over
    static int checkWin(string[][] board) {
        (int,int)[][] winConditions = // ways to get 3 in a row
        [
            [(0,0),(1,1),(2,2)],
            [(0,2),(1,1),(2,0)],
            [(0,0),(0,1),(0,2)],
            [(1,0),(1,1),(1,2)],
            [(2,0),(2,1),(2,2)],
            [(0,0),(1,0),(2,0)],
            [(0,1),(1,1),(2,1)],
            [(0,2),(1,2),(2,2)]
        ];

        foreach((int,int)[] i in winConditions) {
            string check = "";
            int inARow = 0; // number of identicals we've found in this win condition
            foreach((int y, int x) in i) {
                string curr = board[y][x];
                if (curr == "") break; // we know at least one hasn't been set so this isn't a win
                check = check == "" ? curr : check; // set check to curr if it hasn't been set i.e. we are on first in the win condition
                if (curr != check) break;
                inARow++;
            }
            if (inARow == 3) {
                return check == "X" ? 1 : 2; //if found 3 in a row, return 1 if X won and 2 if O won
            }
        }
        return 0; // return 0 if noone won
    }

    // method to print the final end screen
    static void Finish (string[][] board, int winner) {
        Board.DrawBoard(board); //draw the board
        Console.SetCursorPosition(5, 23);
        if (winner == 0) { // if a tie
            Console.WriteLine("Den blev uafgjort");
        }
        else if (winner == 1) { // if X won
            Console.WriteLine("Tillykke kryds, du vandt");
        }
        else { // if O won
            Console.WriteLine("Tillykke bolle, du vandt");
        }
        Console.ReadKey();
    }

}
