namespace ChessLogic
{
    public class Player
    {
        public PieceColor Color { get; private set; }
        public bool IsHuman { get; private set; }
        private ChessBot bot;

        public Player(PieceColor color, bool isHuman, string difficulty = "easy")
        {
            Color = color;
            IsHuman = isHuman;
            if (!isHuman)
            {
                bot = new ChessBot(difficulty);
            }
        }

        public Move MakeMove(Board board)
        {
            if (IsHuman)
            {
                // Handle human move input (likely from UI)
                throw new NotImplementedException("Human move logic not implemented.");
            }
            else
            {
                return bot.MakeMove(board, Color);
            }
        }
    }
}