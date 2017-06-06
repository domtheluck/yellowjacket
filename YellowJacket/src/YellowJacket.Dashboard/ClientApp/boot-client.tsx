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

import "font-awesome/css/font-awesome.css";

import "./css/site.css";
import "bootstrap";

import * as React from "react";
import * as ReactDOM from "react-dom";

import { browserHistory, Router } from "react-router";

import { Provider } from "react-redux";
import { syncHistoryWithStore } from "react-router-redux";
import routes from "./routes";
import configureStore from "./configureStore";
import { IApplicationState }  from "./store";

// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = (window as any).initialReduxState as IApplicationState;
const store = configureStore(initialState);
const history = syncHistoryWithStore(browserHistory, store);

// This code starts up the React app when it runs in a browser. It sets up the routing configuration
// and injects the app into a DOM element.
ReactDOM.render(
    <Provider store={ store }>
        <Router history={ history } children={ routes } />
    </Provider>,
    document.getElementById("react-app")
);
