window.methods = {
    decryptAES: async function ({ key, iv, encryptedString }) {
        const crypto = CryptoJS;

        const ciphertextWA = crypto.enc.Base64.parse(encryptedString);
        const keyWA = crypto.enc.Utf8.parse(key);
        const ivWA = crypto.enc.Utf8.parse(iv);
        const ciphertextCP = { ciphertext: ciphertextWA };

        const decrypted = await crypto.AES.decrypt(
            ciphertextCP,
            keyWA,
            { iv: ivWA }
        );
        console.log('decrypted', decrypted);

        // Chuyển đổi kết quả thành UTF-8
        const result = decrypted.toString(crypto.enc.Utf8);
        console.log('result', result);

        return JSON.parse(result);
    },

    decompressByteStringToString: async function ({ key, iv, byteString }) {
        var binaryString = atob(byteString);
        var bytes = new Uint8Array(binaryString.length);
        for (var i = 0; i < binaryString.length; i++) {
            bytes[i] = binaryString.charCodeAt(i);
        }
        var data = msgpack.decode(bytes);
        return data;
    },

    decompressByteArrayToString: async function ({ key, iv, byteArray }) {
        // Tạo ArrayBuffer từ chuỗi byte
        //const byteArray = byteString.split(',').map(Number);
        //debugger;

        var result = msgpack.decode(byteArray);
        const jsonData = JSON.parse(result);

        //let buffer = Buffer.from(byteArray);
        //var result = Buffer.from(byteArray.buffer).toString();
        //var result = utf8ArrayToStrfunction(byteArray);
        //var result = (new TextDecoder()).decode(byteArray);
        //var result = byteArray.map(b => String.fromCharCode(b)).join("");;

        //var out, i, a, c;
        //var char2, char3;
        //out = "";
        //a = byteArray.length;
        //i = 0;
        //while (i < a) {
        //    c = byteArray[i++];
        //    switch (c >> 4) {
        //        case 0: case 1: case 2: case 3: case 4: case 5: case 6: case 7:
        //            // 0xxxxxxx
        //            out += String.fromCharCode(c);
        //            break;
        //        case 12: case 13:
        //            // 110x xxxx   10xx xxxx
        //            char2 = byteArray[i++];
        //            out += String.fromCharCode(((c & 0x1F) << 6) | (char2 & 0x3F));
        //            break;
        //        case 14:
        //            // 1110 xxxx  10xx xxxx  10xx xxxx
        //            char2 = byteArray[i++];
        //            char3 = byteArray[i++];
        //            out += String.fromCharCode(((c & 0x0F) << 12) |
        //                ((char2 & 0x3F) << 6) |
        //                ((char3 & 0x3F) << 0));
        //            break;
        //    }
        //}

        //// Chuyển đổi văn bản thành đối tượng JSON
        //const jsonData = JSON.parse(out);

        return jsonData;
    },

    utf8ArrayToStrfunction: function (array) {
        var out, i, a, c;
        var char2, char3;
        out = "";
        a = array.length;
        i = 0;
        while (i < a) {
            c = array[i++];
            switch (c >> 4) {
                case 0: case 1: case 2: case 3: case 4: case 5: case 6: case 7:
                    // 0xxxxxxx
                    out += String.fromCharCode(c);
                    break;
                case 12: case 13:
                    // 110x xxxx   10xx xxxx
                    char2 = array[i++];
                    out += String.fromCharCode(((c & 0x1F) << 6) | (char2 & 0x3F));
                    break;
                case 14:
                    // 1110 xxxx  10xx xxxx  10xx xxxx
                    char2 = array[i++];
                    char3 = array[i++];
                    out += String.fromCharCode(((c & 0x0F) << 12) |
                        ((char2 & 0x3F) << 6) |
                        ((char3 & 0x3F) << 0));
                    break;
            }
        }
        return out;
    }
}