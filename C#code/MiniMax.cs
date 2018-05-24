using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TriTueNhanTao
{
    class MiniMax
    {
        public List<int[]> cacNuocDaDiX = new List<int[]>();
        public List<int[]> cacNuocDadiO = new List<int[]>();
        private char computerType;
        private char playerType;
        private char[,] game_board = DuLieuPhanTich.BOARD;
        public MiniMax(char computerType, char playerType)
        {
            this.computerType = computerType;
            this.playerType = playerType;
        }
        public void setMove(int[] move, char type)
        {
            int x = move[0];
            int y = move[1];
            if(type == 'X')
            {
                if (this.game_board[y,x] == '#')
                {
                    this.cacNuocDaDiX.Insert(0, move);
                    this.game_board[y, x] = 'X';
                }else if(type == 'O')
                {
                    if (this.game_board[y, x] == '#')
                    {
                        this.cacNuocDadiO.Insert(0, move);
                        this.game_board[y, x] = 'O';
                    }
                }
            }
            TinhToan.printBoard(this.game_board);
        }
        public void setBoard(char[,] board)
        {
            game_board = board;
        }
        public int[] findBestMove(int d)
        {
            int nuocHayVL = 10;
            List<int[]> coTheDi = TimNuocTrongBanCo.tapCacNuocCoTheDi(game_board, false);
            List<int[]> coTheDi_2 = TimNuocTrongBanCo.tapCacNuocCoTheDi(game_board, true);
            int maxScore = int.MinValue;
            //int minScore = int.MaxValue;
            int[] bestMove = new int[] { };
            int depht = d;
            List<int[]> tapNuocDuocChon = new List<int[]>();
            bool flag = true;
            List<int[]> nuoc_hay_X = new List<int[]>();
            List<int[]> nuoc_hay_O = new List<int[]>();          
            foreach(int[] move in coTheDi_2)
            {
                TinhToan.setBoard(move, game_board, 'X');
                if(TinhDiemBanCo.chacChanWin('X', game_board))
                {
                    TinhToan.removeBoard(move, game_board);
                    return move;
                }
                TinhToan.removeBoard(move, game_board);
            }
            foreach(int[] move in coTheDi_2)
            {
                TinhToan.setBoard(move, game_board, 'O');
                if (TinhDiemBanCo.chacChanWin('O', game_board))
                {
                    tapNuocDuocChon.Add(move);
                    foreach (int[] move1 in coTheDi_2)
                    {
                        if (move1[0] != move[0] && move1[1] != move[1])
                        {
                            TinhToan.setBoard(move1, game_board, 'X');
                            if(!TinhDiemBanCo.chacChanWin('O', game_board))
                            {
                                tapNuocDuocChon.Add(move1);
                            }
                            TinhToan.removeBoard(move1, game_board);
                        }
                    }
                }
                TinhToan.removeBoard(move, game_board);
            }
            if (flag && tapNuocDuocChon.Count == 0)
            {
                foreach(int[] move in coTheDi_2)
                {
                    int VKL_1 = TinhDiemBanCo.nuocDacBietNewVersion(move[0], move[1], 'X', game_board);
                    if (VKL_1 < nuocHayVL && VKL_1 > 0)
                    {
                        nuoc_hay_X.Clear();
                        nuoc_hay_X.Add(move);
                        nuocHayVL = VKL_1;
                    }else if(VKL_1 == nuocHayVL)
                    {
                        nuoc_hay_X.Add(move);
                    }
                }
                foreach (int[] move in coTheDi_2)
                {
                    int VKL_2 = TinhDiemBanCo.nuocDacBietNewVersion(move[0], move[1], 'O', game_board);
                    if (VKL_2 < nuocHayVL && VKL_2 > 0)
                    {
                        nuoc_hay_O.Clear();
                        nuoc_hay_O.Add(move);
                        nuocHayVL = VKL_2;
                    }
                }
                if (nuoc_hay_X.Count != 0 || nuoc_hay_O.Count != 0)
                {
                    tapNuocDuocChon = TinhToan.cong2List(nuoc_hay_X, nuoc_hay_O);
                }
            }
            if (tapNuocDuocChon.Count == 1)
            {
                return tapNuocDuocChon[0];
            }
            if (tapNuocDuocChon.Count == 0)
            {
                tapNuocDuocChon = TinhDiemBanCo.chonNuocToiUu(coTheDi, computerType, game_board);
            }
            foreach(int[] move in tapNuocDuocChon)
            {
                TinhToan.setBoard(move, game_board, 'X');
                int temp = Score(maxScore, int.MaxValue, depht, 'O', game_board);
                TinhToan.removeBoard(move, game_board);
                Console.WriteLine(move[0].ToString() + ","+ move[1].ToString() + ":" + temp.ToString());
                if (temp == DuLieuPhanTich.MAXSCORE)
                {
                    return move;
                }
                if (temp >= maxScore)
                {
                    bestMove = move;
                    maxScore = temp;
                }
            }
            return bestMove;
        }
        private int Score(int min, int max, int depht, char type, char[,] board)
        {
            int maxScore = max;
            int minScore = min;
            List<int[]> coTheDi = TimNuocTrongBanCo.tapCacNuocCoTheDi(board, true);
            if(type == 'X')
            {
                if (TinhDiemBanCo.gameWin('X', board))
                {
                    return DuLieuPhanTich.MAXSCORE;
                }
            }
            else
            {
                if(TinhDiemBanCo.gameWin('O', board))
                {
                    return DuLieuPhanTich.MINSCORE;
                }
            }
            if (depht == 0)
            {
                int[] x = TinhDiemBanCo.tongDoNguyHiem('X', board);
                int[] o = TinhDiemBanCo.tongDoNguyHiem('O', board);
                int score = TinhToan.diemCuaMaTran(x) - TinhToan.diemCuaMaTran(o);
                return score;
            }
            List<int[]> tapNuocDuocChon = TinhDiemBanCo.chonNuocToiUu(coTheDi, type, board);
            foreach(int[] move in tapNuocDuocChon)
            {
                if(type == 'X')
                {
                    TinhToan.setBoard(move, board, 'X');
                    int temp = Score(minScore, maxScore, depht - 1, 'O', board);
                    TinhToan.removeBoard(move, board);
                    if(temp >= maxScore)
                    {
                        return DuLieuPhanTich.MAXSCORE;
                    }
                    else if(minScore < temp && temp < maxScore)
                    {
                        minScore = temp;
                    }
                    else if(minScore == maxScore)
                    {
                        return minScore;
                    } 
                }
                else
                {
                    TinhToan.setBoard(move, board, 'O');
                    int temp = Score(minScore, maxScore, depht - 1, 'X', board);
                    TinhToan.removeBoard(move, board);
                    if (temp <= minScore)
                    {
                        return DuLieuPhanTich.MINSCORE;
                    }
                    else if (minScore < temp && temp < maxScore)
                    {
                        maxScore = temp;
                    }
                    else if (minScore == maxScore)
                    {
                        return maxScore;
                    }
                }
            }
            if(type == 'X')
            {
                return minScore;
            }
            else
            {
                return maxScore;
            }
        }
    }
}
