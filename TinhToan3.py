def add(maTran1, maTran2):
    return [maTran1[0] + maTran2[0], maTran1[1] + maTran2[1], maTran1[2] + maTran2[2], maTran1[3] + maTran2[3],
            maTran1[4] + maTran2[4], maTran1[5] + maTran2[5]]

def compare(set_1, set_2):
    if len(set_1) != len(set_2):
        return False
    for i in set_1:
        if i not in set_2:
            return False
    for i in set_2:
        if i not in set_1:
            return False
    return True


def laConRuot(set_con, set_me):
    for i in set_me:
        if compare(set_con, i):
            return True
    return False


def themTapVao(tapThem, tapCha):
    for i in tapThem:
        if i not in tapCha:
            tapCha.append(i)
    return tapCha


# print(sort_move([['a', 1], ['b', 5], ['c', 2], ['d', 0]]))
# print(laConRuot([1, 2, 3], [[2, 1, 3], [1, 1]]))

def quayDon(diem):
    return [-diem[1], diem[0]]


def dao(diem):
    return [-diem[0], diem[1]]


def daoBo(bo):
    temp_bo = []
    for i in bo:
        temp_bo.append(dao(i))
    return temp_bo


def quayBo(tap):
    temp_set = []
    for i in tap:
        temp_set.append(quayDon(i))
    return temp_set


def convert_string_to_list(string):
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
                list[t].append(int(temp)+7)
                temp = ""
            except:
                temp = ""
    return list


def reverse_string(string):
    string_temp = ''
    for i in string:
        string_temp = i + string_temp
    return string_temp

def vuotKichThuocMang(x, y):
    if x < 0 or x > 14 or y < 0 or y > 14:
        return True
    return False

def setBoard(move, board, type):
    board[move[1]][move[0]] = type

def removeBoard(move, board):
    board[move[1]][move[0]] = "#"

def notType(type):
    if type == 'X':
        return 'O'
    else:
        return 'X'

def diemCuaMaTran(maTran): #COMPLETE
    return maTran[0] * 99999 + maTran[1] * 9999 + maTran[2] * 9999 + maTran[3] * 999 + maTran[4] * 999 + maTran[5] * 99

def printBoard(board):
    for i in board:
        print(i)
    print('----------------------------------------------------------------------------')

def writeBoardToFile(board):
    f = open('data/board.txt', 'w')
    for x in range(15):
        for y in range(15):
            if board[y][x] == 'X':
                f.write('X:'+str(x)+":"+str(y)+':\n')
            elif board[y][x] == 'O':
                f.write('O:' + str(x) + ":" + str(y) + ':\n')
    f.close()

# print(reverse_string("ABCDE"))

# s =  "X[[-1, 0], [-2, -1], [-2, 2], [-1, 1], [0, 0]]"
# print(convert_string_to_list(s[1:]))
