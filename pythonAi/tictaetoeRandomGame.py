# Tic-Tae-Toe 모델로 Random game

from math import isfinite
import random

class TicTaeToe:
    def __init__(self):
        self.board = [0] *9
        self.aiTurn = random.randrange(1, 3)

    # 마이 턴~~!!
    def Put(self, turn, pos):
        self.board[pos] = turn
    
    # 경기가 끝 났는지 검사
    def isFinished(self):
        #가로
        for i in range(3):
            x = self.board[i*3]
            if x == 0: continue
            isFinish = True
            for j in range(3):
                if x != self.board[i * 3 + j]:
                    isFinish = False
                    break
            if isFinish: return x
        #세로
        for i in range(3):
            x = self.board[i]
            if x == 0: continue
            isFinish = True
            for j in range(3):
                if x != self.board[i + j * 3]:
                    isFinish = False
                    break
            if isFinish: return x
        #대각선
        isFinish = True
        x = self.board[2]
        if x !=0:
            for i in range(3):
                if x != self.board[i * 4]:
                    isFinish = False
                    break
            if isFinish: return x
        isFinish = True
        for i in range(9):
            if self.board[i] == 0:
                isFinish = False
                break
        if isFinish: return 0 

        #대각선? 
        isFinish = True
        x = self.board[2]
        if x != 0:
            for i in range(3):
                if x != self.board[i * 2 + 2]:
                    isFinish = False
                    break
            if isFinish: return x
        
        # all board is full
        isFinish = True
        for i in range(9):
            if self.board[i] == 0:
                isFinish = False
                break
        if isFinish: return 0
        return -1

    def Print(self,x):
        tt = (" ", "O", "X")
        print(f"+--------- {x} ------------+")
        print("+---+---+---+")
        print(f"| {tt[self.board[0]]} | {tt[self.board[1]]} | {tt[self.board[2]]} |")
        print("+---+---+---+")
        print(f"| {tt[self.board[3]]} | {tt[self.board[4]]} | {tt[self.board[5]]} |")
        print("+---+---+---+")
        print(f"| {tt[self.board[6]]} | {tt[self.board[7]]} | {tt[self.board[8]]} |")
        print("+---+---+---+")


# 게임 진행 스타트 
while True:
    game = TicTaeToe()
    turn = 1
    while game.isFinished() == -1:
        game.Print(turn)
        if turn == game.aiTurn:
            cand = []
            for i in range(9):
                if game.board[i] == 0: cand.append(i)
            p = random.choice(cand)
            game.Put(turn, p)
        else:
            p = int(input("pos : "))
            game.Put(turn, p)
        turn ^= 1
    yn = input("Do you want more game : ")
    if yn != 'y' and yn != 'Y': break