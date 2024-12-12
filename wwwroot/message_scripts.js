const user1Messages = document.getElementById('user1-messages');
const user2Messages = document.getElementById('user2-messages');
const user1Input = document.getElementById('user1-message');
const user2Input = document.getElementById('user2-message');
const user1Send = document.getElementById('user1-send');
const user2Send = document.getElementById('user2-send');

const user1Id = 1;
const user2Id = 2;

// �û�1���� WebSocket
const socketUser1 = new WebSocket(`wss://localhost:7124/wss?userId=${user1Id}`);
// �û�2���� WebSocket
const socketUser2 = new WebSocket(`wss://localhost:7124/wss?userId=${user2Id}`);

// ���� WebSocket ��Ϣ
function handleWebSocketMessage(event, user) {
    console.log("Received message:", event.data,user);
    const data = JSON.parse(event.data);
    if (!data.content) return;
    const messageElement = document.createElement('div');
    messageElement.classList.add('message', 'user1');
    messageElement.textContent = data.content;
    if (user === 'user1') {
        user1Messages.appendChild(messageElement);
    } else {
        user2Messages.appendChild(messageElement);
    }

    scrollToBottom(user);
}

// �����û�1�� WebSocket ��Ϣ
socketUser1.onmessage = (event) => handleWebSocketMessage(event, 'user1');
// �����û�2�� WebSocket ��Ϣ
socketUser2.onmessage = (event) => handleWebSocketMessage(event, 'user2');

// WebSocket ���Ӵ�
socketUser1.onopen = () => {
    console.log("User 1 WebSocket connection established.");
};

socketUser2.onopen = () => {
    console.log("User 2 WebSocket connection established.");
};

// WebSocket ���ӹرղ��Զ�����
function reconnectWebSocket(socket, userId) {
    console.log(`${userId} WebSocket connection closed, attempting to reconnect...`);
    const socketNew = new WebSocket(`wss://localhost:7124/wss?userId=${userId}`);
    socketNew.onmessage = (event) => handleWebSocketMessage(event, userId === 1 ? 'user1' : 'user2');
    return socketNew;
}

// �û�1���û�2�� WebSocket ���ӹر�ʱ����
socketUser1.onclose = () => {
    socketUser1 = reconnectWebSocket(socketUser1, 1);
};
socketUser2.onclose = () => {
    socketUser2 = reconnectWebSocket(socketUser2, 2);
};

const authToken1 = "Bearer 1";
const authToken2 = "Bearer 2";

user1Send.addEventListener('click', () => {
    const message = user1Input.value.trim();
    if (message) {
        // ����Ϣ��ӵ��û�1����Ϣ��
        const messageElement = document.createElement('div');
        messageElement.classList.add('message', 'user2');
        messageElement.textContent = message;
        user1Messages.appendChild(messageElement);

        // ���ú�̨������վ����
        fetch('/message', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': authToken1  // �� token ��ӵ�����ͷ
            },
            body: JSON.stringify({
                recipientId: 2,
                type: 1,
                subject: 'Chat Message',
                content: message,
                relatedProjectId: 1
            })
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    // ���ͳɹ���ͨ�� WebSocket ���͵����촰��
                    socketUser1.send(JSON.stringify({ sender: 'user1', message }));
                    user1Input.value = '';
                }
            })
            .catch(error => {
                console.error('Error sending message:', error);
            });
    }
});

// �û�2������Ϣ
user2Send.addEventListener('click', () => {
    const message = user2Input.value.trim();
    if (message) {
        const messageElement = document.createElement('div');
        messageElement.classList.add('message', 'user2');
        messageElement.textContent = message;
        user2Messages.appendChild(messageElement);

        // ���ú�̨������վ����
        fetch('/message', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': authToken2  // �� token ��ӵ�����ͷ
            },
            body: JSON.stringify({
                recipientId: 1,
                type: 1,
                subject: 'Chat Message',
                content: message,
                relatedProjectId: 1
            })
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    socketUser2.send(JSON.stringify({ sender: 'user2', message }));
                    user2Input.value = '';
                }
            })
            .catch(error => {
                console.error('Error sending message:', error);
            });
    }
});

function scrollToBottom(user) {
    const container = user === 'user1' ? user1Messages : user2Messages;
    container.scrollTop = container.scrollHeight;
}



const titles = ["Real Time Message            ", "Try Say Hello           ", "Dr. Xing Studio              "];
let titleIndex = 0;

function typeTitle() {
    const titleElement = document.querySelector(".title");
    let text = titles[titleIndex];
    let i = 0;

    titleElement.textContent = "";
    const typingInterval = setInterval(() => {
        titleElement.textContent += text.charAt(i);
        i++;
        if (i === text.length) {
            clearInterval(typingInterval);
            setTimeout(() => {
                titleIndex = (titleIndex + 1) % titles.length;
                typeTitle();
            }, 2000);  // �ӳ�2����л�����һ������
        }
    }, 100);  // ÿ100�����һ����
}

// �����������ֻ�Ч��
window.onload = typeTitle;
