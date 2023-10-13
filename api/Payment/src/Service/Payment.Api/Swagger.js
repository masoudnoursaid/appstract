(req) => {
    if (req.url.endsWith('swagger.json')) return req;
    var now = Date.now();
    req.headers['{HmacAuthentication.DATE_HEADER}'] = now;
    var secret = req.headers['{ApiSecretHeaderName}'];
    delete req.headers['{ApiSecretHeaderName}'];
    var uuid = ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
    var uri = new URL(req.url);
    var payload = req.method.toUpperCase() + ';' + '/' + ';' + uuid + ';' + uri.hostname + ';' + now;
    console.log(payload);
    var hmac = CryptoJS.algo.HMAC.create(CryptoJS.algo.SHA256, secret);
    hmac.update(payload);
    req.headers['{HmacAuthentication.SIGNATURE_HEADER}'] = CryptoJS.enc.Base64.stringify(hmac.finalize());
    req.headers['{HmacAuthentication.NONCE_HEADER}'] = uuid;
    return req;
}
