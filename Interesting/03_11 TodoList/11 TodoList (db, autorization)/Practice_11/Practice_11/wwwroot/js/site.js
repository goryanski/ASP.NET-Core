// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let tasks = document.querySelectorAll('.complete-task');
for (var i = 0; i < tasks.length; i++) {
    tasks[i].onclick = function (e) {
        // set task backgroundColor
        e.target.parentNode.parentNode.parentNode.style.backgroundColor = "grey";
    };
}