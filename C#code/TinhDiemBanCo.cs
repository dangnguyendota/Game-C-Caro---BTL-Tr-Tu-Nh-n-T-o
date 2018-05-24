using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TriTueNhanTao
{
    class TinhDiemBanCo
    {
        public static int[] tongDoNguyHiem(char type, char[,] board)
        {
            int[] result = new int[6] { 0, 0, 0, 0, 0, 0 };
            char type_I = TinhToan.notType(type);
            List<int[]> daDiCMNR = new List<int[]>();
            for(int x = 0; x < 15; x++)
            {
                for(int y = 0; y < 15; y++)
                {
                    if(board[y,x] == type)
                    {
                        result = TinhToan.add(result, doNguyHiemCuaNuoc(x, y, type, board));
                        int[] move_temp = new int[] { x, y };
                        TinhToan.setBoard(move_temp, board, type_I);
                        daDiCMNR.Add(move_temp);
                    }
                }
            }
            foreach(int[] move in daDiCMNR)
            {
                TinhToan.setBoard(move, board, type);
            }
            return result;
        }

        public static int[] doNguyHiemCuaNuoc(int x, int y, char type, char[,] board)
        {
            char type_I = TinhToan.notType(type);
            List<List<int[]>> temp = TimNuocTrongBanCo.tapCacNuoc(x, y);
            int[] result = new int[6] { 0, 0, 0, 0, 0, 0 };
            foreach(List<int[]> i in temp)
            {
                int[] nhanBiet = new int[6] { 0, 0, 0, 0, 0, 0 };
                for (int k = 0; k < 6; k++)
                {
                    int[] j = i[k];
                    if(board[j[1], j[0]] == type)
                    {
                        nhanBiet[k] = 1;
                    }else if(board[j[1], j[0]] == type_I)
                    {
                        nhanBiet[k] = 2;
                    }
                }
                if (TinhToan.Contains_12(nhanBiet, DuLieuPhanTich.strongQuadraMoves))
                {
                    result[0]++;
                }else if (TinhToan.Contains_12(nhanBiet, DuLieuPhanTich.weakQuadraMoves))
                {
                    result[1]++;
                }else if (TinhToan.Contains_12(nhanBiet, DuLieuPhanTich.strongTripleMoves))
                {
                    result[2]++;
                }else if (TinhToan.Contains_12(nhanBiet, DuLieuPhanTich.weakTripleMoves))
                {
                    result[3]++;
                }else if (TinhToan.Contains_12(nhanBiet, DuLieuPhanTich.strongDoubleMoves))
                {
                    result[4]++;
                }else if (TinhToan.Contains_12(nhanBiet, DuLieuPhanTich.weakDoubleMoves))
                {
                    result[5]++;
                }
            }
            return result;
        }

        public static List<int[]> thoaMan(int[][] tapConTrai, int[][] tapConGai, int[][] tapConNuoi, char type, char[,] board, bool flag)
        {
            List<int[]> result = new List<int[]>();
            char type_I = TinhToan.notType(type);
            if (flag)
            {
                if(TinhToan.vuotKichThuocMang(tapConTrai[0][0], tapConTrai[0][1]) || board[tapConTrai[0][1], tapConTrai[0][0]] != type)
                {
                    return result;
                }
            }
            foreach(int[] move in tapConGai)
            {
                if(TinhToan.vuotKichThuocMang(move[0], move[1]) || board[move[1], move[0]] == type_I)
                {
                    return result;
                }
            }
            if (TinhToan.vuotKichThuocMang(tapConNuoi[0][0], tapConNuoi[0][1]) || board[tapConNuoi[0][1],tapConNuoi[0][0]] == type_I)
            {
                if(TinhToan.vuotKichThuocMang(tapConNuoi[1][0], tapConNuoi[1][1]) || board[tapConNuoi[1][1], tapConNuoi[1][0]] == type_I)
                {
                    return result;
                }
            }
            foreach(int[] move in tapConTrai)
            {
                if(TinhToan.vuotKichThuocMang(move[0], move[1]) || board[move[1], move[0]] == type_I)
                {
                    return new List<int[]>();
                }
                if (board[move[1], move[0]] == type && !TinhToan.chech_move_exist(move, result))
                {
                    result.Add(move);
                }
            }
            if (result.Count < 2)
            {
                return new List<int[]>();
            }else if(result.Count == 2)
            {
                if (TinhToan.vuotKichThuocMang(tapConNuoi[0][0], tapConNuoi[0][1]) || board[tapConNuoi[0][1], tapConNuoi[0][0]] == type_I)
                {
                    return new List<int[]>();
                }
                if (TinhToan.vuotKichThuocMang(tapConNuoi[1][0], tapConNuoi[1][1]) || board[tapConNuoi[1][1], tapConNuoi[1][0]] == type_I)
                {
                    return new List<int[]>();
                }
            }
            return result;
        }
        public static bool gameWin(char type, char[,] board)
        {
            List<int[]> cacNuocDiDuoc = TimNuocTrongBanCo.tapCacNuocCoTheDi(board, true);
            foreach(int[] i in cacNuocDiDuoc)
            {
                if(nuocDacBietNewVersion(i[0], i[1], type, board) > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool chacChanWin(char type, char[,] board)
        {
            for(int x = 0; x < 15; x++)
            {
                for(int y = 0; y < 15; y++)
                {
                    int[] move = new int[2] { x, y };
                    if(board[y,x] == type)
                    {
                        if (winCMNR(move, DuLieuPhanTich.set_1_XetWin[0], DuLieuPhanTich.set_1_XetWin[1], type, board))
                        {
                            return true;
                        }
                        if(winCMNR(move, DuLieuPhanTich.set_2_XetWin[0], DuLieuPhanTich.set_2_XetWin[1], type, board))
                        {
                            return true;
                        }
                        if (winCMNR(move, DuLieuPhanTich.set_3_XetWin[0], DuLieuPhanTich.set_3_XetWin[1], type, board))
                        {
                            return true;
                        }
                        if (winCMNR(move, DuLieuPhanTich.set_4_XetWin[0], DuLieuPhanTich.set_4_XetWin[1], type, board))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool winCMNR(int[] toaDo, int[][] tapConTrai, int[][] tapConGai, char type, char[,] board)
        {
            char type_I = TinhToan.notType(type);
            if (!TinhToan.vuotKichThuocMang2(tapConGai[0][0] + toaDo[0], tapConGai[0][1] + toaDo[1]) && TinhToan.vuotKichThuocMang(tapConGai[0][0] + toaDo[0], tapConGai[0][1] + toaDo[1]))
            {
                if (!TinhToan.vuotKichThuocMang2(tapConGai[1][0] + toaDo[0], tapConGai[1][1] + toaDo[1]) && TinhToan.vuotKichThuocMang(tapConGai[1][0] + toaDo[0], tapConGai[1][1] + toaDo[1]))
                {
                    return false;
                }
            }
            if (TinhToan.vuotKichThuocMang2(tapConGai[0][0] + toaDo[0], tapConGai[0][1] + toaDo[1]))
            {
                return false;
            }
            if(TinhToan.vuotKichThuocMang2(tapConGai[1][0] + toaDo[0], tapConGai[1][1] + toaDo[1]))
            {
                return false;
            }
            if(!TinhToan.vuotKichThuocMang(tapConGai[0][0]+toaDo[0], tapConGai[0][1]+toaDo[1]) && board[tapConGai[0][1] + toaDo[1],tapConGai[0][0]+toaDo[0]] == type_I)
            {
                if(!TinhToan.vuotKichThuocMang(tapConGai[1][0]+toaDo[0],tapConGai[1][1]+toaDo[1]) && board[tapConGai[1][1]+toaDo[1],tapConGai[1][0]+toaDo[0]] == type_I)
                {
                    return false;
                }
            }
            foreach(int[] i in tapConTrai)
            {
                int x = i[0] + toaDo[0];
                int y = i[1] + toaDo[1];
                if (TinhToan.vuotKichThuocMang(x, y) || board[y, x] != type)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool nuocDacBiet(int x, int y, char type, char[,] board)
        {
            int[][][][] set_1 = {new int[][][] {new int[][] {new int[] {0,-1},new int[] {0,-2},new int[] {0,-3}},
                                                new int[][] {new int[] {0,-4},new int[] {0,1}},
                                                new int[][] {new int[] {0,-5},new int[] {0,2}},
                                                },
                                new int[][][] { new int[][] {new int[] {-1,0},new int[] {-2,0},new int[] {-3,0}},
                                                new int[][] {new int[] {-4,0},new int[] {1,0}},
                                                new int[][] {new int[] {-5,0},new int[] {2,0}}
                                                },
                                new int[][][] { new int[][] {new int[] {0,1},new int[] {0,2},new int[] {0,3}},
                                                new int[][] {new int[] {0,4},new int[] {0,-1}},
                                                new int[][] {new int[] {0,5},new int[] {0,-2}}
                                                },
                                new int[][][] { new int[][] {new int[] {1,0},new int[] {2,0},new int[] {3,0}},
                                                new int[][] {new int[] {4,0},new int[] {-1,0}},
                                                new int[][] {new int[] {5,0},new int[] {-2,0}}
                                }};
            int[][][][] set_2 ={new int[][][] { new int[][] {new int[] {0,1},new int[] {0,-1},new int[] {0,-2}},
                                                new int[][] {new int[] {0,-3},new int[] {0,2}},
                                                new int[][] {new int[] {0,-4},new int[] {0,3}}
                                                },
                                new int[][][] { new int[][] {new int[] {1,0},new int[] {-1,0},new int[] {-2,0}},
                                                new int[][] {new int[] {-3,0},new int[] {2,0}},
                                                new int[][] {new int[] {-4,0},new int[] {3,0}}
                                },
                                new int[][][] { new int[][] {new int[] {0,-1},new int[] {0,1},new int[] {0,2}},
                                                new int[][] {new int[] {0,3},new int[] {0,-2}},
                                                new int[][] {new int[] {0,4},new int[] {0,-3}}
                                },
                                new int[][][] { new int[][] {new int[] {-1,0},new int[] {1,0},new int[] {2,0}},
                                                new int[][] {new int[] {3,0},new int[] {-2,0}},
                                                new int[][] {new int[] {4,0},new int[] {-3,0}}
                                }};
            int[][][][] set_3 = {new int[][][] {new int[][] {new int[] {1,-1},new int[] {2,-2},new int[] {3,-3}},
                                                new int[][] {new int[] {-1,1},new int[] {4,-4}},
                                                new int[][] {new int[] {5,-5},new int[] {-2,2}}
                                },
                                new int[][][] {new int[][] {new int[] {-1,1},new int[] {-2,2},new int[] {-3,3}},
                                               new int[][] {new int[] {1,-1},new int[] {-4,4}},
                                               new int[][] {new int[] {-5,5},new int[] {2,-2}}
                                },
                                new int[][][] { new int[][] {new int[] {1,1},new int[] {2,2},new int[] {3,3}},
                                                new int[][] {new int[] {-1,-1},new int[] {4,4}},
                                                new int[][] {new int[] {5,5},new int[] {-2,-2}}
                                },
                                new int[][][] { new int[][] {new int[] {-1,-1},new int[] {-2,-2},new int[] {-3,-3}},
                                                new int[][] {new int[] {1,1},new int[] {-4,-4}},
                                                new int[][] {new int[] {-5,-5},new int[] {2,2}}
                                }};
            int[][][][] set_4 = {new int[][][] {new int[][] {new int[] {-1,1},new int[] {1,-1},new int[] {2,-2}},
                                                new int[][] {new int[] {3,-3},new int[] {-2,2}},
                                                new int[][] {new int[] {-3,3},new int[] {4,-4}}
                                },
                                new int[][][] { new int[][] {new int[] {1,-1},new int[] {-1,1},new int[] {-2,2}},
                                                new int[][] {new int[] {-3,3},new int[] {2,-2}},
                                                new int[][] {new int[] {3,-3},new int[] {-4,4}}
                                },
                                new int[][][] { new int[][] {new int[] {-1,-1},new int[] {1,1},new int[] {2,2}},
                                                new int[][] {new int[] {3,3},new int[] {-2,-2}},
                                                new int[][] {new int[] {-3,-3},new int[] {4,4}}
                                },
                                new int[][][] {  new int[][] {new int[] {1,1},new int[] {-1,-1},new int[] {-2,-2}},
                                                 new int[][] {new int[] {-3,-3},new int[] {2,2}},
                                                 new int[][] {new int[] {3,3},new int[] {-4,-4}}
                                }};
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    int l = set_1[i][j].Length;
                    for(int t = 0; t < l; t++)
                    {
                        set_1[i][j][t][0] += x;
                        set_1[i][j][t][1] += y;
                        set_2[i][j][t][0] += x;
                        set_2[i][j][t][1] += y;
                        set_3[i][j][t][0] += x;
                        set_3[i][j][t][1] += y;
                        set_4[i][j][t][0] += x;
                        set_4[i][j][t][1] += y;
                    }
                }
            }
            List<int[]> cacNuocDoi = new List<int[]>();
            for(int ix = 0; ix < 4; ix++)
            {
                int[][][] i1 = set_1[ix];
                List<int[]> k1 = thoaMan(i1[0], i1[1], i1[2], type, board, false);
                int[][][] i2 = set_2[ix];
                List<int[]> k2 = thoaMan(i2[0], i2[1], i2[2], type, board, true);
                int[][][] i3 = set_3[ix];
                List<int[]> k3 = thoaMan(i3[0], i3[1], i3[2], type, board, false);
                int[][][] i4 = set_4[ix];
                List<int[]> k4 = thoaMan(i4[0], i4[1], i4[2], type, board, true);
                cacNuocDoi = TinhToan.themTapVao(k1, cacNuocDoi);
                cacNuocDoi = TinhToan.themTapVao(k2, cacNuocDoi);
                cacNuocDoi = TinhToan.themTapVao(k3, cacNuocDoi);
                cacNuocDoi = TinhToan.themTapVao(k4, cacNuocDoi);
                if(cacNuocDoi.Count >= 4)
                {
                    return true;
                }
            }
            return false;

        }

        public static List<int[]> chonNuocToiUu(List<int[]> coTheDi, char type, char[,] board)
        {
            List<int[]> tapNuocDuocChon = new List<int[]>();
            List<int[]> move_list = new List<int[]>();
            List<int> score_list = new List<int>();
            int tong_diem = 0;
            if(type == 'X')
            {
                foreach(int[] move in coTheDi)
                {
                    if(nuocDacBietNewVersion(move[0], move[1], 'O', board) > 0)
                    {
                        tapNuocDuocChon.Add(move);
                    }
                    if(nuocDacBietNewVersion(move[0], move[1], 'X', board) > 0)
                    {
                        if(!TinhToan.In_int(move, tapNuocDuocChon))
                        {
                            tapNuocDuocChon.Add(move);
                        }
                    }
                    if(tapNuocDuocChon.Count == 0)
                    {
                        TinhToan.setBoard(move, board, 'X');
                        int[] x = tongDoNguyHiem('X', board);
                        int[] o = tongDoNguyHiem('O', board);
                        TinhToan.removeBoard(move, board);
                        int d = TinhToan.diemCuaMaTran(x) - TinhToan.diemCuaMaTran(o);
                        move_list.Add(move);
                        score_list.Add(d);
                        tong_diem += d;
                    }
                }
                if (tapNuocDuocChon.Count == 0)
                {
                    double sumScore = tong_diem / coTheDi.Count;
                    for (int i = 0; i < move_list.Count; i++)
                     {
                     if (score_list[i] >= sumScore)
                      {
                        tapNuocDuocChon.Add(move_list[i]);
                       }
                     }
                }
            }
            else
            {
                foreach (int[] move in coTheDi)
                {
                    if (nuocDacBietNewVersion(move[0], move[1], 'X', board) > 0)
                    {
                        tapNuocDuocChon.Add(move);
                    }
                    if (nuocDacBietNewVersion(move[0], move[1], 'O', board) > 0)
                    {
                        if (!TinhToan.In_int(move, tapNuocDuocChon))
                        {
                            tapNuocDuocChon.Add(move);
                        }
                    }
                    if (tapNuocDuocChon.Count == 0)
                    {
                        TinhToan.setBoard(move, board, 'O');
                        int[] x = tongDoNguyHiem('X', board);
                        int[] o = tongDoNguyHiem('O', board);
                        TinhToan.removeBoard(move, board);
                        int d = TinhToan.diemCuaMaTran(x) - TinhToan.diemCuaMaTran(o);
                        move_list.Add(move);
                        score_list.Add(d);
                        tong_diem += d;
                    }
                }
                if (tapNuocDuocChon.Count == 0)
                {
                    double sumScore = tong_diem / coTheDi.Count;
                    for (int i = 0; i < move_list.Count; i++)
                     {
                     if (score_list[i] <= sumScore)
                     {
                        tapNuocDuocChon.Add(move_list[i]);
                     }
                    }
                }
            }
            return tapNuocDuocChon;
        }

        public static bool thoaManNewVersion(int[][] tapConTrai, int[][] tapConGai, int[][] tapConNuoi, char type, char[,] board, int flag)
        {
            char type_I = TinhToan.notType(type);
            foreach(int[] move in tapConNuoi)
            {
                if (TinhToan.vuotKichThuocMang(move[0], move[1]) || board[move[1],move[0]] == type_I)
                {
                    return false;
                }
            }
            if (flag == 1)
            {
                if (TinhToan.vuotKichThuocMang2(tapConGai[0][0], tapConGai[0][1]) || (!TinhToan.vuotKichThuocMang(tapConGai[0][0], tapConGai[0][1]) && board[tapConGai[0][1], tapConGai[0][0]] != '#'))
                {
                    return false;
                }
            }if(flag == 2)
            {
                int count = 0;
                foreach (int[] move in tapConGai)
                {
                    if (!TinhToan.vuotKichThuocMang2(move[0], move[1]) && TinhToan.vuotKichThuocMang(move[0], move[1]))
                    {
                        count++;
                    }
                    if(count > 1)
                    {
                        return false;
                    }
                    if (TinhToan.vuotKichThuocMang2(move[0], move[1]) || (!TinhToan.vuotKichThuocMang(move[0], move[1]) && board[move[1], move[0]] != '#'))
                    {
                        return false;
                    }
                }
            }
            else
            {
                foreach (int[] move in tapConGai)
                {
                    if (TinhToan.vuotKichThuocMang(move[0], move[1]) || board[move[1], move[0]] != '#')
                    {
                        return false;
                    }
                }
            }
            foreach(int[] move in tapConTrai)
            {
                if (TinhToan.vuotKichThuocMang(move[0], move[1]) || board[move[1], move[0]] != type)
                {
                    return false;
                }
            }
            return true;
        }
        public static int nuocDacBietNewVersion(int x, int y, char type, char[,] board)
        {
            int [][][][][] NuocHaiBa = new int[][][][][] {
new int[][][][] {new int[][][] {new int[][] {new int[]{-1,0},new int[]{-2,0},new int[]{-3,0}},new int[][] {new int[]{-4,0},new int[]{1,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{-2,0},new int[]{-3,0}},new int[][] {new int[]{2,0},new int[]{1,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{-2,0},new int[]{-3,0}},new int[][] {new int[]{-4,0},new int[]{-5,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{-2,0},new int[]{1,0}},new int[][] {new int[]{-3,0},new int[]{-4,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{-2,0},new int[]{1,0}},new int[][] {new int[]{-3,0},new int[]{2,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{-2,0},new int[]{1,0}},new int[][] {new int[]{3,0},new int[]{3,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{-2,0},new int[]{-3,0}},new int[][] {new int[]{-1,0},new int[]{2,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{-2,0},new int[]{-3,0}},new int[][] {new int[]{-1,0},new int[]{-4,0}}
},new int[][][] {new int[][] {new int[]{-2,0},new int[]{-3,0},new int[]{-4,0}},new int[][] {new int[]{-1,0},new int[]{-5,0}}
},new int[][][] {new int[][] {new int[]{-2,0},new int[]{-3,0},new int[]{-4,0}},new int[][] {new int[]{-1,0},new int[]{1,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{2,0},new int[]{-2,0}},new int[][] {new int[]{-1,0},new int[]{3,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{2,0},new int[]{-2,0}},new int[][] {new int[]{-1,0},new int[]{-3,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{-1,0},new int[]{-3,0}},new int[][] {new int[]{-2,0},new int[]{2,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{-1,0},new int[]{-3,0}},new int[][] {new int[]{-2,0},new int[]{4,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{-3,0},new int[]{-4,0}},new int[][] {new int[]{-2,0},new int[]{1,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{-3,0},new int[]{-4,0}},new int[][] {new int[]{-2,0},new int[]{5,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{-2,0}},new int[][] {new int[]{1,0},new int[]{2,0},new int[]{-3,0},new int[]{-4,0}},new int[][] {new int[]{-5,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{-2,0}},new int[][] {new int[]{1,0},new int[]{2,0},new int[]{-3,0},new int[]{-4,0}},new int[][] {new int[]{3,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{-3,0}},new int[][] {new int[]{-2,0},new int[]{-4,0},new int[]{1,0}},new int[][] {new int[]{-5,0},new int[]{2,0}}
},new int[][][] {new int[][] {new int[]{-2,0},new int[]{-3,0}},new int[][] {new int[]{-1,0},new int[]{-4,0},new int[]{1,0}},new int[][] {new int[]{2,0},new int[]{-5,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{1,0}},new int[][] {new int[]{-2,0},new int[]{2,0},new int[]{-3,0},new int[]{3,0}},new int[][] {new int[]{-4,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{1,0}},new int[][] {new int[]{-2,0},new int[]{2,0},new int[]{-3,0},new int[]{3,0}},new int[][] {new int[]{4,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{-2,0}},new int[][] {new int[]{-1,0},new int[]{2,0},new int[]{-3,0}},new int[][] {new int[]{3,0},new int[]{-4,0}}
}
},new int[][][][] {new int[][][] {new int[][] {new int[]{-1,-1},new int[]{-2,-2},new int[]{-3,-3}},new int[][] {new int[]{-4,-4},new int[]{1,1}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{-2,-2},new int[]{-3,-3}},new int[][] {new int[]{2,2},new int[]{1,1}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{-2,-2},new int[]{-3,-3}},new int[][] {new int[]{-4,-4},new int[]{-5,-5}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{-2,-2},new int[]{1,1}},new int[][] {new int[]{-3,-3},new int[]{-4,-4}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{-2,-2},new int[]{1,1}},new int[][] {new int[]{-3,-3},new int[]{2,2}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{-2,-2},new int[]{1,1}},new int[][] {new int[]{3,3},new int[]{3,3}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{-2,-2},new int[]{-3,-3}},new int[][] {new int[]{-1,-1},new int[]{2,2}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{-2,-2},new int[]{-3,-3}},new int[][] {new int[]{-1,-1},new int[]{-4,-4}}
},new int[][][] {new int[][] {new int[]{-2,-2},new int[]{-3,-3},new int[]{-4,-4}},new int[][] {new int[]{-1,-1},new int[]{-5,-5}}
},new int[][][] {new int[][] {new int[]{-2,-2},new int[]{-3,-3},new int[]{-4,-4}},new int[][] {new int[]{-1,-1},new int[]{1,1}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{2,2},new int[]{-2,-2}},new int[][] {new int[]{-1,-1},new int[]{3,3}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{2,2},new int[]{-2,-2}},new int[][] {new int[]{-1,-1},new int[]{-3,-3}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{-1,-1},new int[]{-3,-3}},new int[][] {new int[]{-2,-2},new int[]{2,2}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{-1,-1},new int[]{-3,-3}},new int[][] {new int[]{-2,-2},new int[]{4,4}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{-3,-3},new int[]{-4,-4}},new int[][] {new int[]{-2,-2},new int[]{1,1}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{-3,-3},new int[]{-4,-4}},new int[][] {new int[]{-2,-2},new int[]{5,5}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{-2,-2}},new int[][] {new int[]{1,1},new int[]{2,2},new int[]{-3,-3},new int[]{-4,-4}},new int[][] {new int[]{-5,-5}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{-2,-2}},new int[][] {new int[]{1,1},new int[]{2,2},new int[]{-3,-3},new int[]{-4,-4}},new int[][] {new int[]{3,3}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{-3,-3}},new int[][] {new int[]{-2,-2},new int[]{-4,-4},new int[]{1,1}},new int[][] {new int[]{-5,-5},new int[]{2,2}}
},new int[][][] {new int[][] {new int[]{-2,-2},new int[]{-3,-3}},new int[][] {new int[]{-1,-1},new int[]{-4,-4},new int[]{1,1}},new int[][] {new int[]{2,2},new int[]{-5,-5}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{1,1}},new int[][] {new int[]{-2,-2},new int[]{2,2},new int[]{-3,-3},new int[]{3,3}},new int[][] {new int[]{-4,-4}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{1,1}},new int[][] {new int[]{-2,-2},new int[]{2,2},new int[]{-3,-3},new int[]{3,3}},new int[][] {new int[]{4,4}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{-2,-2}},new int[][] {new int[]{-1,-1},new int[]{2,2},new int[]{-3,-3}},new int[][] {new int[]{3,3},new int[]{-4,-4}}
}
},new int[][][][] {new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,-2},new int[]{0,-3}},new int[][] {new int[]{0,-4},new int[]{0,1}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,-2},new int[]{0,-3}},new int[][] {new int[]{0,2},new int[]{0,1}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,-2},new int[]{0,-3}},new int[][] {new int[]{0,-4},new int[]{0,-5}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,-2},new int[]{0,1}},new int[][] {new int[]{0,-3},new int[]{0,-4}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,-2},new int[]{0,1}},new int[][] {new int[]{0,-3},new int[]{0,2}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,-2},new int[]{0,1}},new int[][] {new int[]{0,3},new int[]{0,3}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,-2},new int[]{0,-3}},new int[][] {new int[]{0,-1},new int[]{0,2}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,-2},new int[]{0,-3}},new int[][] {new int[]{0,-1},new int[]{0,-4}}
},new int[][][] {new int[][] {new int[]{0,-2},new int[]{0,-3},new int[]{0,-4}},new int[][] {new int[]{0,-1},new int[]{0,-5}}
},new int[][][] {new int[][] {new int[]{0,-2},new int[]{0,-3},new int[]{0,-4}},new int[][] {new int[]{0,-1},new int[]{0,1}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,2},new int[]{0,-2}},new int[][] {new int[]{0,-1},new int[]{0,3}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,2},new int[]{0,-2}},new int[][] {new int[]{0,-1},new int[]{0,-3}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,-1},new int[]{0,-3}},new int[][] {new int[]{0,-2},new int[]{0,2}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,-1},new int[]{0,-3}},new int[][] {new int[]{0,-2},new int[]{0,4}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,-3},new int[]{0,-4}},new int[][] {new int[]{0,-2},new int[]{0,1}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,-3},new int[]{0,-4}},new int[][] {new int[]{0,-2},new int[]{0,5}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,-2}},new int[][] {new int[]{0,1},new int[]{0,2},new int[]{0,-3},new int[]{0,-4}},new int[][] {new int[]{0,-5}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,-2}},new int[][] {new int[]{0,1},new int[]{0,2},new int[]{0,-3},new int[]{0,-4}},new int[][] {new int[]{0,3}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,-3}},new int[][] {new int[]{0,-2},new int[]{0,-4},new int[]{0,1}},new int[][] {new int[]{0,-5},new int[]{0,2}}
},new int[][][] {new int[][] {new int[]{0,-2},new int[]{0,-3}},new int[][] {new int[]{0,-1},new int[]{0,-4},new int[]{0,1}},new int[][] {new int[]{0,2},new int[]{0,-5}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,1}},new int[][] {new int[]{0,-2},new int[]{0,2},new int[]{0,-3},new int[]{0,3}},new int[][] {new int[]{0,-4}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,1}},new int[][] {new int[]{0,-2},new int[]{0,2},new int[]{0,-3},new int[]{0,3}},new int[][] {new int[]{0,4}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,-2}},new int[][] {new int[]{0,-1},new int[]{0,2},new int[]{0,-3}},new int[][] {new int[]{0,3},new int[]{0,-4}}
}
},new int[][][][] {new int[][][] {new int[][] {new int[]{1,-1},new int[]{2,-2},new int[]{3,-3}},new int[][] {new int[]{4,-4},new int[]{-1,1}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{2,-2},new int[]{3,-3}},new int[][] {new int[]{-2,2},new int[]{-1,1}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{2,-2},new int[]{3,-3}},new int[][] {new int[]{4,-4},new int[]{5,-5}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{2,-2},new int[]{-1,1}},new int[][] {new int[]{3,-3},new int[]{4,-4}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{2,-2},new int[]{-1,1}},new int[][] {new int[]{3,-3},new int[]{-2,2}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{2,-2},new int[]{-1,1}},new int[][] {new int[]{-3,3},new int[]{-3,3}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{2,-2},new int[]{3,-3}},new int[][] {new int[]{1,-1},new int[]{-2,2}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{2,-2},new int[]{3,-3}},new int[][] {new int[]{1,-1},new int[]{4,-4}}
},new int[][][] {new int[][] {new int[]{2,-2},new int[]{3,-3},new int[]{4,-4}},new int[][] {new int[]{1,-1},new int[]{5,-5}}
},new int[][][] {new int[][] {new int[]{2,-2},new int[]{3,-3},new int[]{4,-4}},new int[][] {new int[]{1,-1},new int[]{-1,1}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{-2,2},new int[]{2,-2}},new int[][] {new int[]{1,-1},new int[]{-3,3}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{-2,2},new int[]{2,-2}},new int[][] {new int[]{1,-1},new int[]{3,-3}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{1,-1},new int[]{3,-3}},new int[][] {new int[]{2,-2},new int[]{-2,2}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{1,-1},new int[]{3,-3}},new int[][] {new int[]{2,-2},new int[]{-4,4}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{3,-3},new int[]{4,-4}},new int[][] {new int[]{2,-2},new int[]{-1,1}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{3,-3},new int[]{4,-4}},new int[][] {new int[]{2,-2},new int[]{-5,5}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{2,-2}},new int[][] {new int[]{-1,1},new int[]{-2,2},new int[]{3,-3},new int[]{4,-4}},new int[][] {new int[]{5,-5}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{2,-2}},new int[][] {new int[]{-1,1},new int[]{-2,2},new int[]{3,-3},new int[]{4,-4}},new int[][] {new int[]{-3,3}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{3,-3}},new int[][] {new int[]{2,-2},new int[]{4,-4},new int[]{-1,1}},new int[][] {new int[]{5,-5},new int[]{-2,2}}
},new int[][][] {new int[][] {new int[]{2,-2},new int[]{3,-3}},new int[][] {new int[]{1,-1},new int[]{4,-4},new int[]{-1,1}},new int[][] {new int[]{-2,2},new int[]{5,-5}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{-1,1}},new int[][] {new int[]{2,-2},new int[]{-2,2},new int[]{3,-3},new int[]{-3,3}},new int[][] {new int[]{4,-4}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{-1,1}},new int[][] {new int[]{2,-2},new int[]{-2,2},new int[]{3,-3},new int[]{-3,3}},new int[][] {new int[]{-4,4}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{2,-2}},new int[][] {new int[]{1,-1},new int[]{-2,2},new int[]{3,-3}},new int[][] {new int[]{-3,3},new int[]{4,-4}}
}
},new int[][][][] {new int[][][] {new int[][] {new int[]{1,0},new int[]{2,0},new int[]{3,0}},new int[][] {new int[]{4,0},new int[]{-1,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{2,0},new int[]{3,0}},new int[][] {new int[]{-2,0},new int[]{-1,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{2,0},new int[]{3,0}},new int[][] {new int[]{4,0},new int[]{5,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{2,0},new int[]{-1,0}},new int[][] {new int[]{3,0},new int[]{4,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{2,0},new int[]{-1,0}},new int[][] {new int[]{3,0},new int[]{-2,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{2,0},new int[]{-1,0}},new int[][] {new int[]{-3,0},new int[]{-3,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{2,0},new int[]{3,0}},new int[][] {new int[]{1,0},new int[]{-2,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{2,0},new int[]{3,0}},new int[][] {new int[]{1,0},new int[]{4,0}}
},new int[][][] {new int[][] {new int[]{2,0},new int[]{3,0},new int[]{4,0}},new int[][] {new int[]{1,0},new int[]{5,0}}
},new int[][][] {new int[][] {new int[]{2,0},new int[]{3,0},new int[]{4,0}},new int[][] {new int[]{1,0},new int[]{-1,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{-2,0},new int[]{2,0}},new int[][] {new int[]{1,0},new int[]{-3,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{-2,0},new int[]{2,0}},new int[][] {new int[]{1,0},new int[]{3,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{1,0},new int[]{3,0}},new int[][] {new int[]{2,0},new int[]{-2,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{1,0},new int[]{3,0}},new int[][] {new int[]{2,0},new int[]{-4,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{3,0},new int[]{4,0}},new int[][] {new int[]{2,0},new int[]{-1,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{3,0},new int[]{4,0}},new int[][] {new int[]{2,0},new int[]{-5,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{2,0}},new int[][] {new int[]{-1,0},new int[]{-2,0},new int[]{3,0},new int[]{4,0}},new int[][] {new int[]{5,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{2,0}},new int[][] {new int[]{-1,0},new int[]{-2,0},new int[]{3,0},new int[]{4,0}},new int[][] {new int[]{-3,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{3,0}},new int[][] {new int[]{2,0},new int[]{4,0},new int[]{-1,0}},new int[][] {new int[]{5,0},new int[]{-2,0}}
},new int[][][] {new int[][] {new int[]{2,0},new int[]{3,0}},new int[][] {new int[]{1,0},new int[]{4,0},new int[]{-1,0}},new int[][] {new int[]{-2,0},new int[]{5,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{-1,0}},new int[][] {new int[]{2,0},new int[]{-2,0},new int[]{3,0},new int[]{-3,0}},new int[][] {new int[]{4,0}}
},new int[][][] {new int[][] {new int[]{1,0},new int[]{-1,0}},new int[][] {new int[]{2,0},new int[]{-2,0},new int[]{3,0},new int[]{-3,0}},new int[][] {new int[]{-4,0}}
},new int[][][] {new int[][] {new int[]{-1,0},new int[]{2,0}},new int[][] {new int[]{1,0},new int[]{-2,0},new int[]{3,0}},new int[][] {new int[]{-3,0},new int[]{4,0}}
}
},new int[][][][] {new int[][][] {new int[][] {new int[]{1,1},new int[]{2,2},new int[]{3,3}},new int[][] {new int[]{4,4},new int[]{-1,-1}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{2,2},new int[]{3,3}},new int[][] {new int[]{-2,-2},new int[]{-1,-1}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{2,2},new int[]{3,3}},new int[][] {new int[]{4,4},new int[]{5,5}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{2,2},new int[]{-1,-1}},new int[][] {new int[]{3,3},new int[]{4,4}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{2,2},new int[]{-1,-1}},new int[][] {new int[]{3,3},new int[]{-2,-2}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{2,2},new int[]{-1,-1}},new int[][] {new int[]{-3,-3},new int[]{-3,-3}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{2,2},new int[]{3,3}},new int[][] {new int[]{1,1},new int[]{-2,-2}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{2,2},new int[]{3,3}},new int[][] {new int[]{1,1},new int[]{4,4}}
},new int[][][] {new int[][] {new int[]{2,2},new int[]{3,3},new int[]{4,4}},new int[][] {new int[]{1,1},new int[]{5,5}}
},new int[][][] {new int[][] {new int[]{2,2},new int[]{3,3},new int[]{4,4}},new int[][] {new int[]{1,1},new int[]{-1,-1}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{-2,-2},new int[]{2,2}},new int[][] {new int[]{1,1},new int[]{-3,-3}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{-2,-2},new int[]{2,2}},new int[][] {new int[]{1,1},new int[]{3,3}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{1,1},new int[]{3,3}},new int[][] {new int[]{2,2},new int[]{-2,-2}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{1,1},new int[]{3,3}},new int[][] {new int[]{2,2},new int[]{-4,-4}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{3,3},new int[]{4,4}},new int[][] {new int[]{2,2},new int[]{-1,-1}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{3,3},new int[]{4,4}},new int[][] {new int[]{2,2},new int[]{-5,-5}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{2,2}},new int[][] {new int[]{-1,-1},new int[]{-2,-2},new int[]{3,3},new int[]{4,4}},new int[][] {new int[]{5,5}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{2,2}},new int[][] {new int[]{-1,-1},new int[]{-2,-2},new int[]{3,3},new int[]{4,4}},new int[][] {new int[]{-3,-3}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{3,3}},new int[][] {new int[]{2,2},new int[]{4,4},new int[]{-1,-1}},new int[][] {new int[]{5,5},new int[]{-2,-2}}
},new int[][][] {new int[][] {new int[]{2,2},new int[]{3,3}},new int[][] {new int[]{1,1},new int[]{4,4},new int[]{-1,-1}},new int[][] {new int[]{-2,-2},new int[]{5,5}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{-1,-1}},new int[][] {new int[]{2,2},new int[]{-2,-2},new int[]{3,3},new int[]{-3,-3}},new int[][] {new int[]{4,4}}
},new int[][][] {new int[][] {new int[]{1,1},new int[]{-1,-1}},new int[][] {new int[]{2,2},new int[]{-2,-2},new int[]{3,3},new int[]{-3,-3}},new int[][] {new int[]{-4,-4}}
},new int[][][] {new int[][] {new int[]{-1,-1},new int[]{2,2}},new int[][] {new int[]{1,1},new int[]{-2,-2},new int[]{3,3}},new int[][] {new int[]{-3,-3},new int[]{4,4}}
}
},new int[][][][] {new int[][][] {new int[][] {new int[]{0,1},new int[]{0,2},new int[]{0,3}},new int[][] {new int[]{0,4},new int[]{0,-1}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,2},new int[]{0,3}},new int[][] {new int[]{0,-2},new int[]{0,-1}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,2},new int[]{0,3}},new int[][] {new int[]{0,4},new int[]{0,5}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,2},new int[]{0,-1}},new int[][] {new int[]{0,3},new int[]{0,4}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,2},new int[]{0,-1}},new int[][] {new int[]{0,3},new int[]{0,-2}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,2},new int[]{0,-1}},new int[][] {new int[]{0,-3},new int[]{0,-3}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,2},new int[]{0,3}},new int[][] {new int[]{0,1},new int[]{0,-2}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,2},new int[]{0,3}},new int[][] {new int[]{0,1},new int[]{0,4}}
},new int[][][] {new int[][] {new int[]{0,2},new int[]{0,3},new int[]{0,4}},new int[][] {new int[]{0,1},new int[]{0,5}}
},new int[][][] {new int[][] {new int[]{0,2},new int[]{0,3},new int[]{0,4}},new int[][] {new int[]{0,1},new int[]{0,-1}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,-2},new int[]{0,2}},new int[][] {new int[]{0,1},new int[]{0,-3}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,-2},new int[]{0,2}},new int[][] {new int[]{0,1},new int[]{0,3}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,1},new int[]{0,3}},new int[][] {new int[]{0,2},new int[]{0,-2}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,1},new int[]{0,3}},new int[][] {new int[]{0,2},new int[]{0,-4}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,3},new int[]{0,4}},new int[][] {new int[]{0,2},new int[]{0,-1}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,3},new int[]{0,4}},new int[][] {new int[]{0,2},new int[]{0,-5}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,2}},new int[][] {new int[]{0,-1},new int[]{0,-2},new int[]{0,3},new int[]{0,4}},new int[][] {new int[]{0,5}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,2}},new int[][] {new int[]{0,-1},new int[]{0,-2},new int[]{0,3},new int[]{0,4}},new int[][] {new int[]{0,-3}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,3}},new int[][] {new int[]{0,2},new int[]{0,4},new int[]{0,-1}},new int[][] {new int[]{0,5},new int[]{0,-2}}
},new int[][][] {new int[][] {new int[]{0,2},new int[]{0,3}},new int[][] {new int[]{0,1},new int[]{0,4},new int[]{0,-1}},new int[][] {new int[]{0,-2},new int[]{0,5}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,-1}},new int[][] {new int[]{0,2},new int[]{0,-2},new int[]{0,3},new int[]{0,-3}},new int[][] {new int[]{0,4}}
},new int[][][] {new int[][] {new int[]{0,1},new int[]{0,-1}},new int[][] {new int[]{0,2},new int[]{0,-2},new int[]{0,3},new int[]{0,-3}},new int[][] {new int[]{0,-4}}
},new int[][][] {new int[][] {new int[]{0,-1},new int[]{0,2}},new int[][] {new int[]{0,1},new int[]{0,-2},new int[]{0,3}},new int[][] {new int[]{0,-3},new int[]{0,4}}
}
},new int[][][][] {new int[][][] {new int[][] {new int[]{-1,1},new int[]{-2,2},new int[]{-3,3}},new int[][] {new int[]{-4,4},new int[]{1,-1}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{-2,2},new int[]{-3,3}},new int[][] {new int[]{2,-2},new int[]{1,-1}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{-2,2},new int[]{-3,3}},new int[][] {new int[]{-4,4},new int[]{-5,5}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{-2,2},new int[]{1,-1}},new int[][] {new int[]{-3,3},new int[]{-4,4}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{-2,2},new int[]{1,-1}},new int[][] {new int[]{-3,3},new int[]{2,-2}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{-2,2},new int[]{1,-1}},new int[][] {new int[]{3,-3},new int[]{3,-3}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{-2,2},new int[]{-3,3}},new int[][] {new int[]{-1,1},new int[]{2,-2}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{-2,2},new int[]{-3,3}},new int[][] {new int[]{-1,1},new int[]{-4,4}}
},new int[][][] {new int[][] {new int[]{-2,2},new int[]{-3,3},new int[]{-4,4}},new int[][] {new int[]{-1,1},new int[]{-5,5}}
},new int[][][] {new int[][] {new int[]{-2,2},new int[]{-3,3},new int[]{-4,4}},new int[][] {new int[]{-1,1},new int[]{1,-1}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{2,-2},new int[]{-2,2}},new int[][] {new int[]{-1,1},new int[]{3,-3}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{2,-2},new int[]{-2,2}},new int[][] {new int[]{-1,1},new int[]{-3,3}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{-1,1},new int[]{-3,3}},new int[][] {new int[]{-2,2},new int[]{2,-2}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{-1,1},new int[]{-3,3}},new int[][] {new int[]{-2,2},new int[]{4,-4}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{-3,3},new int[]{-4,4}},new int[][] {new int[]{-2,2},new int[]{1,-1}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{-3,3},new int[]{-4,4}},new int[][] {new int[]{-2,2},new int[]{5,-5}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{-2,2}},new int[][] {new int[]{1,-1},new int[]{2,-2},new int[]{-3,3},new int[]{-4,4}},new int[][] {new int[]{-5,5}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{-2,2}},new int[][] {new int[]{1,-1},new int[]{2,-2},new int[]{-3,3},new int[]{-4,4}},new int[][] {new int[]{3,-3}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{-3,3}},new int[][] {new int[]{-2,2},new int[]{-4,4},new int[]{1,-1}},new int[][] {new int[]{-5,5},new int[]{2,-2}}
},new int[][][] {new int[][] {new int[]{-2,2},new int[]{-3,3}},new int[][] {new int[]{-1,1},new int[]{-4,4},new int[]{1,-1}},new int[][] {new int[]{2,-2},new int[]{-5,5}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{1,-1}},new int[][] {new int[]{-2,2},new int[]{2,-2},new int[]{-3,3},new int[]{3,-3}},new int[][] {new int[]{-4,4}}
},new int[][][] {new int[][] {new int[]{-1,1},new int[]{1,-1}},new int[][] {new int[]{-2,2},new int[]{2,-2},new int[]{-3,3},new int[]{3,-3}},new int[][] {new int[]{4,-4}}
},new int[][][] {new int[][] {new int[]{1,-1},new int[]{-2,2}},new int[][] {new int[]{-1,1},new int[]{2,-2},new int[]{-3,3}},new int[][] {new int[]{3,-3},new int[]{-4,4}}
}
}
};
            int [][][][] NuocBon = new int[][][][] {
new int[][][] {new int[][]{new int[] {-1,0},new int[] {-2,0},new int[] {-3,0},new int[] {-4,0}},new int[][]{new int[] {1,0}}
},new int[][][] {new int[][]{new int[] {-1,0},new int[] {-2,0},new int[] {-3,0},new int[] {-4,0}},new int[][]{new int[] {-5,0}}
},new int[][][] {new int[][]{new int[] {-1,0},new int[] {-2,0},new int[] {-3,0},new int[] {1,0}},new int[][]{new int[] {2,0}}
},new int[][][] {new int[][]{new int[] {-1,0},new int[] {-2,0},new int[] {-3,0},new int[] {1,0}},new int[][]{new int[] {-4,0}}
},new int[][][] {new int[][]{new int[] {-1,0},new int[] {-2,0},new int[] {1,0},new int[] {2,0}},new int[][]{new int[] {3,0}}
},new int[][][] {new int[][]{new int[] {-1,0},new int[] {-2,0},new int[] {1,0},new int[] {2,0}},new int[][]{new int[] {-3,0}}
},new int[][][] {new int[][]{new int[] {-1,-1},new int[] {-2,-2},new int[] {-3,-3},new int[] {-4,-4}},new int[][]{new int[] {1,1}}
},new int[][][] {new int[][]{new int[] {-1,-1},new int[] {-2,-2},new int[] {-3,-3},new int[] {-4,-4}},new int[][]{new int[] {-5,-5}}
},new int[][][] {new int[][]{new int[] {-1,-1},new int[] {-2,-2},new int[] {-3,-3},new int[] {1,1}},new int[][]{new int[] {2,2}}
},new int[][][] {new int[][]{new int[] {-1,-1},new int[] {-2,-2},new int[] {-3,-3},new int[] {1,1}},new int[][]{new int[] {-4,-4}}
},new int[][][] {new int[][]{new int[] {-1,-1},new int[] {-2,-2},new int[] {1,1},new int[] {2,2}},new int[][]{new int[] {3,3}}
},new int[][][] {new int[][]{new int[] {-1,-1},new int[] {-2,-2},new int[] {1,1},new int[] {2,2}},new int[][]{new int[] {-3,-3}}
},new int[][][] {new int[][]{new int[] {0,-1},new int[] {0,-2},new int[] {0,-3},new int[] {0,-4}},new int[][]{new int[] {0,1}}
},new int[][][] {new int[][]{new int[] {0,-1},new int[] {0,-2},new int[] {0,-3},new int[] {0,-4}},new int[][]{new int[] {0,-5}}
},new int[][][] {new int[][]{new int[] {0,-1},new int[] {0,-2},new int[] {0,-3},new int[] {0,1}},new int[][]{new int[] {0,2}}
},new int[][][] {new int[][]{new int[] {0,-1},new int[] {0,-2},new int[] {0,-3},new int[] {0,1}},new int[][]{new int[] {0,-4}}
},new int[][][] {new int[][]{new int[] {0,-1},new int[] {0,-2},new int[] {0,1},new int[] {0,2}},new int[][]{new int[] {0,3}}
},new int[][][] {new int[][]{new int[] {0,-1},new int[] {0,-2},new int[] {0,1},new int[] {0,2}},new int[][]{new int[] {0,-3}}
},new int[][][] {new int[][]{new int[] {1,-1},new int[] {2,-2},new int[] {3,-3},new int[] {4,-4}},new int[][]{new int[] {-1,1}}
},new int[][][] {new int[][]{new int[] {1,-1},new int[] {2,-2},new int[] {3,-3},new int[] {4,-4}},new int[][]{new int[] {5,-5}}
},new int[][][] {new int[][]{new int[] {1,-1},new int[] {2,-2},new int[] {3,-3},new int[] {-1,1}},new int[][]{new int[] {-2,2}}
},new int[][][] {new int[][]{new int[] {1,-1},new int[] {2,-2},new int[] {3,-3},new int[] {-1,1}},new int[][]{new int[] {4,-4}}
},new int[][][] {new int[][]{new int[] {1,-1},new int[] {2,-2},new int[] {-1,1},new int[] {-2,2}},new int[][]{new int[] {-3,3}}
},new int[][][] {new int[][]{new int[] {1,-1},new int[] {2,-2},new int[] {-1,1},new int[] {-2,2}},new int[][]{new int[] {3,-3}}
},new int[][][] {new int[][]{new int[] {1,0},new int[] {2,0},new int[] {3,0},new int[] {4,0}},new int[][]{new int[] {-1,0}}
},new int[][][] {new int[][]{new int[] {1,0},new int[] {2,0},new int[] {3,0},new int[] {4,0}},new int[][]{new int[] {5,0}}
},new int[][][] {new int[][]{new int[] {1,0},new int[] {2,0},new int[] {3,0},new int[] {-1,0}},new int[][]{new int[] {-2,0}}
},new int[][][] {new int[][]{new int[] {1,0},new int[] {2,0},new int[] {3,0},new int[] {-1,0}},new int[][]{new int[] {4,0}}
},new int[][][] {new int[][]{new int[] {1,0},new int[] {2,0},new int[] {-1,0},new int[] {-2,0}},new int[][]{new int[] {-3,0}}
},new int[][][] {new int[][]{new int[] {1,0},new int[] {2,0},new int[] {-1,0},new int[] {-2,0}},new int[][]{new int[] {3,0}}
},new int[][][] {new int[][]{new int[] {1,1},new int[] {2,2},new int[] {3,3},new int[] {4,4}},new int[][]{new int[] {-1,-1}}
},new int[][][] {new int[][]{new int[] {1,1},new int[] {2,2},new int[] {3,3},new int[] {4,4}},new int[][]{new int[] {5,5}}
},new int[][][] {new int[][]{new int[] {1,1},new int[] {2,2},new int[] {3,3},new int[] {-1,-1}},new int[][]{new int[] {-2,-2}}
},new int[][][] {new int[][]{new int[] {1,1},new int[] {2,2},new int[] {3,3},new int[] {-1,-1}},new int[][]{new int[] {4,4}}
},new int[][][] {new int[][]{new int[] {1,1},new int[] {2,2},new int[] {-1,-1},new int[] {-2,-2}},new int[][]{new int[] {-3,-3}}
},new int[][][] {new int[][]{new int[] {1,1},new int[] {2,2},new int[] {-1,-1},new int[] {-2,-2}},new int[][]{new int[] {3,3}}
},new int[][][] {new int[][]{new int[] {0,1},new int[] {0,2},new int[] {0,3},new int[] {0,4}},new int[][]{new int[] {0,-1}}
},new int[][][] {new int[][]{new int[] {0,1},new int[] {0,2},new int[] {0,3},new int[] {0,4}},new int[][]{new int[] {0,5}}
},new int[][][] {new int[][]{new int[] {0,1},new int[] {0,2},new int[] {0,3},new int[] {0,-1}},new int[][]{new int[] {0,-2}}
},new int[][][] {new int[][]{new int[] {0,1},new int[] {0,2},new int[] {0,3},new int[] {0,-1}},new int[][]{new int[] {0,4}}
},new int[][][] {new int[][]{new int[] {0,1},new int[] {0,2},new int[] {0,-1},new int[] {0,-2}},new int[][]{new int[] {0,-3}}
},new int[][][] {new int[][]{new int[] {0,1},new int[] {0,2},new int[] {0,-1},new int[] {0,-2}},new int[][]{new int[] {0,3}}
},new int[][][] {new int[][]{new int[] {-1,1},new int[] {-2,2},new int[] {-3,3},new int[] {-4,4}},new int[][]{new int[] {1,-1}}
},new int[][][] {new int[][]{new int[] {-1,1},new int[] {-2,2},new int[] {-3,3},new int[] {-4,4}},new int[][]{new int[] {-5,5}}
},new int[][][] {new int[][]{new int[] {-1,1},new int[] {-2,2},new int[] {-3,3},new int[] {1,-1}},new int[][]{new int[] {2,-2}}
},new int[][][] {new int[][]{new int[] {-1,1},new int[] {-2,2},new int[] {-3,3},new int[] {1,-1}},new int[][]{new int[] {-4,4}}
},new int[][][] {new int[][]{new int[] {-1,1},new int[] {-2,2},new int[] {1,-1},new int[] {2,-2}},new int[][]{new int[] {3,-3}}
},new int[][][] {new int[][]{new int[] {-1,1},new int[] {-2,2},new int[] {1,-1},new int[] {2,-2}},new int[][]{new int[] {-3,3}}
}
};
            int [][][][] NuocBaKhongChan = new int[][][][] {
new int[][][] {new int[][]{new int[] {-1,0},new int[] {-2,0},new int[] {-3,0}},new int[][]{new int[] {1,0},new int[] {2,0},new int[] {-4,0},new int[] {-5,0}}
},new int[][][] {new int[][]{new int[] {1,0},new int[] {-1,0},new int[] {-2,0}},new int[][]{new int[] {2,0},new int[] {3,0},new int[] {-3,0},new int[] {-4,0}}
},new int[][][] {new int[][]{new int[] {-1,-1},new int[] {-2,-2},new int[] {-3,-3}},new int[][]{new int[] {1,1},new int[] {2,2},new int[] {-4,-4},new int[] {-5,-5}}
},new int[][][] {new int[][]{new int[] {1,1},new int[] {-1,-1},new int[] {-2,-2}},new int[][]{new int[] {2,2},new int[] {3,3},new int[] {-3,-3},new int[] {-4,-4}}
},new int[][][] {new int[][]{new int[] {0,-1},new int[] {0,-2},new int[] {0,-3}},new int[][]{new int[] {0,1},new int[] {0,2},new int[] {0,-4},new int[] {0,-5}}
},new int[][][] {new int[][]{new int[] {0,1},new int[] {0,-1},new int[] {0,-2}},new int[][]{new int[] {0,2},new int[] {0,3},new int[] {0,-3},new int[] {0,-4}}
},new int[][][] {new int[][]{new int[] {1,-1},new int[] {2,-2},new int[] {3,-3}},new int[][]{new int[] {-1,1},new int[] {-2,2},new int[] {4,-4},new int[] {5,-5}}
},new int[][][] {new int[][]{new int[] {-1,1},new int[] {1,-1},new int[] {2,-2}},new int[][]{new int[] {-2,2},new int[] {-3,3},new int[] {3,-3},new int[] {4,-4}}
},new int[][][] {new int[][]{new int[] {1,0},new int[] {2,0},new int[] {3,0}},new int[][]{new int[] {-1,0},new int[] {-2,0},new int[] {4,0},new int[] {5,0}}
},new int[][][] {new int[][]{new int[] {-1,0},new int[] {1,0},new int[] {2,0}},new int[][]{new int[] {-2,0},new int[] {-3,0},new int[] {3,0},new int[] {4,0}}
},new int[][][] {new int[][]{new int[] {1,1},new int[] {2,2},new int[] {3,3}},new int[][]{new int[] {-1,-1},new int[] {-2,-2},new int[] {4,4},new int[] {5,5}}
},new int[][][] {new int[][]{new int[] {-1,-1},new int[] {1,1},new int[] {2,2}},new int[][]{new int[] {-2,-2},new int[] {-3,-3},new int[] {3,3},new int[] {4,4}}
},new int[][][] {new int[][]{new int[] {0,1},new int[] {0,2},new int[] {0,3}},new int[][]{new int[] {0,-1},new int[] {0,-2},new int[] {0,4},new int[] {0,5}}
},new int[][][] {new int[][]{new int[] {0,-1},new int[] {0,1},new int[] {0,2}},new int[][]{new int[] {0,-2},new int[] {0,-3},new int[] {0,3},new int[] {0,4}}
},new int[][][] {new int[][]{new int[] {-1,1},new int[] {-2,2},new int[] {-3,3}},new int[][]{new int[] {1,-1},new int[] {2,-2},new int[] {-4,4},new int[] {-5,5}}
},new int[][][] {new int[][]{new int[] {1,-1},new int[] {-1,1},new int[] {-2,2}},new int[][]{new int[] {2,-2},new int[] {3,-3},new int[] {-3,3},new int[] {-4,4}}
}
};

            for (int i = 0; i < NuocHaiBa.Length; i++)
            {
                for (int j = 0; j < NuocHaiBa[i].Length; j++)
                {
                    for(int k = 0; k < NuocHaiBa[i][j].Length; k++)
                    {
                        for(int t = 0; t < NuocHaiBa[i][j][k].Length; t++)
                        {
                            NuocHaiBa[i][j][k][t][0] += x;
                            NuocHaiBa[i][j][k][t][1] += y;
                        }
                    }
                }
            }
            for(int i = 0; i < NuocBaKhongChan.Length; i++)
            {
                for(int j= 0; j < NuocBaKhongChan[i].Length; j++)
                {
                    for(int k = 0; k < NuocBaKhongChan[i][j].Length; k++)
                    {
                        NuocBaKhongChan[i][j][k][0] += x;
                        NuocBaKhongChan[i][j][k][1] += y;
                    }
                }
            }
            for (int i = 0; i < NuocBon.Length; i++)
            {
                for (int j = 0; j < NuocBon[i].Length; j++)
                {
                    for (int k = 0; k < NuocBon[i][j].Length; k++)
                    {
                        NuocBon[i][j][k][0] += x;
                        NuocBon[i][j][k][1] += y;
                    }
                }
            }
            foreach(int[][][] i in NuocBon)
            {
                if (thoaManNewVersion(i[0], i[1], new int[][] { }, type, board, 1))
                {
                    return 1;
                }
            }
            foreach(int[][][] i in NuocBaKhongChan)
            {
                if (thoaManNewVersion(i[0], i[1], new int[][] { }, type, board, 2))
                {
                    return 3;
                }
            }
            List<int[]> cacNuocDoi = new List<int[]>();
            bool flag = false;
            foreach(int[][][][] Nuoc in NuocHaiBa)
            {
                foreach(int[][][] i in Nuoc)
                {
                    if (i.Length == 2)
                    {
                        if(thoaManNewVersion(i[0], i[1], new int[][] { }, type, board, 3))
                        {
                            cacNuocDoi = TinhToan.themTapVao2(i[0], cacNuocDoi);
                            flag = true;
                            break;
                        }
                    }
                    else
                    {
                        if(thoaManNewVersion(i[0], i[1], i[2], type, board, 4))
                        {
                            cacNuocDoi = TinhToan.themTapVao2(i[0], cacNuocDoi);
                            break;
                        }
                    }
                }
                if(flag && cacNuocDoi.Count >= 5)
                {
                    return 2;
                }
            }
            if (cacNuocDoi.Count >= 4)
            {
                return 4;
            }
            return 0;
        }
    }
}
