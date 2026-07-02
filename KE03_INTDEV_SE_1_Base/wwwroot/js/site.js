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

//profile icon dropdown
//wait until full html loaded until we retrieve elements
document.addEventListener("DOMContentLoaded", function () {
    console.log("JS loaded");

    const profileButton = document.getElementById("profileButton");
    const profileMenu = document.getElementById("profileMenu");
    console.log("button:", profileButton);
    console.log("menu:", profileMenu);

    // toggle open and close menu from the icon
    function toggleMenu(e) {
        e.preventDefault();
        e.stopPropagation();

        profileMenu.classList.toggle("open");
    }

    // open or close menu when clicking icon
    profileButton.addEventListener("click", toggleMenu);

    // when interact outside icon close menu
    document.addEventListener("click", function (e) {
        if (!profileMenu.contains(e.target) &&
            e.target !== profileButton) {

            profileMenu.classList.remove("open");
        }
    });
});

//logout conceptual
const logoutButton = document.getElementById("logoutButton");

if (logoutButton) {
    logoutButton.addEventListener("click", function (e) {
        e.preventDefault(); // <-- DIT is wat de # en navigatie stopt
        e.stopPropagation();

        alert("Je bent uitgelogd");
    });
}