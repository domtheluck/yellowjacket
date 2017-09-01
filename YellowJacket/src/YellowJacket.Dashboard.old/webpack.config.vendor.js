// ***********************************************************************
// Copyright (c) 2017 Dominik Lachance
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

const path = require("path");
const webpack = require("webpack");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const merge = require("webpack-merge");

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    const extractCss = new ExtractTextPlugin("vendor.css");

    const sharedConfig = {
        stats: { modules: false },
        resolve: { extensions: [ ".js" ] },
        module: {
            rules: [
                { test: /\.(png|woff|woff2|eot|ttf|svg)(\?|$)/, use: "url-loader?limit=100000" }
            ]
        },
        entry: {
            vendor: [
                "bootstrap",
                "bootstrap/dist/css/bootstrap.css",
                "inspinia",
                "inspinia/dist/fonts.css",
                "inspinia/dist/inspinia.css",
                "domain-task",
                "event-source-polyfill",
                "react",
                "react-dom",
                "react-router",
                "react-redux",
                "redux",
                "redux-thunk",
                "react-router-redux",
                "jquery",
                "animate.css/animate.css"
            ]
        },
        output: {
            publicPath: "/dist/",
            filename: "[name].js",
            library: "[name]_[hash]"
        },
        plugins: [
            new webpack.ProvidePlugin({
                jQuery: "jquery",
                "window.jQuery": "jquery",
                "window.$": "jquery",
                $: "jquery"
            }), // maps these identifiers to the jQuery package (because Bootstrap expects it to be a global variable)
            new webpack.NormalModuleReplacementPlugin(/\/iconv-loader$/, require.resolve("node-noop")), // Workaround for https://github.com/andris9/encoding/issues/16
            new webpack.DefinePlugin({
                'process.env.NODE_ENV': isDevBuild ? '"development"' : '"production"'
            })
        ]
    };

    const clientBundleConfig = merge(sharedConfig, {
        output: { path: path.join(__dirname, "wwwroot", "dist") },
        module: {
            rules: [
                { test: /\.css(\?|$)/, use: extractCss.extract({ use: "css-loader" }) }
            ]
        },
        plugins: [
            extractCss,
            new webpack.DllPlugin({
                path: path.join(__dirname, "wwwroot", "dist", "[name]-manifest.json"),
                name: "[name]_[hash]"
            })
        ].concat(isDevBuild ? [] : [
            new webpack.optimize.UglifyJsPlugin()
        ])
    });

    const serverBundleConfig = merge(sharedConfig, {
        target: "node",
        resolve: { mainFields: ["main"] },
        output: {
            path: path.join(__dirname, "ClientApp", "dist"),
            libraryTarget: "commonjs2"
        },
        module: {
            rules: [ { test: /\.css(\?|$)/, use: "css-loader" } ]
        },
        entry: { vendor: ["aspnet-prerendering", "react-dom/server"] },
        plugins: [
            new webpack.DllPlugin({
                path: path.join(__dirname, "ClientApp", "dist", "[name]-manifest.json"),
                name: "[name]_[hash]"
            })
        ]
    });

    return [clientBundleConfig, serverBundleConfig];
};
