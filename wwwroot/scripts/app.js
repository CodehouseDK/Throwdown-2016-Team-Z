require("../style/style.css");
var socket = require("./websocket");
var VanillaModal = require('vanilla-modal');
var socketEvents = socket("localhost:5004").then(events => {
    events.on('connection-open', data => { console.log(data) });

    events.on('message', data => { console.log(data) });

    events.emit('send', "Hello Backend");
});



const modal = new VanillaModal();