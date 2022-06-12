function IncrementQuantity(n) {
    var Value = parseInt(document.getElementById("Quantity{" + n + "}").value);
    Value = Value + 1;
    document.getElementById("Quantity{" + n + "}").value = Value;
}
function DecrementQuantity(n) {
    var Value = parseInt(document.getElementById("Quantity{" + n + "}").value);
    if (Value > 0) {
        Value = Value - 1;
    }
    document.getElementById("Quantity{" + n + "}").value = Value;
}
//function ExtractValue(n)
//{
//    var selectTag = document.getElementById("QuantityDD{" + n + "}");
//    alert(selectTag.value);
//    document.getElementById("QuantityIn{" + n + "}").value = selectTag.value;
//}
function ExtractValue() {
    var selectTag = document.querySelector("#QuantityDD");
    alert(selectTag.value);
    document.querySelector("#QuantityIn").value = selectTag.value;
}

