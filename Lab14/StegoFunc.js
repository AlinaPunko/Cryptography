let bit0 = '\u0020' //space
let bit1 = '\u00A0' //NO-BREAK space
//let bit0 = '\u02C2' // <- button
//let bit1 = '\u02C3' // -> button

// Convert string to bit string from 8-bit to 10-bit frames
// with 1-start and 0-end bit.
function tobits(data) {
	let bits = '';

	for (let i = 0; i < data.length; i++) {
		let c = data.charCodeAt(i);

		bits += bit1;
		bits += (c & 128) ? bit1 : bit0;
		bits += (c &  64) ? bit1 : bit0;
		bits += (c &  32) ? bit1 : bit0;
		bits += (c &  16) ? bit1 : bit0;
		bits += (c &   8) ? bit1 : bit0;
		bits += (c &   4) ? bit1 : bit0;
		bits += (c &   2) ? bit1 : bit0;
		bits += (c &   1) ? bit1 : bit0;
		bits += bit0;
	}
	return bits
}


// Check sync of bit string from  i-bit
// Check 10-bit frame (1 - start bit, 0 - stop bit) 
// check n good frames.
function checksync(bits, i, n) {
	for (let j = i; j < i + 10 * n; j += 10) {
		if (j + 9 < bits.length) {
			if (bits[j + 0] != bit1) return false;
			if (bits[j + 9] != bit0) return false;
		}
	}
	return true
}


// Decode string, 8-bit encoding in 10-bit frames.
// Check sync in every step. 
function frombits(bits) {
	let insync = checksync(bits, 0, 1);
	let data   = '';
	console.log(bits.length)

	for (let i = 0; i < bits.length; ) {
		if (insync) {
			if (checksync(bits, i, 1)) {
				let c = 0;

				if (bits[i + 1] == bit1) c |= 128
				if (bits[i + 2] == bit1) c |=  64
				if (bits[i + 3] == bit1) c |=  32
				if (bits[i + 4] == bit1) c |=  16
				if (bits[i + 5] == bit1) c |=   8
				if (bits[i + 6] == bit1) c |=   4
				if (bits[i + 7] == bit1) c |=   2
				if (bits[i + 8] == bit1) c |=   1

				data += String.fromCharCode(c);
				console.log('|'+data+'|')

				i += 10;
			} 
			else {
				insync = false;
			}
		} 
		else {
			if (checksync(bits, i, 4))
				insync = true;
			else
				i += 1;
		}
	}
	return data;
}

function istext(c) {
	return (c != '\u0020' && c != '\u00A0' && c != '\t');
}
// is end of string?
function iseol(c) {
	return (c == '\n');
}

// Return bit string, from space.
function decode(data) {
	let intext = false;
	let bits   = '';

	for (let i = 0; i < data.length; i++) {
		if (intext) {
			if (data[i] == bit0)
				bits += bit0;
			if (data[i] == bit1)
				bits += bit1;
			console.log('bit'+bits + '| |' + bit1 + '| |' + bit0)
		}

		if (istext(data[i])) intext = true;
		if (iseol (data[i])) intext = false;
	}
	return bits;
}


// Encode bit string in empty space.
function encode(text, bits) {
	let intext = false;
	let data   = '';
	let j      = 0;

	for (let i = 0; i < text.length; i++) {
		if (intext && j < bits.length && (text[i] == bit0 || text[i] == bit1))
			data += bits[j++];
		else
			data += text[i];

		if (istext(text[i])) intext = true;
		if (iseol (text[i])) intext = false;
	}
	return data;
}

// Delete white mark.
function sanitize(data) {
	return data.replace(/\u00A0/g, '\u0020');
}