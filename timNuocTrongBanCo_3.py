from Data_game_board import *


def tapCacNuoc(x, y):
    result = []
    t = 0
    for i in cacNuocXungQuanhMotNuoc:
        if 0 <= i[0][0] + x <= 14 and 0 <= i[0][1] + y <= 14 and \
                                0 <= i[5][0] + x <= 14 and 0 <= i[5][1] + y <= 14:
            result.append([])
            for j in i:
                result[t].append([j[0] + x, j[1] + y])
            t += 1
    return result


def tapCacNuocCoTheDi(board, xetWin=False):
    result = []
    for x in range(15):
        for y in range(15):
            if board[y][x] != "#":
                temp = cacNuocXungQuanh(x, y, xetWin)
                for move in temp:
                    if move not in result and board[move[1]][move[0]] == "#":
                        result.append(move)
    return result


def cacNuocXungQuanh(x, y, flag=False):
    result = []
    for i in tap:
        if 0 <= x + i[0] <= 14 and 0 <= y + i[1] <= 14:
            result.append([x + i[0], y + i[1]])
    if flag:
        for i in tap_1:
            if 0 <= x + i[0] <= 14 and 0 <= y + i[1] <= 14:
                result.append([x + i[0], y + i[1]])
    return result


