from pygame import *
import sys
from MiniMax_3 import *

init()
mixer.init()
WIDTH = 700
HEIGHT = 700
FONT = font.Font('font/VnArabia.TTF', 11)
BIG_FONT = font.Font('font/VnArabia.TTF', 20)


class Screen:
    def __init__(self):
        self.screen = display.set_mode((WIDTH, HEIGHT))
        display.set_caption("Ko Kazo troll nguoi!")
        self.clock = time.Clock()
        self.running = True

    def new(self):
        self.doDichX = 300
        self.doDichY = 300
        self.doDichX_temp = 0
        self.doDichY_temp = 0
        self.mouse_start_pos = [0, 0]
        self.click = False
        self.game_end = False
        self.X_data = []
        self.O_data = []
        self.computerType = 'X'
        self.playerType = 'O'
        self.cacNuocDaDiO = []
        self.cacNuocDaDiX = []
        self.get_data()

    def run(self):
        while self.running:
            self.event()
            self.update()
            self.draw()
            display.update()
            self.clock.tick(60)

    def event(self):
        for e in event.get():
            if e.type == QUIT:
                quit()
                sys.exit(0)
            if e.type == MOUSEBUTTONDOWN:
                m = mouse.get_pos()
                if e.button == 1:
                    if not self.click:
                        self.mouse_start_pos = m
                        self.click = True
                if e.button == 3 and not self.game_end:
                    new_X = [(m[0] - self.doDichX - self.doDichX_temp) // 40,
                             (m[1] - self.doDichY - self.doDichY_temp) // 40]
                    if self.computerType == 'X':
                        if new_X not in self.cacNuocDaDiX and new_X not in self.cacNuocDaDiO:
                            self.cacNuocDaDiX.insert(0, new_X)
                            self.computerType = "O"
                    else:
                        if new_X not in self.cacNuocDaDiO and new_X not in self.cacNuocDaDiX:
                            self.cacNuocDaDiO.insert(0, new_X)
                            self.computerType = "X"

            if e.type == MOUSEBUTTONUP:
                if e.button == 1:
                    if self.click:
                        self.click = False
                        self.doDichX += self.doDichX_temp
                        self.doDichX_temp = 0
                        self.doDichY += self.doDichY_temp
                        self.doDichY_temp = 0
            if e.type == KEYDOWN:
                keys = key.get_pressed()
                if keys[K_z] and (keys[K_LCTRL] or keys[K_RCTRL]):
                    if self.computerType == "X":
                        self.computerType = "O"
                        del self.cacNuocDaDiO[0]
                    else:
                        self.computerType = "X"
                        del self.cacNuocDaDiX[0]
                if keys[K_s] and (keys[K_LCTRL] or keys[K_RCTRL]):
                    if self.cacNuocDaDiX in self.X_data and self.cacNuocDaDiO in self.O_data:
                        pass
                    else:
                        self.save_data()
                        self.get_data()

    def update(self):
        if self.click:
            m = mouse.get_pos()
            self.doDichX_temp = m[0] - self.mouse_start_pos[0]
            self.doDichY_temp = m[1] - self.mouse_start_pos[1]
        m = mouse.get_pos()
        display.set_caption(str([(m[0] - self.doDichX - self.doDichX_temp) // 40,
                                 (m[1] - self.doDichY - self.doDichY_temp) // 40]))
        if self.cacNuocDaDiX in self.X_data and self.cacNuocDaDiO in self.O_data:
            display.set_caption("Da co!")

    def draw(self):
        self.screen.fill(Color('WHITE'))
        self.drawGameBoard([-5000 + self.doDichX + self.doDichX_temp, -5000 + self.doDichY + self.doDichY_temp],
                           [5000 + self.doDichX + self.doDichX_temp, 5000 + self.doDichY + self.doDichY_temp])
        # for i in range(-100, 10):
        # for j in range(-100, 100):
        # if [i, j] not in self.cacNuocDaDiO and [i, j] not in self.cacNuocDaDiX:
        #    # text = FONT.render(str(i)+':'+str(j), True, Color('black'))
        # self.screen.blit(text, (i * 40 + self.doDichX + self.doDichX_temp + 5,
        # j * 40 + self.doDichY_temp + self.doDichY + 15))
        for i in self.cacNuocDaDiX:
            text = FONT.render(str(i), True, Color('black'))
            self.screen.blit(text, (i[0] * 40 + self.doDichX + self.doDichX_temp + 5,
                                    i[1] * 40 + self.doDichY_temp + self.doDichY + 15))
            draw.circle(self.screen, Color('red'), (i[0] * 40 + self.doDichX + self.doDichX_temp + 20,
                                                    i[1] * 40 + self.doDichY_temp + self.doDichY + 20), 20, 4)

        for i in self.cacNuocDaDiO:
            text = FONT.render(str(i), True, Color('black'))
            self.screen.blit(text, (i[0] * 40 + self.doDichX + self.doDichX_temp + 5,
                                    i[1] * 40 + self.doDichY_temp + self.doDichY + 15))
            draw.circle(self.screen, Color('blue'), (i[0] * 40 + self.doDichX + self.doDichX_temp + 20,
                                                     i[1] * 40 + self.doDichY_temp + self.doDichY + 20), 20, 4)

        if self.game_end:
            text = BIG_FONT.render("END CMNR!", True, Color("orange"))
            self.screen.blit(text, (WIDTH // 2, HEIGHT // 2))

    def drawGameBoard(self, x, y):
        for i in range(x[0], y[0], 40):
            draw.line(self.screen, Color('black'), (i, x[1]), (i, y[1]), 2)
        for i in range(x[1], y[1], 40):
            draw.line(self.screen, Color('black'), (x[0], i), (y[0], i), 2)
        draw.line(self.screen, Color("orange"),
                  (-7 * 40 + self.doDichX + self.doDichX_temp,
                   -7 * 40 + self.doDichY_temp + self.doDichY), (8 * 40 + self.doDichX + self.doDichX_temp,
                                                                 -7 * 40 + self.doDichY_temp + self.doDichY), 2)
        draw.line(self.screen, Color("orange"),
                  (-7 * 40 + self.doDichX + self.doDichX_temp,
                   -7 * 40 + self.doDichY_temp + self.doDichY), (-7 * 40 + self.doDichX + self.doDichX_temp,
                                                                 8 * 40 + self.doDichY_temp + self.doDichY), 2)
        draw.line(self.screen, Color("orange"),
                  (-7 * 40 + self.doDichX + self.doDichX_temp,
                   8 * 40 + self.doDichY_temp + self.doDichY), (8 * 40 + self.doDichX + self.doDichX_temp,
                                                                8 * 40 + self.doDichY_temp + self.doDichY), 2)
        draw.line(self.screen, Color("orange"),
                  (8 * 40 + self.doDichX + self.doDichX_temp,
                   -7 * 40 + self.doDichY_temp + self.doDichY), (8 * 40 + self.doDichX + self.doDichX_temp,
                                                                 8 * 40 + self.doDichY_temp + self.doDichY), 2)

    def save_data(self):
        f = open('data/Xdata.txt', 'a')
        set_1 = self.cacNuocDaDiX
        set_2 = self.cacNuocDaDiO
        set_3 = daoBo(self.cacNuocDaDiX)
        set_4 = daoBo(self.cacNuocDaDiO)
        for i in range(4):
            f.write('X' + str(set_1) + '\n')
            f.write('O' + str(set_2) + '\n')
            set_1 = quayBo(set_1)
            set_2 = quayBo(set_2)
            f.write('X' + str(set_3) + '\n')
            f.write('O' + str(set_4) + '\n')
            set_3 = quayBo(set_3)
            set_4 = quayBo(set_4)
        f.close()
        # image.save(self.screen, 'image/' + str(set_1) + str(set_2) + '.png')



    def get_data(self):
        self.X_data = []
        self.O_data = []
        with open('data/Xdata.txt', 'r') as lines:
            for line in lines:
                if line[0] == "X":
                    self.X_data.append(convert_string_to_list1(line[1:]))
                elif line[0] == "O":
                    self.O_data.append(convert_string_to_list1(line[1:]))
        #self.cacNuocDaDiX = [[0, -5], [1, -6], [1, -4], [-2, -3], [1, -3], [-1, -3], [-1, -4], [0, -3], [1, 3], [1, -2], [-2, -1], [-1, -2], [0, 0]]
        #self.cacNuocDaDiO = [[1, -5], [2, -5], [-3, -3], [2, -3], [-1, -1], [-2, -5], [-3, 0], [0, -2], [1, 2], [1, 0], [1, -1], [1, 1]]


def convert_string_to_list1(string):
    list = []
    t = -2
    temp = ''
    for i in string:
        if i != " ":
            if i == '[':
                if t >= -1:
                    list.append([])
                t += 1
        if i not in [" ", "[", "]", ","]:
            temp += i
        else:

            try:
                list[t].append(int(temp))
                temp = ""
            except:
                temp = ""
    return list


g = Screen()
g.new()
g.run()
