from pygame import *
X = image.load('image/X.png')
last_X = image.load('image/last_X.png')
O = image.load('image/O.png')
last_O = image.load('image/last_O.png')
class GameBoard:
    def __init__(self):
        self.board = [['#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'],
                      ['#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'],
                      ['#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'],
                      ['#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'],
                      ['#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'],
                      ['#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'],
                      ['#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'],
                      ['#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'],
                      ['#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'],
                      ['#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'],
                      ['#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'],
                      ['#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'],
                      ['#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'],
                      ['#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'],
                      ['#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#']]
        self.moveX = []
        self.moveO = []
        self.turn = 'X'
        self.computerType = 'X'
        self.playerType = 'O'

    def setMove(self, move, type):
        self.board[move[1]][ move[0]] = type
        if type == 'X':
            self.moveX.append(move)
            self.turn = 'O'
        elif type == 'O':
            self.moveO.append(move)
            self.turn = 'X'

    def RETURN(self):
        if self.turn == 'X':
            self.board[self.moveO[len(self.moveO) - 1][1]][self.moveO[len(self.moveO) - 1][0]] = "#"
            del self.moveO[len(self.moveO) - 1]
            self.turn = 'O'
        elif self.turn == 'O':
            self.board[self.moveO[len(self.moveO) - 1][1]][self.moveO[len(self.moveO) - 1][0]] = "#"
            del self.moveX[len(self.moveX) - 1]
            self.turn = 'X'

def drawX(screen, pos, flag):
    if flag:
        screen.blit(last_X, (pos[0], pos[1]))
        #draw.line(screen, Color('white'), (pos[0] + 2, pos[1] + 2), (pos[0] + 38, pos[1] + 38), 8)
        #draw.line(screen, Color('white'), (pos[0] + 38, pos[1] + 2), (pos[0] + 2, pos[1] + 38), 8)
    else:
        screen.blit(X, (pos[0], pos[1]))
    #draw.line(screen, color, (pos[0]+2, pos[1]+2), (pos[0]+38, pos[1] + 38), 5)
    #draw.line(screen, color, (pos[0]+38, pos[1]+2), (pos[0]+2, pos[1]+38), 5)

def drawO(screen, pos, flag):
    if flag:
        screen.blit(last_O, (pos[0], pos[1]))
        #draw.circle(screen, Color('white'),pos, 19, 6)
        #draw.circle(screen, Color('white'), pos, 15, 6)
    else:
        screen.blit(O, (pos[0], pos[1]))
    #draw.circle(screen, color, pos, 17, 4)
def In(list1, list2):
    if list1 == []:
        return True
    for i in list1:
        if i not in list2[len(list2)-len(list1):]:
            return False
    return True



