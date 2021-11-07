# (v(s))' = (1-alpha)V(s) + alpha(R + V(s'))

# 격자 세계를 읽고 프로세스 할 클래스
class GridWorld:
    # 생성자
    def __init__(self, fname):
        # 비어있는 맵으로 초기화
        self.map = []
        # fname으로 파일을 열어 처리
        with open(fname) as f:
            self.n, self.m = map(int, f.readline().split())
            self.start = tuple(map(int, f.readline().split()))
            self.end = tuple(map(int, f.readline().split()))
            for _ in range(self.n):
                self.map.append(
                    [0 if k == '0' else 1 for k in f.readline()
                     if k == '0' or k == '1'])
    def init(self):
        # 에이전트의 위치를 시작지점으로 옮긴다.
        self.agent = self.start
        return self.agent

    # 에이전트를 움직인다.
    def move(self, dir):
        # dir 값에 따라 움직여야할 에이전트 위치를 계산한다.
        dxy = [ (0, 1), (1, 0), (0, -1), (-1, 0)]
        nr, nc = self.agent[0] + dxy[dir][0], self.agent[1] + dxy[dir][1]
        # 움직일 수 있는 위치인지 파악한다.
        if nr < 0 or nr >= self.n or nc < 0 or nc >= self.m or self.map[nr][nc] != 0:
            nr, nc = self.agent
        self.agent = (nr, nc)
        return self.agent
    
    # dir로 움직일경우 에이전트의 다음 상태
    def nextState(self, dir):
        # dir 값에 따라 움직여야할 에이전트 위치를 계산한다.
        dxy = [ (0, 1), (1, 0), (0, -1), (-1, 0)]
        nr, nc = self.agent[0] + dxy[dir][0], self.agent[1] + dxy[dir][1]
        # 움직일 수 있는 위치인지 파악한다.
        if nr < 0 or nr >= self.n or nc < 0 or nc >= self.m or self.map[nr][nc] != 0:
            nr, nc = self.agent
        return (nr, nc)

    # 목적지 도착여부 검사
    def isFinish(self):
        return self.agent == self.end

    # 결과값 출력하기
    def print(self, ss):
        print("-"*(9*self.m))
        for r in range(self.n):
            line = ""
            for c in  range(self.m):
                if self.map[r][c] == 1:
                    line += " [-----] "
                else:
                    line += "{:^9.2f}".format(ss[r][c])
            print(line)
        print("-"*(9*self.m))
                    
        

mapName = input(" Map name : ")
iterCount = int((input("iteration Count : ")))
alpha = float(input("Learning rate: "))
env = GridWorld(mapName)

ss = [[0] *env.m for _ in range(env.n)]
#print(env.init())
for _ in range(iterCount):
    env.init()
    while not env.isFinish():
        # 어디로 갈지 결정합니다.
        maxv, dir = -10**9, 0
        for d in range(4):
            ns = env.nextState(d)
            if maxv < ss[ns[0]][ns[1]]:
                maxv, dir = ss[ns[0]][ns[1]], d

        # 현재의 상태값을 업데이트 합니다.
        ss[env.agent[0]][env.agent[1]] =(1-alpha) * ss[env.agent[0]][env.agent[1]] + alpha*(-1 + maxv) 
        # 선택한 dir로 움직입니다.
        env.move(dir)        
        
        

env.print(ss)
        
        