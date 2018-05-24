from tinhDiemBanCo_3 import *
from timNuocTrongBanCo_3 import *
from threading import *


class game():
    def __init__(self):
        self.cacNuocDaDiX = []
        self.cacNuocDaDiO = []
        self.computerType = 'X'
        if self.computerType == "X":
            self.playerType = 'O'
        else:
            self.playerType = "X"
        self.game_board = BOARD

        # if self.computerType == "X":
        # self.cacNuocDaDiX.append([0, 0])

    def setMove(self, move, type):
        x = move[0]
        y = move[1]
        if type == "X":
            if [x, y] not in self.cacNuocDaDiO:
                self.cacNuocDaDiX.insert(0, move)
                self.game_board[y][x] = 'X'
        elif type == "O":
            if [x, y] not in self.cacNuocDaDiX:
                self.cacNuocDaDiO.insert(0, move)
                self.game_board[y][x] = 'O'
        writeBoardToFile(self.game_board)

    def findBestMove(self):
        coTheDi = tapCacNuocCoTheDi(self.game_board, False)
        coTheDi_2 = tapCacNuocCoTheDi(self.game_board, True)
        maxScore = -999999
        minScore = 999999
        bestMove = []
        depht =2
        tapNuocDuocChon = chonNuocToiUu(coTheDi, self.computerType, self.game_board)
        flag = True  # khong co nuoc thua luon
        if self.computerType == 'X':
            nuoc_hay_X = []
            nuoc_hay_O = []
            for move in coTheDi_2:
                ###################3
                setBoard(move, self.game_board, 'X')
                if chacChanWin("X", self.game_board):
                    removeBoard(move, self.game_board)
                    return move
                removeBoard(move, self.game_board)
                #####################
            for move in coTheDi_2:
                #####################
                setBoard(move, self.game_board, 'O')
                if chacChanWin("O", self.game_board):
                    if flag:
                        tapNuocDuocChon = []
                        flag = False
                    tapNuocDuocChon.append(move)
                removeBoard(move, self.game_board)
                ######################
            if flag:
                for move in coTheDi_2:
                    if nuocDacBiet(move[0], move[1], "X", self.game_board):
                        nuoc_hay_X.append(move)
                    if nuocDacBiet(move[0], move[1], "O", self.game_board):
                        nuoc_hay_O.append(move)
                if nuoc_hay_X != [] or nuoc_hay_O != []:
                    tapNuocDuocChon = nuoc_hay_O + nuoc_hay_X
            for move in tapNuocDuocChon:
                #######################
                setBoard(move, self.game_board, 'X')  # dat nuoc move cho ban co
                temp = self.Score(maxScore, 999999, depht, 'O', self.game_board)
                removeBoard(move, self.game_board)  # tra ve trang thai ban dau
                ######################
                print(move,':',temp)
                if temp == 999999:
                    return move
                if temp >= maxScore:
                    bestMove = move
                    maxScore = temp
        else:
            nuoc_hay_X = []
            nuoc_hay_O = []
            for move in coTheDi_2:
                #######################
                setBoard(move, self.game_board, 'O')
                if chacChanWin("O", self.game_board):
                    removeBoard(move, self.game_board)
                    return move
                removeBoard(move, self.game_board)
                ########################
            for move in coTheDi_2:
                ########################
                setBoard(move, self.game_board, 'X')
                if chacChanWin("X", self.game_board):
                    if flag:
                        tapNuocDuocChon = []
                        flag = False
                    tapNuocDuocChon.append(move)
                removeBoard(move, self.game_board)
                #######################
            if flag:
                for move in coTheDi_2:
                    if nuocDacBiet(move[0], move[1], "X", self.game_board):
                        nuoc_hay_X.append(move)
                    if nuocDacBiet(move[0], move[1], "O", self.game_board):
                        nuoc_hay_O.append(move)
                if nuoc_hay_X != [] or nuoc_hay_O != []:
                    tapNuocDuocChon = nuoc_hay_O + nuoc_hay_X
            for move in tapNuocDuocChon:
                #########################
                setBoard(move, self.game_board, 'O')  # dat nuoc move cho ban co
                temp = self.Score(-999999, minScore, depht, 'X', self.game_board)
                removeBoard(move, self.game_board)  # tra ve trang thai ban dau
                ########################
                # print(move, ':', temp)
                if temp == -999999:
                    return move
                if temp <= minScore:
                    bestMove = move
                    minScore = temp
        return bestMove

    def Score(self, min, max, depht, type, board):
        maxScore = max
        minScore = min
        coTheDi = tapCacNuocCoTheDi(board, False)
        # Xét win đặc biệt
        if type == 'X':
            if gameWin('X', board):
                return 999999
        else:
            if gameWin('O', board):
                return -999999
                ## Tính điểm bàn cờ
        if depht == 0:
            x = tongDoNguyHiem('X', board)
            o = tongDoNguyHiem('O', board)
            score = diemCuaMaTran(x) - diemCuaMaTran(o)
            return score
        # MiniMax Cắt tỉa Alpha-Beta
        # chọn các nước có khả năng cao là nước tốt
        tapNuocDuocChon = chonNuocToiUu(coTheDi, type, board)
        # tapNuocDuocChon = tapCacNuocCoTheDi(daDi, False)
        for move in tapNuocDuocChon:
            if type == 'X':
                setBoard(move, board, 'X')  # dat nuoc move cho ban co
                temp = self.Score(minScore, maxScore, depht - 1, 'O', board)
                removeBoard(move, board)  # tra ve trang thai ban dau
                if temp >= maxScore:
                    return 999999
                if minScore < temp < maxScore:
                    minScore = temp
                if minScore == maxScore:
                    return minScore
            else:
                #####################
                setBoard(move, board, 'O')  # dat nuoc move cho ban co
                temp = self.Score(minScore, maxScore, depht - 1, 'X', board)
                removeBoard(move, board)  # tra ve trang thai ban dau
                ###################3
                if temp <= minScore:
                    return -999999
                if minScore < temp < maxScore:
                    maxScore = temp
                if maxScore == minScore:
                    return maxScore
        if type == 'X':
            return minScore
        else:
            return maxScore



