// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

 
   function darkMode(x) {
       var element = document.body;
       element.classList.toggle("mystyle");
       element.classList.toggle("dark-mode");
       x.classList.toggle("fa-sun");

        
}

// This function clear all the values
function clearScreen() {
    document.getElementById("result").value = "";
}

// This function display values
function display(value) {
    
    document.getElementById("result").value += value;
}
 

// This function evaluates the expression and return result
function calculate() {
    var p = document.getElementById("result").value;
    var q = eval(p);
    document.getElementById("result").value = q;
}