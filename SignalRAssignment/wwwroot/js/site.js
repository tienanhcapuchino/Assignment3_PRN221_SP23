
"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/signalRServer")
    .build();

connection.on("LoadPosts", function () {
    location.href = '/Posts'
});
connection.start().catch(function (err) {
    return console.error(err.toString());
});

connection.on("LoadPostCate", function () {
    location.href = '/PostCategories'
});
connection.start().catch(function (err) {
    return console.error(err.toString());
});

connection.on("LoadApp", function () {
    location.href = '/AppUsers'
});
connection.start().catch(function (err) {
    return console.error(err.toString());
});