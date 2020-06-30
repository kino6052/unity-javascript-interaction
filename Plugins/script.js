const HOST = "192.168.0.75";
const PORT = "9966";

const ws = new WebSocket(`ws://${HOST}:${PORT}`);

window.OnMessageCallback = (message) => {
  ws.send(message)
}
ws.onmessage = (event) => {
  const { data } = event;
  unityInstance.SendMessage('Cube', '_ReceivePositions', data);
}

setInterval(() => {
  unityInstance.SendMessage('Cube', 'GetCurrentPosition');
}, 100);