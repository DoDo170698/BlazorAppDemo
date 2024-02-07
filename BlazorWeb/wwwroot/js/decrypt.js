window.methods = {

    decompressByteStringToString: async function ({ key, iv, byteString }) {

        var result = atob(byteString);
        const jsonData = JSON.parse(result);

        return jsonData;
    },

    decompressByteArrayToString: async function ({ key, iv, byteArray }) {
        let decoder = new TextDecoder();
        let result = decoder.decode(byteArray);
        const jsonData = JSON.parse(result);

        return jsonData;
    },
}