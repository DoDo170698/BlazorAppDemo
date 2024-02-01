window.methods = {


    decompressByteArrayToString: async function ({ key, iv, byteArray }) {

        var result = msgpack.decode(byteArray);
        const jsonData = JSON.parse(result);

        return jsonData;
    },
}