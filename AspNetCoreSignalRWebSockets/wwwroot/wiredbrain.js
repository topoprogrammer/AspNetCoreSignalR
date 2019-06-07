﻿listen = (id) => {
    const socket = new WebSocket(`ws://localhost:56513/Coffee/${id}`);
    //const socket = new WebSocket(`wss://localhost:44317/Coffee/${id}`); for TLS (HTTPS)

    socket.onmessage = event => {
        const statusDiv = document.getElementById("status");
        statusDiv.innerHTML = JSON.parse(event.data);
    };
};

document.getElementById("submit").addEventListener("click", e => {
    e.preventDefault();
    const product = document.getElementById("product").value;
    const size = document.getElementById("size").value;
    fetch("/Coffee",
        {
            method: "POST",
            body: { product, size }
        })
        .then(response => response.text())
        .then(text => listen(text));
});