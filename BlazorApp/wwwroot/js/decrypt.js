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

    decompressByteArrayToString: async function (byteString) {
        // Tạo ArrayBuffer từ chuỗi byte
        const buffer = new TextEncoder().encode(byteString).buffer;

        // Sử dụng TextDecoder để chuyển đổi ArrayBuffer thành văn bản
        const text = new TextDecoder('utf-8').decode(buffer);

        // Chuyển đổi văn bản thành đối tượng JSON
        const jsonData = JSON.parse(text);

        return jsonData;
    }
}