from pygame import *
import sys
from random import *
from threading import Thread
from RunCSFile import *
from Sprites import *
from tinhDiemBanCo_3 import *

init()
mixer.init()
WIDTH = 900
HEIGHT = 700
FONT = font.Font('font/arial.ttf', 11)
BIG_FONT = font.Font('font/arial.ttf', 20)
icon = transform.scale(image.load('image/bkhn.jpg'), (20, 30))


class Screen:
    def __init__(self):

        self.screen = display.set_mode((WIDTH, HEIGHT))
        display.set_caption("<< N.D.NGUYEN  >> T.P.NAM  >> H.D.PHUC >>")
        display.set_icon(icon)
        self.clock = time.Clock()
        self.running = True

    def new(self):
        self.doDichX = 300
        self.doDichY = 300
        self.doDichX_temp = 0
        self.doDichY_temp = 0
        self.mouse_start_pos = [0, 0]
        self.click = False
        self.game = GameBoard()
        self.getType()
        if self.game.computerType == "X":
            self.thinking = True
        else:
            self.thinking = False
        self.game_end = False
        self.move_num = 0
        if self.game.computerType == "X":
            self.get_data('Xdata.txt')
        else:
            self.get_data("Odata.txt")
        self.level = self.level_read()

    def run(self):
        while self.running:
            self.event()
            self.update()
            self.draw()
            if self.thinking:
                if self.move_X == []:
                    self.think()
                else:
                    if self.game.computerType == "X":
                        self.think_X()
                    else:
                        self.think_O()
                self.thinking = False
            display.update()
            self.clock.tick(60)

    def event(self):
        for e in event.get():
            m = mouse.get_pos()
            if e.type == QUIT:
                quit()
                sys.exit(0)
            if e.type == KEYDOWN:
                if e.key == K_z and not self.thinking:
                    self.game.RETURN()
                    self.thinking = True
            if e.type == MOUSEBUTTONDOWN:
                if e.button == 3:
                    if not self.click:
                        self.mouse_start_pos = m
                        self.click = True

                if e.button == 1:
                    if 350 + self.doDichX + self.doDichX_temp < m[0] < 550 + self.doDichX + self.doDichX_temp and \
                                                            -200 + self.doDichY + self.doDichY_temp < m[
                                        1] < -150 + self.doDichY + self.doDichY_temp:
                        self.newGame()
                    if 350 + self.doDichX + self.doDichX_temp < m[0] < 550 + self.doDichX + self.doDichX_temp and \
                                                            -100 + self.doDichY + self.doDichY_temp < m[
                                        1] < -50 + self.doDichY + self.doDichY_temp:
                        self.doiQuan()
                    if 410 + self.doDichX + self.doDichX_temp < m[0] < 490 + self.doDichX + self.doDichX_temp and \
                                                            -30 + self.doDichY + self.doDichY_temp < m[
                                        1] < 20 + self.doDichY + self.doDichY_temp:
                        self.level_set(1)
                        self.level = 1
                    if 410 + self.doDichX + self.doDichX_temp < m[0] < 490 + self.doDichX + self.doDichX_temp and \
                                                            30 + self.doDichY + self.doDichY_temp < m[
                                        1] < 80 + self.doDichY + self.doDichY_temp:
                        self.level_set(2)
                        self.level = 2
                    if 410 + self.doDichX + self.doDichX_temp < m[0] < 490 + self.doDichX + self.doDichX_temp and \
                                                            90 + self.doDichY + self.doDichY_temp < m[
                                        1] < 140 + self.doDichY + self.doDichY_temp:
                        self.level_set(3)
                        self.level = 3
                    if 410 + self.doDichX + self.doDichX_temp < m[0] < 490 + self.doDichX + self.doDichX_temp and \
                                                            150 + self.doDichY + self.doDichY_temp < m[
                                        1] < 200 + self.doDichY + self.doDichY_temp:
                        self.level_set(4)
                        self.level = 4

            if e.type == MOUSEBUTTONUP:
                if e.button == 1 and not self.game_end:
                    new_move = [(m[0] - self.doDichX - self.doDichX_temp) // 40 + 7,
                                (m[1] - self.doDichY - self.doDichY_temp) // 40 + 7]
                    if not vuotKichThuocMang(new_move[0], new_move[1]) :
                        if self.game.playerType == 'X':
                            # if not self.thinking:
                            if new_move not in self.game.moveX and new_move not in self.game.moveO:
                                self.game.setMove(new_move, self.game.playerType)
                                if chacChanWin('X', self.game.board):
                                    self.game_end = True
                                else:
                                    self.thinking = True
                                temp = []
                                for i in self.move_X:
                                    if new_move != self.X_data[i][len(self.X_data[i]) - 1 - self.move_num]:
                                        temp.append(i)
                                for i in temp:
                                    self.move_O.remove(i)
                                    self.move_X.remove(i)
                        else:
                            if new_move not in self.game.moveO and new_move not in self.game.moveX:
                                self.game.setMove(new_move, self.game.playerType)
                                if chacChanWin('O', self.game.board):
                                    self.game_end = True
                                else:
                                    self.thinking = True
                                temp = []
                                for i in self.move_O:
                                    if new_move != self.O_data[i][len(self.O_data[i]) - 1 - self.move_num]:
                                        temp.append(i)
                                for i in temp:
                                    self.move_O.remove(i)
                                    self.move_X.remove(i)
                                self.move_num += 1
                if e.button == 3:
                    if self.click:
                        self.click = False
                        self.doDichX += self.doDichX_temp
                        self.doDichX_temp = 0
                        self.doDichY += self.doDichY_temp
                        self.doDichY_temp = 0

    def update(self):
        if self.click:
            m = mouse.get_pos()
            self.doDichX_temp = m[0] - self.mouse_start_pos[0]
            self.doDichY_temp = m[1] - self.mouse_start_pos[1]
        m = mouse.get_pos()
        if not self.game_end:
            if chacChanWin('X', self.game.board) or chacChanWin('O',self.game.board):
                self.game_end = True

    def draw(self):
        m = mouse.get_pos()
        mouse_pos = (((m[0] - self.doDichX - self.doDichX_temp) // 40 ) * 40 + self.doDichX + self.doDichX_temp,
                     ((m[1] - self.doDichY - self.doDichY_temp) // 40) * 40 + self.doDichY + self.doDichY_temp)
        self.screen.fill((114, 117, 104))
        gg = Surface((600, 600))
        gg.fill((50, 50, 50))
        self.screen.blit(gg, (-280+self.doDichX + self.doDichX_temp, -280+self.doDichY + self.doDichY_temp))
        gg1 = Surface((150, 50))
        gg1.fill((114, 117, 104))
        self.screen.blit(gg1, (350 + self.doDichX + self.doDichX_temp, -280 + self.doDichY + self.doDichY_temp))
        gg2 = Surface((200, 50))
        if 350 + self.doDichX + self.doDichX_temp < m[0] < 550 + self.doDichX + self.doDichX_temp and \
                                -200 + self.doDichY + self.doDichY_temp < m[1] < -150 + self.doDichY + self.doDichY_temp:
            gg2.fill((114, 117, 104))
        else:
            gg2.fill((231,53,255))
        gg22 = Surface((200, 50))
        if 350 + self.doDichX + self.doDichX_temp < m[0] < 550 + self.doDichX + self.doDichX_temp and \
                                                -100 + self.doDichY + self.doDichY_temp < m[
                            1] < -50 + self.doDichY + self.doDichY_temp:
            gg22.fill((114, 117, 104))
        else:
            gg22.fill((231, 53, 255))
        if 410 + self.doDichX + self.doDichX_temp < m[0] < 490 + self.doDichX + self.doDichX_temp and \
                                                -30 + self.doDichY + self.doDichY_temp < m[
                            1] < 20 + self.doDichY + self.doDichY_temp:
            draw.rect(self.screen, Color('yellow'), (410 + self.doDichX + self.doDichX_temp, -30 + self.doDichY + self.doDichY_temp, 80, 50), 4)
        if 410 + self.doDichX + self.doDichX_temp < m[0] < 490 + self.doDichX + self.doDichX_temp and \
                                                30 + self.doDichY + self.doDichY_temp < m[
                            1] < 80 + self.doDichY + self.doDichY_temp:
            draw.rect(self.screen, Color('yellow'),
                      (410 + self.doDichX + self.doDichX_temp, 30 + self.doDichY + self.doDichY_temp, 80, 50), 4)
        if 410 + self.doDichX + self.doDichX_temp < m[0] < 490 + self.doDichX + self.doDichX_temp and \
                                                90 + self.doDichY + self.doDichY_temp < m[
                            1] < 140 + self.doDichY + self.doDichY_temp:
            draw.rect(self.screen, Color('yellow'),
                      (410 + self.doDichX + self.doDichX_temp, 90 + self.doDichY + self.doDichY_temp, 80, 50), 4)
        if 410 + self.doDichX + self.doDichX_temp < m[0] < 490 + self.doDichX + self.doDichX_temp and \
                                                150 + self.doDichY + self.doDichY_temp < m[
                            1] < 200 + self.doDichY + self.doDichY_temp:
            draw.rect(self.screen, Color('yellow'),
                      (410 + self.doDichX + self.doDichX_temp, 150 + self.doDichY + self.doDichY_temp, 80, 50), 4)
        self.screen.blit(gg2, (350 + self.doDichX + self.doDichX_temp, -200 + self.doDichY + self.doDichY_temp))
        self.screen.blit(gg22, (350 + self.doDichX + self.doDichX_temp, -100 + self.doDichY + self.doDichY_temp))
        self.screen.blit(BIG_FONT.render("VÁN MỚI!", True, Color("white")),
                         (380 + self.doDichX + self.doDichX_temp, -180 + self.doDichY + self.doDichY_temp))
        self.screen.blit(BIG_FONT.render("ĐỔI QUÂN!", True, Color("white")),
                         (410 + self.doDichX + self.doDichX_temp, -90 + self.doDichY + self.doDichY_temp))
        self.screen.blit(BIG_FONT.render("Level 1", True, Color("white")),
                         (420 + self.doDichX + self.doDichX_temp, -20 + self.doDichY + self.doDichY_temp))
        self.screen.blit(BIG_FONT.render("Level 2", True, Color("white")),
                         (420 + self.doDichX + self.doDichX_temp, 40 + self.doDichY + self.doDichY_temp))
        self.screen.blit(BIG_FONT.render("Level 3", True, Color("white")),
                         (420 + self.doDichX + self.doDichX_temp, 100 + self.doDichY + self.doDichY_temp))
        self.screen.blit(BIG_FONT.render("Level 4", True, Color("white")),
                         (420 + self.doDichX + self.doDichX_temp, 160 + self.doDichY + self.doDichY_temp))
        gg3 = Surface((40, 40))
        gg3.fill((238,238,238))

        if -7 <= (m[0] - self.doDichX - self.doDichX_temp) // 40 <= 7 and -7 <= (m[1] - self.doDichY - self.doDichY_temp) // 40 <= 7:
            self.screen.blit(gg3, mouse_pos)

        self.drawGameBoard([-280+self.doDichX + self.doDichX_temp, -280+self.doDichY + self.doDichY_temp],
                           [320 + self.doDichX + self.doDichX_temp, 320 + self.doDichY + self.doDichY_temp])
        # for i in range(-100, 10):
        # for j in range(-100, 100):
        # if [i, j] not in self.game.cacNuocDaDiO and [i, j] not in self.game.cacNuocDaDiX:
        #    # text = FONT.render(str(i)+':'+str(j), True, Color('black'))
        # self.screen.blit(text, (i * 40 + self.doDichX + self.doDichX_temp + 5,
        # j * 40 + self.doDichY_temp + self.doDichY + 15))
        for ix in self.game.moveX:
            i = [ix[0]-7, ix[1]-7]
            if self.game.moveX.index(ix) == len(self.game.moveX) - 1:
                drawX(self.screen, (i[0] * 40 + self.doDichX + self.doDichX_temp,
                                                  i[1] * 40 + self.doDichY_temp + self.doDichY), True)
            else:
                drawX(self.screen, (i[0] * 40 + self.doDichX + self.doDichX_temp ,
                                                        i[1] * 40 + self.doDichY_temp + self.doDichY ), False)

        for ix in self.game.moveO:
            i = [ix[0]-7, ix[1]-7]
            if self.game.moveO.index(ix) == len(self.game.moveO) - 1:
                drawO(self.screen, (i[0] * 40 + self.doDichX + self.doDichX_temp,
                                                  i[1] * 40 + self.doDichY_temp + self.doDichY), True)
            else:
                drawO(self.screen, (i[0] * 40 + self.doDichX + self.doDichX_temp,
                                                  i[1] * 40 + self.doDichY_temp + self.doDichY), False)

        if self.game_end:
            text = BIG_FONT.render("Trò chơi kết thúc!", True, Color("orange"))
            self.screen.blit(text, (370 + self.doDichX + self.doDichX_temp, -270 + self.doDichY + self.doDichY_temp))
        else:
            text = BIG_FONT.render("Ván đấu đang diễn ra!", True, Color("orange"))
            self.screen.blit(text, (370 + self.doDichX + self.doDichX_temp, -270 + self.doDichY + self.doDichY_temp))
        self.screen.blit(BIG_FONT.render("Level:"+str(self.level), True, Color("white")),
                         (370 + self.doDichX + self.doDichX_temp, -250 + self.doDichY + self.doDichY_temp))
    def drawGameBoard(self, x, y):
        for i in range(x[0], y[0], 40):
            draw.line(self.screen, (236, 255, 164), (i, x[1]), (i, y[1]), 2)
        for i in range(x[1], y[1], 40):
            draw.line(self.screen, (236, 255, 164), (x[0], i), (y[0], i), 2)
        draw.line(self.screen, (194, 143, 11),
                  (-7 * 40 + self.doDichX + self.doDichX_temp,
                   -7 * 40 + self.doDichY_temp + self.doDichY), (8 * 40 + self.doDichX + self.doDichX_temp,
                                                                 -7 * 40 + self.doDichY_temp + self.doDichY), 2)
        draw.line(self.screen, (194, 143, 11),
                  (-7 * 40 + self.doDichX + self.doDichX_temp,
                   -7 * 40 + self.doDichY_temp + self.doDichY), (-7 * 40 + self.doDichX + self.doDichX_temp,
                                                                 8 * 40 + self.doDichY_temp + self.doDichY), 2)
        draw.line(self.screen, (194, 143, 11),
                  (-7 * 40 + self.doDichX + self.doDichX_temp,
                   8 * 40 + self.doDichY_temp + self.doDichY), (8 * 40 + self.doDichX + self.doDichX_temp,
                                                                8 * 40 + self.doDichY_temp + self.doDichY), 2)
        draw.line(self.screen, (194, 143, 11),
                  (8 * 40 + self.doDichX + self.doDichX_temp,
                   -7 * 40 + self.doDichY_temp + self.doDichY), (8 * 40 + self.doDichX + self.doDichX_temp,
                                                                 8 * 40 + self.doDichY_temp + self.doDichY), 2)

    def think(self):
        #temp = self.game.findBestMove()
        temp = runCSFile(self.game.board, self.game.computerType)
        self.game.setMove(temp, self.game.computerType)

    def think_X(self):
        #r = randrange(len(self.move_X))
        #move = self.X_data[self.move_X[r]][len(self.X_data[self.move_X[r]]) - 1 - self.move_num]
        move = self.get_nextMove()
        temp = []
        for i in self.move_X:
            if move != self.X_data[i][len(self.X_data[i]) - 1 - self.move_num]:
                temp.append(i)
        for i in temp:
            self.move_O.remove(i)
            self.move_X.remove(i)
        self.game.setMove([move[0] , move[1]], 'X')

    def think_O(self):
        move = self.get_nextMove()
        temp = []
        for i in self.move_O:
            if move != self.O_data[i][len(self.O_data[i]) - 1 - self.move_num]:
                temp.append(i)
        for i in temp:
            self.move_O.remove(i)
            self.move_X.remove(i)
        self.game.setMove([move[0], move[1]], 'O')
        self.move_num += 1


    def get_data(self, name):
        self.X_data = []
        self.O_data = []
        with open('data/' + name, 'r') as lines:
            for line in lines:
                if line[0] == "X":
                    self.X_data.append(convert_string_to_list(line[1:]))
                elif line[0] == "O":
                    self.O_data.append(convert_string_to_list(line[1:]))
        self.move_X = []
        self.move_O = []
        temp = len(self.X_data)
        for i in range(temp):
            self.move_X.append(i)
            self.move_O.append(i)

    def get_nextMove(self):
        nextMove = []
        if self.game.computerType == 'X':
            for ix in self.move_X:
                i = self.X_data[ix]
                next = i[len(i) - 1 - self.move_num]
                if next not in nextMove:
                    nextMove.append(next)
        else:
            for ix in self.move_O:
                i = self.O_data[ix]
                next = i[len(i) - 1 - self.move_num]
                if next not in nextMove:
                    nextMove.append(next)
        r = randrange(len(nextMove))
        return nextMove[r]

    def save_data(self):
        f = open('data/data.txt', 'a')
        f.write('X' + str(self.game.moveX) + '\n')
        f.write('O' + str(self.game.moveO) + '\n')
        f.close()
    def newGame(self):
        self.click = False
        self.game = GameBoard()
        self.getType()
        if self.game.computerType == "X":
            self.thinking = True
        else:
            self.thinking = False
        self.game_end = False
        self.move_num = 0
        if self.game.computerType == "X":
            self.get_data('Xdata.txt')
        else:
            self.get_data("Odata.txt")
    def doiQuan(self):
        if self.game.playerType == 'X':
            f = open('data/Type1.txt', 'w')
            f.write('X\n')
            f.write('O')
            self.game.playerType = 'O'
            self.game.computerType = 'X'
            f.close()
        elif self.game.playerType == 'O':
            f = open('data/Type1.txt', 'w')
            f.write('O\n')
            f.write('X')
            self.game.playerType = 'X'
            self.game.computerType = 'O'
            f.close()
        self.newGame()
    def getType(self):
        with open('data/Type1.txt', 'r') as lines:
            temp = []
            for line in lines:
                temp.append(line.replace('\n','').replace(' ',''))
        self.game.computerType = temp[0]
        self.game.playerType = temp[1]

    def level_read(self):
        with open('data/level.txt', 'r') as lines:
            for line in lines:
                level = int(line)+1
                break
        return level

    def level_set(self, number):
        f = open('data/level.txt', 'w')
        f.write(str(number-1))
        f.close()
        self.newGame()
