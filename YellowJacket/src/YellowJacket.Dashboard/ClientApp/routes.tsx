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

import * as React from "react";
import { Router, Route, hashHistory , IndexRedirect } from "react-router";

import Home from "./components/views/Home";
import Agents from "./components/views/Agent/Agents"

import View2 from "./components/views/View2";

import { Main } from "./components/layouts/Main";
import Blank from "./components/layouts/Blank";

import NotFound from "./components/common/NotFound";

export default (
    <Router history={hashHistory}>
        <Route path="/" component={Main}>
            //<IndexRedirect to="/home" />
            <Route path="home" component={Home}></Route>
            <Route path="agents" component={Agents}></Route>
            <Route path="*" component={NotFound} />
        </Route>
    </Router>
);

// enable Hot Module Replacement (HMR)
if (module.hot) {
    module.hot.accept();
}
