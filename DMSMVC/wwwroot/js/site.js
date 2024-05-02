let error = document.getElementById("error")
let pin = document.querySelector("#pin")
let confirmpin = document.querySelector("#confirmpin")
let form = document.querySelector(".wrapper")
form.addEventListener("submit", (e) => {
    //e.preventDefault();
    console.log("entered")
    if (confirmpin.value !== pin.value) {
        let errorMessage = document.createElement("p");
        errorMessage.textContent = "Password does not match";
        errorMessage.setAttribute("style", "color:red");
        error.appendChild(errorMessage);
    }
})