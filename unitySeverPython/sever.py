import socket   # Network connection을 위한 디스크립터
import select   # Network event 선택자
import time     # 시간 관련된 내용

class Server:

    # Constructor
    def __init__(self, port):
        self.port = port
        self.running = False

        # 사용자 프로파일
        self.users = dict()

    # Start server
    def start(self):
        # Create a socket for listen
        self.listenSock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.listenSock.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)

        # Bind address to listen socket
        self.listenSock.bind(('', self.port))

        # Listen socket
        self.listenSock.listen(5)

        # Connected client sockets
        self.reads = [self.listenSock]

        # Infinite loop
        self.running = True
        while self.running:
            self.select()

        print("Shutdown")
        self.listenSock.close()

    # on connect
    def onConnect(self, sock):
        # 요청이 온 클라이언트에 대하여 접속 수락
        client, addr = sock.accept()
        print(f"Connected from {addr}")
        self.reads.append(client)
        self.users[client] = ["Anonymous", "0000" ]

    # on close
    def onClose(self, sock):
        self.reads.remove(sock)
        del self.users[sock]

    # on recv
    def onRecv(self, sock):
        # 클라이언트로부터 온 데이터를 수신한다.
        try:
            data = sock.recv(1024)
            if data == None or len(data) <= 0:
                print("closed by peer")
                self.onClose(sock)
                return
        except socket.error:
            print("socket error")
            self.onClose(sock)
            return
        sdata = data.decode()
        if sdata[0] == "/":
            ss = sdata.split()
            if ss[0][1:] == "name":
                u = self.users[sock]
                print(f"Change name {u[0]} --> {ss[1]}")
                u[0] = ss[1]
            elif ss[0][1:] == "number":
                u = self.users[sock]
                print(f"Change number {u[0]} --> {ss[1]}")
                u[1] = ss[1]
            elif ss[0][1:] == "shutdown":
                self.running = False
            return
        user = self.users[sock][0]
        phone = self.users[sock][1]
        mesg = f"{user}({phone}) : {sdata}"
        print(mesg)
        self.broadcast(mesg.encode())

    # onidle
    def onidle(self):
        print("metaverse server is alive")

    # Select
    def select(self):
        reads, _, _ = select.select(self.reads, [], [], 10.0)

        # for every socket events
        for s in reads:
            # if s is listen socket
            if s == self.listenSock: self.onConnect(s)
            # if s is not listen socket
            else: self.onRecv(s)
                
        # timeout event
        if len(reads) == 0:
            self.onidle()
            
    # Broadcast
    def broadcast(self, data):
        for s in self.reads:
            if s != self.listenSock:
                s.send(data)

server = Server(8888)
server.start()


                