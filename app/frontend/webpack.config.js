const path = require("path");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const postcssPresetEnv = require("postcss-preset-env");
const CssMinimizerPlugin = require("css-minimizer-webpack-plugin");
const sass = require('sass');
const glob = require("glob");

// get all scss files
filenames = glob.sync("**/*.scss").filter(file => {
    return file.match(/node_modules/) === null;
});

filenames = filenames.map(file => {
    return "./" + file;
})

const devMode = process.env.NODE_ENV !== "production";

module.exports = {
    mode: devMode ? "development" : "production",
    entry: filenames,
    output: {
        path: path.resolve("./src/UltraTone.SharedUI/wwwroot"),
    },
    watchOptions: {
        ignored: /node_modules/
    },
    module: {
        rules: [
            {
                test: /\.(sc|c)ss$/i,
                exclude: /node_modules/,
                use: [
                    MiniCssExtractPlugin.loader,
                    "css-loader",
                    {
                        loader: "sass-loader",
                        options: {
                            implementation: sass,
                        },
                    },
                ],
            },
        ],
    },
    optimization: {
        minimizer: [
            new CssMinimizerPlugin(),
        ],
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename: devMode ? "css/app.css" : "css/[name].css?t=[hash]"
        })
    ]
};
