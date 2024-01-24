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
}