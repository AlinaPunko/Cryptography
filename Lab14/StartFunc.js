function plainChange() {
	let plain   = document.getElementById("plain");
	let message = document.getElementById("message");
	let stego   = document.getElementById("stego");

	stego.value = encode(plain.value, tobits(message.value));

	confirm();
}

function messageChange() {
	let plain   = document.getElementById("plain");
	let message = document.getElementById("message");
	let stego   = document.getElementById("stego");

	stego.value = encode(plain.value, tobits(message.value));

	confirm();
}

function stegoChange() {
	let plain   = document.getElementById("plain");
	let message = document.getElementById("message");
	let stego   = document.getElementById("stego");

	message.value = frombits(decode(stego.value));
	plain.value   = sanitize(stego.value);

	confirm();
}


function confirm() {
	let message = document.getElementById("message");
	let stego   = document.getElementById("stego");

	if (message.value.localeCompare(frombits(decode(stego.value))))
		stego.style.color = 'red';
	else
		stego.style.color = 'black';
}