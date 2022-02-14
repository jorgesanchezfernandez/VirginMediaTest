const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/scenario",
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7218',
        secure: false
    });

    app.use(appProxy);
};
