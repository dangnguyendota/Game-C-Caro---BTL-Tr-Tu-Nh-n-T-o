import os
def writeBoardToFile(board, type):
    f = open('data/board.txt', 'w')
    if type == 'X':
        for x in range(15):
            for y in range(15):
                if board[y][x] == 'X':
                    f.write('X:'+str(x)+":"+str(y)+':\n')
                elif board[y][x] == 'O':
                    f.write('O:' + str(x) + ":" + str(y) + ':\n')
    else:
        for x in range(15):
            for y in range(15):
                if board[y][x] == 'O':
                    f.write('X:'+str(x)+":"+str(y)+':\n')
                elif board[y][x] == 'X':
                    f.write('O:' + str(x) + ":" + str(y) + ':\n')
    f.close()

def runCSFile(board, type):
    writeBoardToFile(board, type)
    os.system("TriTueNhanTao.exe")
    move = readFileResult()
    return move

def readFileResult():
    move = []
    with open('data/nextMove.txt', 'r') as lines:
        for line in lines:
            try:
                temp = int(line)
                move.append(temp)
            except:
                pass
    return move

def setMove(move, board, type):
    board[move[1]][move[0]] = type
