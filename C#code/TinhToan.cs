using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TriTueNhanTao
{
    class TinhToan
    {
        public static int[] add(int[] maTran1, int[] maTran2)
        {
            int[] result = { maTran1[0]+maTran2[0], maTran1[1]+maTran2[1], maTran1[2] + maTran2[2]
                    , maTran1[3]+maTran2[3], maTran1[4]+maTran2[4], maTran1[5]+maTran2[5]};
            return result;
        }

        public static List<int[]> themTapVao(List<int[]> tapThem, List<int[]> tapCha)
        {
            foreach(int[] i in tapThem)
            {
                if(!In_int(i, tapCha))
                {
                    tapCha.Add(i);
                }
            }
            return tapCha;
        }
        public static List<int[]> themTapVao2(int[][] tapThem, List<int[]> tapCha)
        {
            foreach (int[] i in tapThem)
            {
                if (!In_int(i, tapCha))
                {
                    tapCha.Add(i);
                }
            }
            return tapCha;
        }
        public static bool In_int(int[] x, List<int[]> y)
        {
            foreach(int[] i in y)
            {
                if(x[0] == i[0] && x[1] == i[1] && x.Length == i.Length)
                {
                    return true;
                }
            }
            return false;
        } 

        public static bool Contains_12(int[] i, int[][] j)
        {
            foreach(int[] t in j)
            {
                if (t[0] == i[0] && t[1] == i[1] && t[2] == i[2] && t[3] == i[3] && t[4] == i[4] && t[5] == i[5])
                {
                    return true;
                }
            }
            return false;
        }
        public static bool chech_move_exist(int[] move, List<int[]> move_list)
        {
            foreach(int[] i in move_list)
            {
                if(i[0] == move[0] && i[1] == move[1] && i.Length == move.Length)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool vuotKichThuocMang(int x, int y)
        {
            if(x < 0 || x > 14 || y < 0 || y > 14)
            {
                return true;
            }
            return false;
        }
        public static bool vuotKichThuocMang2(int x, int y)
        {
            if (x < -1 || x > 15 || y < -1 || y > 15)
            {
                return true;
            }
            return false;
        }

        public static void setBoard(int[] move, char[,] board, char type)
        {
            board[move[1],move[0]] = type;
        }

        public static void removeBoard(int[] move, char[,] board)
        {
            board[move[1],move[0]] = '#';
        }

        public static char notType(char type)
        {
            if(type == 'X')
            {
                return 'O';
            }
            else
            {
                return 'X';
            }
        }

        public static int diemCuaMaTran(int[] maTran)=> maTran[0] * 99999 + maTran[1] * 9999 + maTran[2] * 9999 + maTran[3] * 999 + maTran[4] * 999 + maTran[5] * 99;

        public static void printBoard(char[,] board)
        {
            for(int i = 0; i< 15; i++)
            {
                Console.Write("[");
                for(int j =0; j< 15; j++)
                {
                    Console.Write(board[i,j].ToString()+ ',');
                }
                Console.Write("]");
                Console.Write('\n');
            }
            Console.WriteLine("-----------------------------------------------------------");
        }

        public static List<int[]> cong2List(List<int[]> list1, List<int[]> list2)
        {
            List<int[]> result = new List<int[]>();
            foreach(int[] i in list1)
            {
                result.Add(i);
            }
            foreach(int[] i in list2)
            {
                result.Add(i);
            }
            return result;
        }

        public static void printList(int[][][][] set)
        {
            foreach(int[][][] i in set)
            {
                foreach(int[][] j in i)
                {
                    Console.Write("[");
                    foreach(int[] k in j)
                    {
                        Console.Write("[");
                        foreach(int t in k)
                        {
                            Console.Write(t.ToString() + ",");
                        }
                        Console.Write("],");
                    }
                    Console.Write("],");
                }
                Console.Write("\n");
            }
            Console.WriteLine("--------------");
        }

        public static void printMoveList(List<int[]> move_list)
        {
            Console.Write("|");
            foreach(int[]i in move_list)
            {
                Console.Write("[" + i[0].ToString() + "," + i[1].ToString() + "]");
            }
            Console.Write("|\n");
        }

        public static char[,] readFile(string path)
        {
            char[,] board = {
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'}
                            };
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        int x = 0;
                        int y = 0;
                        char type = '#';
                        int count = 0;
                        string temp = "";
                        foreach (char i in line)
                        {
                            if (i == ':')
                            {
                                if (count == 0)
                                {
                                    type = temp[0];
                                    temp = "";
                                }
                                else if (count == 1)
                                {
                                    x = int.Parse(temp);
                                    temp = "";
                                }
                                else if (count == 2)
                                {
                                    y = int.Parse(temp);
                                    temp = "";
                                }
                                count++;
                                
                            }
                            else {
                                temp += i;
                            }
                            
                        }
                        board[y, x] = type;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            return board;
        }

        public static char[] getType(string path)
        {
            char[] result = new char[2] { '#', '#' };
            string[] lines = File.ReadAllLines(path);
            result[0] = lines[0][0];
            result[1] = lines[1][0];
            return result;
        }

        public static int getLevel(string path)
        {
            int result = 0;
            string[] lines = File.ReadAllLines(path);
            try
            {
                result = int.Parse(lines[0]);
            }
            catch
            {

            }
            
            return result;
        }

        public static void writeNextMove(int[] move, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {

                sw.WriteLine(move[0].ToString());
                sw.WriteLine(move[1].ToString());
            }
        }
    }
}
