using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TriTueNhanTao
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] type = TinhToan.getType(@"data/Type.txt");
            MiniMax m = new MiniMax(type[0], type[1]);
            char[,] board = TinhToan.readFile(@"data/board.txt");
            int level = TinhToan.getLevel(@"data/level.txt");
            m.setBoard(board);
            int[] move = m.findBestMove(level);
            Console.WriteLine(move[0].ToString() + "|" + move[1].ToString());
            TinhToan.setBoard(move, board, type[0]);
            TinhToan.printBoard(board);
            TinhToan.writeNextMove(move, @"data/nextMove.txt");
            
        }
    }
}
