// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//search bar clearer
document.addEventListener("DOMContentLoaded", function () {

    const input = document.querySelector("input[name='search']");

    if (!input) return;

    const form = input.closest("form");

    input.addEventListener("input", function () {
        if (input.value === "") {
            form.submit();
        }
    });

});