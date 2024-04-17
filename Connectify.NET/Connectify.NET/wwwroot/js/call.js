// Подключение к SignalR хабу
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/webrtchub") // URL для подключения к SignalR хабу на сервере
    .build();

// Обработчик события при получении предложения (offer) от другого участника
connection.on("ReceiveOffer", async (offer) => {
    try {
        // Установка удаленного описания (SDP) предложения (offer)
        await peerConnection.setRemoteDescription(new RTCSessionDescription({ type: "offer", sdp: offer }));

        // Создание ответа (answer) на полученное предложение
        const answer = await peerConnection.createAnswer();
        await peerConnection.setLocalDescription(answer);

        // Отправка ответа (answer) на сервер через SignalR
        await connection.invoke("SendAnswer", answer.sdp);

    } catch (error) {
        console.error('Ошибка при обработке предложения (offer):', error);
    }
});

// Обработчик события при получении ответа (answer) от другого участника
connection.on("ReceiveAnswer", async (answer) => {
    try {
        // Установка удаленного описания (SDP) ответа (answer)
        await peerConnection.setRemoteDescription(new RTCSessionDescription({ type: "answer", sdp: answer }));

    } catch (error) {
        console.error('Ошибка при обработке ответа (answer):', error);
    }
});

// Обработчик события при получении ICE-кандидата (ICE candidate) от другого участника
connection.on("ReceiveIceCandidate", async (candidate) => {
    try {
        // Добавление ICE-кандидата (ICE candidate) к соединению WebRTC
        await peerConnection.addIceCandidate(new RTCIceCandidate(candidate));

    } catch (error) {
        console.error('Ошибка при обработке ICE-кандидата (ICE candidate):', error);
    }
});

// Запуск соединения с SignalR
connection.start()
    .then(() => console.log("SignalR подключен"))
    .catch(err => console.error("Ошибка подключения к SignalR:", err));

// Функция для начала звонка
async function startCall() {
    try {
        // Получение доступа к локальному медиапотоку (видео и аудио)
        const localStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });

        // Отображение локального видео
        const localVideo = document.getElementById('localVideo');
        localVideo.srcObject = localStream;

        // Инициализация объекта RTCPeerConnection
        const peerConnection = new RTCPeerConnection();

        // Добавление локального медиапотока к соединению
        localStream.getTracks().forEach(track => {
            peerConnection.addTrack(track, localStream);
        });

        // Обработчик события при получении удаленного потока
        peerConnection.ontrack = handleRemoteStreamAdded;

        // Создание и отправка предложения (offer) на сервер через SignalR
        const offer = await peerConnection.createOffer();
        await peerConnection.setLocalDescription(offer);
        await connection.invoke("SendOffer", offer.sdp);

    } catch (error) {
        console.error('Ошибка при начале звонка:', error);
    }
}

// Обработчик события при получении удаленного потока (видео и аудио) от другого участника
function handleRemoteStreamAdded(event) {
    const remoteVideo = document.getElementById('remoteVideo');
    remoteVideo.srcObject = event.streams[0];
}

// Функция для завершения звонка
async function endCall() {
    if (peerConnection) {
        peerConnection.close();
        peerConnection = null;
    }

    const localVideo = document.getElementById('localVideo');
    localVideo.srcObject = null;

    const remoteVideo = document.getElementById('remoteVideo');
    remoteVideo.srcObject = null;
}
