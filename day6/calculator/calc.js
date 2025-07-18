let expression = "";
const display = document.getElementById('display');

function appendValue(value) {
    expression += value;
    updateDisplay();
}

function compute() {
    try {
        const result = eval(expression);
        display.value = result;
        expression = result.toString();
    } catch (error) {
        display.value = "Error";
        expression = "";
    }
}

function clearDisplay() {
    expression = "";
    updateDisplay();
}

function updateDisplay() {
    display.value = expression;
}
