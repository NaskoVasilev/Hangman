let elements = document.getElementsByClassName("alphabet-letter");
let instance;

//for (let element of elements) {
//    element.addEventListener("click", (e) => {
//        let letter = e.target.textContent;
//        instance.invokeMethodAsync('ReceiveLetter', letter);
//    });
//}

function invokeReceiveLetter() {
    let letter = this.target.textContent;
    instance.invokeMethodAsync('ReceiveLetter', letter);
}

window.createInstance = (classInstance) => {
    instance = classInstance;
};

