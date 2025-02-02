using System;
using System.Collections.Generic;

namespace ChessLogic
{
    public class ChessBot
    {
        private Random random = new Random();
        private string difficulty;

        public ChessBot(string difficulty)
        {
            this.difficulty = difficulty;
        }

        public Move MakeMove(Board board, PieceColor color)
        {
            List<Move> possibleMoves = GetAllPossibleMoves(board, color);

            if (possibleMoves.Count == 0)
            {
                throw new InvalidOperationException("No possible moves!");
            }

            // Difficulty-based move selection
            switch (difficulty)
            {
                case "easy":
                    return possibleMoves[random.Next(possibleMoves.Count)];
                case "medium":
                    return SelectMediumMove(possibleMoves, board);
                case "hard":
                    return SelectHardMove(possibleMoves, board);
                default:
                    return possibleMoves[random.Next(possibleMoves.Count)];
            }
        }

        private List<Move> GetAllPossibleMoves(Board board, PieceColor color)
        {
            List<Move> moves = new List<Move>();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Piece piece = board.GetPiece(x, y);
                    if (piece != null && piece.Color == color)
                    {
                        moves.AddRange(board.GetValidMoves(x, y));
                    }
                }
            }

            return moves;
        }

        private Move SelectMediumMove(List<Move> moves, Board board)
        {
            // Prioritize capturing opponent pieces
            foreach (var move in moves)
            {
                if (board.GetPiece(move.ToX, move.ToY) != null)
                {
                    return move;
                }
            }
            return moves[random.Next(moves.Count)];
        }

        private Move SelectHardMove(List<Move> moves, Board board)
        {
            // Implement a simple MiniMax algorithm for hard difficulty
            // This is a placeholder; you can expand it further
            int bestScore = int.MinValue;
            Move bestMove = moves[0];

            foreach (var move in moves)
            {
                Board simulatedBoard = board.Clone();
                simulatedBoard.MakeMove(move);

                int score = EvaluateBoard(simulatedBoard);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }

            return bestMove;
        }

        private int EvaluateBoard(Board board)
        {
            // Simple evaluation function: count material advantage
            int score = 0;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Piece piece = board.GetPiece(x, y);
                    if (piece != null)
                    {
                        int value = GetPieceValue(piece.Type);
                        score += (piece.Color == PieceColor.White) ? value : -value;
                    }
                }
            }
            return score;
        }

        private int GetPieceValue(PieceType type)
        {
            switch (type)
            {
                case PieceType.Pawn: return 1;
                case PieceType.Knight: return 3;
                case PieceType.Bishop: return 3;
                case PieceType.Rook: return 5;
                case PieceType.Queen: return 9;
                case PieceType.King: return 100;
                default: return 0;
            }
        }
    }
}