let mediaRecorder;
let audioChunks = [];
let permissionStatusObj = null;

export async function checkMicrophonePermission() {
    if (navigator.permissions && navigator.permissions.query) {
        try {
            permissionStatusObj = await navigator.permissions.query({ name: 'microphone' });
            return permissionStatusObj.state; // 'granted', 'prompt', 'denied'
        } catch (err) {
            console.warn("Permissions API query failed:", err);
            return 'prompt';
        }
    }
    return 'prompt';
}

export async function startRecording(dotNetHelper) {
    audioChunks = [];

    // Event-Listener registrieren, falls sich Berechtigungen während der Laufzeit ändern
    if (permissionStatusObj) {
        permissionStatusObj.onchange = () => {
            dotNetHelper.invokeMethodAsync('OnPermissionChanged', permissionStatusObj.state);
        };
    }

    const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
    mediaRecorder = new MediaRecorder(stream);

    mediaRecorder.ondataavailable = (event) => {
        if (event.data.size > 0) {
            audioChunks.push(event.data);
        }
    };

    mediaRecorder.start();
}

export function stopRecording(dotNetHelper) {
    if (!mediaRecorder) return;

    mediaRecorder.onstop = async () => {
        const audioBlob = new Blob(audioChunks, { type: mediaRecorder.mimeType || 'audio/webm' });
        const reader = new FileReader();

        reader.onloadend = async () => {
            const base64String = reader.result.split(',')[1];
            await dotNetHelper.invokeMethodAsync('OnAudioCaptured', base64String, mediaRecorder.mimeType);
        };

        reader.readAsDataURL(audioBlob);

        mediaRecorder.stream.getTracks().forEach(track => track.stop());
    };

    mediaRecorder.stop();
}