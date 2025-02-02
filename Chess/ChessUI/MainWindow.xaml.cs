using System.Windows;
using ChessLogic;
namespace ChessUI
{
    public partial class MainWindow : Window
    {
        private Board board;
        private Player humanPlayer;
        private Player botPlayer;
        private PieceColor currentPlayer;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            board = new Board();
            humanPlayer = new Player(PieceColor.White, true);
            botPlayer = new Player(PieceColor.Black, false, "medium");
            currentPlayer = PieceColor.White;

            RenderBoard();
        }

        private void RenderBoard()
        {
            // Render the board and pieces
        }

        private void MakeMove(Move move)
        {
            board.MakeMove(move);
            RenderBoard();

            currentPlayer = (currentPlayer == PieceColor.White) ? PieceColor.Black : PieceColor.White;

            if (currentPlayer == botPlayer.Color)
            {
                Move botMove = botPlayer.MakeMove(board);
                MakeMove(botMove);
            }
        }
    }
}