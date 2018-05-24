
def level_read():
    with open('data/level.txt','r') as lines:
        for line in lines:
            level = int(line)
            break
    return level

def level_set(number):
    f = open('data/level.txt', 'w')
    f.write(str(number))
    f.close()

level_set(111)
print(level_read())
