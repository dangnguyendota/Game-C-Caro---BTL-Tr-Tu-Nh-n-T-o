using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TriTueNhanTao
{
    class TimNuocTrongBanCo
    {
        public static List<List<int[]>> tapCacNuoc(int x, int y)
        {
            List<List<int[]>> result = new List<List<int[]>>();
            int count = 0;
            foreach(int[][] i in DuLieuPhanTich.cacNuocXungQuanhMotNuoc)
            {
                int x1_temp = i[0][0] + x;
                int y1_temp = i[0][1] + y;
                int x2_temp = i[5][0] + x;
                int y2_temp = i[5][1] + y;
                if(!TinhToan.vuotKichThuocMang(x1_temp, y1_temp)  && !TinhToan.vuotKichThuocMang(x2_temp, y2_temp))
                {
                    result.Add(new List<int[]>());
                    foreach(int[] j in i)
                    {
                        result[count].Add(new int[] {j[0] + x, j[1] + y });
                    }
                    count++;
                }
            }

            return result;
        }

        public static  List<int[]> tapCacNuocCoTheDi(char[,] board, bool xetWin = false)
        {
            List<int[]> result = new List<int[]>();
            for(int x = 0; x < 15; x++)
            {
                for(int y = 0; y < 15; y++)
                {
                    if (board[y,x] != '#')
                    {
                        List<int[]> temp = cacNuocXungQuanh(x, y, xetWin);
                        foreach(int[] move in temp)
                        {
                            if (!TinhToan.chech_move_exist(move, result) && board[move[1],move[0]] == '#')
                            {
                                result.Add(move);
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static List<int[]> cacNuocXungQuanh(int x, int y, bool flag = false)
        {
            List<int[]> result = new List<int[]>();
            foreach(int[] i in DuLieuPhanTich.tap)
            {
                int x_temp = x + i[0];
                int y_temp = y + i[1];
                if(!TinhToan.vuotKichThuocMang(x_temp, y_temp))
                {
                    result.Add(new int[] {x_temp, y_temp});
                }
            }
            if (flag)
            {
                foreach (int[] i in DuLieuPhanTich.tap_1)
                {
                    int x_temp = x + i[0];
                    int y_temp = y + i[1];
                    if (!TinhToan.vuotKichThuocMang(x_temp, y_temp))
                    {
                        result.Add(new int[] { x_temp, y_temp });
                    }
                }
            }
            return result;
        }

    }
}
