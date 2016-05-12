var events = require('events');
var eventEmitter = new events.EventEmitter();
var Promise = require("promise");

module.exports = (url) => {

    return new Promise((resolve, reject) => {

        try {
            var socket = new WebSocket("ws://" + url);
        } catch (error) {
            return reject(error);
        }

        socket.onmessage = (event) => {
            eventEmitter.emit('message', event.data);
        }
        eventEmitter.on("send", data => {
            socket.send(data);
        });

        socket.onerror = (event) => {
            eventEmitter.emit('error', event.data);
        }

        socket.onopen = (event) => {
            eventEmitter.emit('connection-open', "Connection to " + url);


            return resolve(eventEmitter);
        };



    });

};