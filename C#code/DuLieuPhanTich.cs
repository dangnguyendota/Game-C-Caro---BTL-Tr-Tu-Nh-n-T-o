using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TriTueNhanTao
{
    public class DuLieuPhanTich
    {
        public const int MAXSCORE = 999999;
        public const int MINSCORE = -999999;
        public static int[][][] cacNuocXungQuanhMotNuoc =   {new int[][] {new int[] { 0 , -5 },new int[] { 0 , -4 },new int[] { 0 , -3 },new int[] { 0 , -2 },new int[] { 0 , -1 },new int[] { 0 , 0 }},
                                                            new int[][] {new int[] { 0 , -4 },new int[] { 0 , -3 },new int[] { 0 , -2 },new int[] { 0 , -1 },new int[] { 0 , 0 },new int[] { 0 , 1 }},
                                                            new int[][] {new int[] { 0 , -3 },new int[] { 0 , -2 },new int[] { 0 , -1 },new int[] { 0 , 0 },new int[] { 0 , 1 },new int[] { 0 , 2 }},
                                                            new int[][] {new int[] { 0 , -2 },new int[] { 0 , -1 },new int[] { 0 , 0 },new int[] { 0 , 1 },new int[] { 0 , 2 },new int[] { 0 , 3 }},
                                                            new int[][] {new int[] { 0 , -1 },new int[] { 0 , 0 },new int[] { 0 , 1 },new int[] { 0 , 2 },new int[] { 0 , 3 },new int[] { 0 , 4 }},
                                                            new int[][] {new int[] { 0 , 0 },new int[] { 0 , 1 },new int[] { 0 , 2 },new int[] { 0 , 3 },new int[] { 0 , 4 },new int[] { 0 , 5 }},
                                                            new int[][] {new int[] { -5 , 0 },new int[] { -4 , 0 },new int[] { -3 , 0 },new int[] { -2 , 0 },new int[] { -1 , 0 },new int[] { 0 , 0 }},
                                                            new int[][] {new int[] { -4 , 0 },new int[] { -3 , 0 },new int[] { -2 , 0 },new int[] { -1 , 0 },new int[] { 0 , 0 },new int[] { 1 , 0 }},
                                                            new int[][] {new int[] { -3 , 0 },new int[] { -2 , 0 },new int[] { -1 , 0 },new int[] { 0 , 0 },new int[] { 1 , 0 },new int[] { 2 , 0 }},
                                                            new int[][] {new int[] { -2 , 0 },new int[] { -1 , 0 },new int[] { 0 , 0 },new int[] { 1 , 0 },new int[] { 2 , 0 },new int[] { 3 , 0 }},
                                                            new int[][] {new int[] { -1 , 0 },new int[] { 0 , 0 },new int[] { 1 , 0 },new int[] { 2 , 0 },new int[] { 3 , 0 },new int[] { 4 , 0 }},
                                                            new int[][] {new int[] { 0 , 0 },new int[] { 1 , 0 },new int[] { 2 , 0 },new int[] { 3 , 0 },new int[] { 4 , 0 },new int[] { 5 , 0 }},
                                                            new int[][] {new int[] { -5 , -5 },new int[] { -4 , -4 },new int[] { -3 , -3 },new int[] { -2 , -2 },new int[] { -1 , -1 },new int[] { 0 , 0 }},
                                                            new int[][] {new int[] { -4 , -4 },new int[] { -3 , -3 },new int[] { -2 , -2 },new int[] { -1 , -1 },new int[] { 0 , 0 },new int[] { 1 , 1 }},
                                                            new int[][] {new int[] { -3 , -3 },new int[] { -2 , -2 },new int[] { -1 , -1 },new int[] { 0 , 0 },new int[] { 1 , 1 },new int[] { 2 , 2 }},
                                                            new int[][] {new int[] { -2 , -2 },new int[] { -1 , -1 },new int[] { 0 , 0 },new int[] { 1 , 1 },new int[] { 2 , 2 },new int[] { 3 , 3 }},
                                                            new int[][] {new int[] { -1 , -1 },new int[] { 0 , 0 },new int[] { 1 , 1 },new int[] { 2 , 2 },new int[] { 3 , 3 },new int[] { 4 , 4 }},
                                                            new int[][] {new int[] { 0 , 0 },new int[] { 1 , 1 },new int[] { 2 , 2 },new int[] { 3 , 3 },new int[] { 4 , 4 },new int[] { 5 , 5 }},
                                                            new int[][] {new int[] { -5 , 5 },new int[] { -4 , 4 },new int[] { -3 , 3 },new int[] { -2 , 2 },new int[] { -1 , 1 },new int[] { 0 , 0 }},
                                                            new int[][] {new int[] { -4 , 4 },new int[] { -3 , 3 },new int[] { -2 , 2 },new int[] { -1 , 1 },new int[] { 0 , 0 },new int[] { 1 , -1 }},
                                                            new int[][] {new int[] { -3 , 3 },new int[] { -2 , 2 },new int[] { -1 , 1 },new int[] { 0 , 0 },new int[] { 1 , -1 },new int[] { 2 , -2 }},
                                                            new int[][] {new int[] { -2 , 2 },new int[] { -1 , 1 },new int[] { 0 , 0 },new int[] { 1 , -1 },new int[] { 2 , -2 },new int[] { 3 , -3 }},
                                                            new int[][] {new int[] { -1 , 1 },new int[] { 0 , 0 },new int[] { 1 , -1 },new int[] { 2 , -2 },new int[] { 3 , -3 },new int[] { 4 , -4 }},
                                                            new int[][] {new int[] { 0 , 0 },new int[] { 1 , -1 },new int[] { 2 , -2 },new int[] { 3 , -3 },new int[] { 4 , -4 },new int[] { 5 , -5 }},
                                                                    };
        public static int[][] strongDoubleMoves = {new int[] {0 ,1 ,1 ,0 ,0 ,0 ,},
new int[] {0 ,0 ,1 ,1 ,0 ,0 },
new int[] {0 ,0 ,0 ,1 ,1 ,0 },
new int[] {0 ,1 ,0 ,1 ,0 ,0 },
new int[] {0 ,0 ,1 ,0 ,1 ,0 },
};
        public static int[][] weakDoubleMoves = {new int[] {1 ,1 ,0 ,0 ,0 ,0 },
new int[] {0 ,0 ,0 ,1 ,0 ,1 },
new int[] {1 ,0 ,1 ,0 ,0 ,0 },
new int[] {0 ,0 ,0 ,0 ,1 ,1 },
};
        public static int[][] strongTripleMoves = {new int[] {0 ,1 ,1 ,1 ,0 ,0 },
new int[] {0 ,0 ,1 ,1 ,1 ,0 },
new int[] {0 ,1 ,1 ,0 ,1 ,0 },
new int[] {0 ,1 ,0 ,1 ,1 ,0 },
};
        public static int[][] weakTripleMoves ={new int[] {1 ,0 ,1 ,0 ,1 ,0 },
new int[] {0 ,1 ,0 ,1 ,0 ,1 },
new int[] {0 ,0 ,0 ,1 ,1 ,1 },
new int[] {0 ,0 ,1 ,0 ,1 ,1 },
new int[] {0 ,0 ,1 ,1 ,0 ,1 },
new int[] {1 ,1 ,1 ,0 ,0 ,0 },
new int[] {1 ,0 ,1 ,1 ,0 ,0 },
new int[] {1 ,1 ,0 ,1 ,0 ,0 },
};
        public static int[][] strongQuadraMoves = {new int[] {1 ,1 ,1 ,1 ,0 ,0 },
new int[] {0 ,1 ,1 ,1 ,1 ,0 },
new int[] {0 ,0 ,1 ,1 ,1 ,1 },
};
        public static int[][] weakQuadraMoves = {new int[] {1 ,1 ,0 ,1 ,1 ,0 },
new int[] {0 ,1 ,1 ,0 ,1 ,1 },
new int[] {1 ,0 ,1 ,0 ,1 ,1 },
new int[] {1 ,0 ,1 ,1 ,0 ,1 },
new int[] {1 ,1 ,0 ,0 ,1 ,1 },
new int[] {1 ,1 ,0 ,1 ,0 ,1 },
new int[] {1 ,1 ,1 ,0 ,0 ,1},
new int[] {1 ,0 ,0 ,1 ,1 ,1 },
new int[] {1 ,0 ,1 ,1 ,1 ,0 },
new int[] {0 ,1 ,0 ,1 ,1 ,1 },
new int[] {1 ,1 ,1 ,0 ,1 ,0 },
new int[] {0 ,1 ,1 ,1 ,0 ,1 },
};
        public static int[][] strongestMoveEver = { new int[] { 1, 1, 1, 1, 1, 0 }, new int[] { 0, 1, 1, 1, 1, 1 } };
        public static char[,] BOARD = new char[15,15] {{'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
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
                                                    };
        public static int[][] tap = { new int[] { -1, -1 }, new int[] { 1, 1 }, new int[] { -1, 1 }, new int[] { 1, -1 }, new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
        public static int[][] tap_1 = { new int[] { 2, 0 }, new int[] { -2, 0 }, new int[] { 0, 2 }, new int[] { 0, -2 }, new int[] { 2, 2 }, new int[] { -2, -2 }, new int[] { 2, -2 }, new int[] { -2, 2 } };
        public static int[][][] set_1_XetWin = { new int[][] { new int[] { 0, 0 }, new int[] { 1, 0 }, new int[] { 2, 0 }, new int[] { 3, 0 }, new int[] { 4, 0 } }, new int[][] { new int[] { -1, 0 }, new int[] { 5, 0 } } };
        public static int[][][] set_2_XetWin = { new int[][] { new int[] { 0, 0 }, new int[] { 1, -1 }, new int[] { 2, -2 }, new int[] { 3, -3 }, new int[] { 4, -4 } }, new int[][] { new int[] { -1, 1 }, new int[] { 5, -5 } } };
        public static int[][][] set_3_XetWin = { new int[][] { new int[] { 0, 0 }, new int[] { 0, -1 }, new int[] { 0, -2 }, new int[] { 0, -3 }, new int[] { 0, -4 } }, new int[][] { new int[] { 0, 1 }, new int[] { 0, -5 } } };
        public static int[][][] set_4_XetWin = { new int[][] { new int[] { 0, 0 }, new int[] { 1, 1 }, new int[] { 2, 2 }, new int[] { 3, 3 }, new int[] { 4, 4 } }, new int[][] { new int[] { -1, -1 }, new int[] { 5, 5 } } };
    }
}
