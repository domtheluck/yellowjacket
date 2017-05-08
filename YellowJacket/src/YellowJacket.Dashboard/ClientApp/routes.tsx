import * as React from "react";
import { Router, Route, hashHistory , IndexRedirect } from "react-router";

import Home from "./components/views/Home";
import Agents from "./components/views/Agents"
import View1 from "./components/views/View1";
import View2 from "./components/views/View2";

import { Main } from "./components/layouts/Main";
import Blank from "./components/layouts/Blank";

export default (
    <Router history={hashHistory}>
        <Route path="/" component={Main}>
            //<IndexRedirect to="/home" />
            <Route path="home" component={Home}> </Route>
            <Route path="agents" component={Agents}></Route>
            <Route path="view1" component={View1}></Route>
            <Route path="view2" component={View2}></Route>
        </Route>
    </Router>
);

// Enable Hot Module Replacement (HMR)
if (module.hot) {
    module.hot.accept();
}
