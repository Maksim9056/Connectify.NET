window.webRTCInterop = {
    peerConnection: null,

    initialize: async function () {
        const config = { iceServers: [{ urls: "stun:stun.l.google.com:19302" }] };
        this.peerConnection = new RTCPeerConnection(config);

        this.peerConnection.ontrack = function (event) {
            const audioElement = document.createElement("audio");
            audioElement.srcObject = event.streams[0];
            audioElement.autoplay = true;
            document.body.appendChild(audioElement);
        };
    },

    startCall: async function () {
        const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
        stream.getTracks().forEach(track => this.peerConnection.addTrack(track, stream));

        const offer = await this.peerConnection.createOffer();
        await this.peerConnection.setLocalDescription(offer);
        return offer;
    },

    answerCall: async function (offer) {
        await this.peerConnection.setRemoteDescription(new RTCSessionDescription(offer));

        const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
        stream.getTracks().forEach(track => this.peerConnection.addTrack(track, stream));

        const answer = await this.peerConnection.createAnswer();
        await this.peerConnection.setLocalDescription(answer);
        return answer;
    },

    handleOffer: async function (offer) {
        if (!this.peerConnection) {
            await this.initialize();
        }

        const answer = await this.answerCall(offer);
        return answer;
    },

    handleAnswer: async function (answer) {
        await this.peerConnection.setRemoteDescription(new RTCSessionDescription(answer));
    },

    endCall: function () {
        if (this.peerConnection) {
            this.peerConnection.close();
            this.peerConnection = null;
        }
    }
};
