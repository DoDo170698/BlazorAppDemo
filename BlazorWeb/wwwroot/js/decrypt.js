window.methods = {


    decompressByteArrayToString: async function ({ key, iv, byteArray }) {

        //var bytes = new Int32Array(byteArray);
        //var s = JSON.stringify(bytes, function (k, v) {
        //    if (v instanceof Int32Array) {
        //        return Array.apply([], v);
        //    }
        //    return v;
        //});
        //console.log(s, "s cc");

        //var bytes = new Int32Array(byteArray);
        //var s = JSON.stringify(bytes);
        //console.log(s, "s");

        //var result = msgpack.decode(byteArray);

        let decoder = new TextDecoder();
        let result = decoder.decode(byteArray);
        const jsonData = JSON.parse(result);

        return jsonData;

        //console.log(byteArray, "byteArray");
        //var bytes = new Uint8Array(byteArray);
        //let decoder = new TextDecoder();
        //let result = decoder.decode(bytes);
        //console.log(result, "result");

        //return result;
    },
}